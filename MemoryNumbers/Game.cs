using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MemoryNumbers
{
    [Flags]
    public enum PlayMode
    {
        TimeFixed = 0b_0000_0001,
        TimeIncremental = 0b_0000_0010,
        SequenceProgressive = 0b_0000_0100,
        SequenceRandom = 0b_0000_1000
    }

    public class Game
    {
        #region Private variables

        private int _nMaxDigit = 10;
        private int _nMinDigit = 0;
        /// It's the current length of the _nSequence array
        private int _nSequenceLength = 0; 
        private int _nSequenceIndex = 0;    // index (button clicked) whithin the _nSequence array
        private int _nMaxAttempts = 10;
        private int _nCurrAttempt = 0;      // The accumulated number of attempts
        private int _nMinLength = 2;        // The minimum length of the initial _nSequence array
        private int[] _nSequence;



        #endregion Private variables

        #region Public properties
        /// <summary>
        /// Maximum attempts allowed to the player.
        /// Only positive numbers > 0. Default is 10.
        /// </summary>
        [Description("Maximum attempts (minimum 1 attempt)"),
        Category("Sequence properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int MaximumAttempts { get => _nMaxAttempts; set => _nMaxAttempts = value < 1 ? 1 : value; }

        /// <summary>
        /// Minimum length of the initial sequence: minimum numbers of digits to display at the begining of the game.
        /// Only positive numbers > 0. Default is 2.
        /// </summary>
        [Description("Minimum initial length (minimum 1 digit)"),
        Category("Sequence properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int MinimumLength { get => _nMinLength; set => _nMinLength = value < 1 ? 1 : value; }

        /// <summary>
        /// Maximum excluded number (digit) of the sequence.
        /// Only positive numbers > 0 and >= MinimumDigit. Default is 10.
        /// </summary>
        [Description("Maximum excluded number (digit) of the sequence"),
        Category("Sequence properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int MaximumDigit { get => _nMaxDigit; set => _nMaxDigit = value < _nMinDigit ? _nMinDigit : value; }

        /// <summary>
        /// Minimum included number (digit) of the sequence.
        /// Only positive numbers >= 0. Default is 0.
        /// </summary>
        [Description("Minimum initial length (minimum 1 digit)"),
        Category("Sequence properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int MinimumDigit { get => _nMinDigit; set => _nMinDigit = value < 0 ? 0 : value; }

        /// <summary>
        /// Current score of the game: the length of the sequence correctly guessed by the player (the length of the array).
        /// Only positive numbers >= 0.
        /// </summary>
        /// [Description("Minimum digits (minimum 1 digit)"),
        [Description("Current score (length og the numeric sequence"),
        Category("Sequence properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int CurrentScore { get => _nSequenceLength; set => _nSequenceLength = value < 0 ? 0 : value; }

        /// <summary>
        /// (Read only) Gets the numeric array containing the current sequence.
        /// Its length corresponds to the CurrentScore property.
        /// </summary>
        [Description("Get the numeric sequence to represent (read only)"),
        Category("Sequence properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int[] GetSequence { get => _nSequence; }

        #endregion Public properties

        #region Events

            #region Events definitions
            public event EventHandler<TickEventArgs> Tick;
            public event EventHandler<WrongEventArgs> WrongSequence;
            public event EventHandler<CorrectEventArgs> CorrectSequence;
            public event EventHandler<OverEventArgs> GameOver;
            #endregion Events definitions

            #region Events encapsulation

            protected virtual void OnTick(TickEventArgs e)
            {
                if (Tick != null) Tick(this, e);
            }
            protected virtual void OnWrongSequence(WrongEventArgs e)
            {
                if (WrongSequence != null) WrongSequence(this, e);
            }
            protected virtual void OnCorrectSequence(CorrectEventArgs e)
            {
                if (CorrectSequence != null) CorrectSequence(this, e);
            }
            protected virtual void OnGameOver(OverEventArgs e)
            {
                if (GameOver != null) GameOver(this, e);
            }
            #endregion Events encapsulation

            #region EventArgs definitions
        public class TickEventArgs : EventArgs
        {
            public readonly bool Flash;
            public readonly Int32 ButtonValue;
            public TickEventArgs(bool flash, Int32 button)
            {
                Flash = flash;
                ButtonValue = button;
            }
        }

        public class WrongEventArgs : EventArgs
        {
            public readonly Int32 Score;
            public WrongEventArgs(Int32 value) { Score = value; }
        }
        public class CorrectEventArgs : EventArgs
        {
            public readonly Int32 Score;
            public CorrectEventArgs(Int32 value) { Score = value; }
        }
        public class OverEventArgs : EventArgs
        {
            public readonly Int32 Score;
            public OverEventArgs(Int32 value) { Score = value; }
        }
        #endregion EvantArgs definitions

        #endregion Events

        //if (GameOver != null) OnGameOver(new OverEventArgs(_nScore));

        public Game()
        {
            //_nScore = 5;
        }

        public bool Start()
        {
            // Check we didn't reach the _nMaxAttempts limits and that the sequence length is whithin the allowed digits limits
            _nSequenceLength += 1;
            // Increment the current attempts counter            
            _nCurrAttempt++;

            if ((_nCurrAttempt > _nMaxAttempts) || _nSequenceLength > (_nMaxDigit - _nMinDigit + 1))
            {
                _nSequence = null;
                OnGameOver(new OverEventArgs(_nSequenceLength - 1));
                return false;
            }
            _nSequenceIndex = 0;
            return SetSequence();
        }

        public void ReSet()
        {
            _nSequenceLength = _nMinLength - 1;
            _nSequenceIndex = 0;
            _nCurrAttempt = 0;
            _nSequence = null;
        }

        /// <summary>
        /// Generates a numeric secuence
        /// </summary>
        private bool SetSequence()
        {
            // Create and fill the array
            _nSequence = new int[_nSequenceLength];
            for (int i = 0; i < _nSequenceLength; i++)
            {
                _nSequence[i] = _nMinDigit - 1;
                _nSequence[i] = GetRandomNumber();
            }
            Array.Sort(_nSequence); // This is not necessary since the numbers would be random scattered in the board control
       
            return true;
        }

        public void Sequence(int length)
        {
            if (length >= _nMaxAttempts) length = 1;

            _nMaxAttempts++;

            int nLength = _nSequence == null ? 0 : _nSequence.Length;
            int nNewLength = length + 1;
            Random rnd = new Random();

            // Clear the array if it's not empty
            if (nLength > 0) Array.Clear(_nSequence, 0, nLength);

            _nSequence = (new int[nNewLength]).Select(i => _nMaxDigit).ToArray<int>();
            _nSequenceIndex = 0;

            for (int i = 0; i < nNewLength; i++)
            {
                _nSequence[i] = GetRandomNumber();
            }

            Array.Sort(_nSequence);

            return;
        }

        /// <summary>
        /// Generates a random number between _nMinDigit (included) and _nMaxDigit (excluded) which is not yet part of _nSequence[]
        /// </summary>
        /// <returns>A random number</returns>
        private int GetRandomNumber()
        {
            Random rnd = new Random();
            int nNumber;

            Func<int, bool> Contains = (value) =>
            {
                for (int i = 0; i < _nSequence.Length; i++)
                {
                    if (_nSequence[i] == value) return true;
                }
                return false;
            };

            while (true)
            {
                nNumber = rnd.Next(_nMinDigit, _nMaxDigit + 1);
                if (!Contains(nNumber)) break;
                
            }

            return nNumber;
        }

        /// <summary>
        /// Checks the button value clicked by the user and fires the corresponding event if necessary
        /// </summary>
        /// <param name="value">Number of the button clicked</param>
        public bool Check(int value)
        {
            // First check if the value clicked is correct
            if (_nSequence[_nSequenceIndex] != value)
            {
                OnWrongSequence(new WrongEventArgs(_nSequenceLength - 2));
                return false;
            }
            
            // Increase the counter whithin the _nSequence
            _nSequenceIndex++;

            // Check if this is the last button, i.e. the last value of _nSequence
            if (_nSequenceIndex > _nSequence.Length-1)
            {
                //_nSequenceLength++;
                OnCorrectSequence(new CorrectEventArgs(_nSequenceLength));
                return false;
            }


            /*
            if (_nGameScore>(_nMinDigit+_nMaxDigit+1))
            {
                OnGameOver(new OverEventArgs(_nGameScore - 1));
                ReSet();
            }
            */
            // If we get here, it means the player guessed correctly and that there are still numbers left in the sequence
            return true;
        }

    }
}
