using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MemoryNumbers
{
    public class Game
    {
        #region Private variables

        private int _nMaxDigit = 10;
        private int _nMinDigit = 0;
        private int _nScore = 0;
        private int _nSubScore = 0;
        private int _nMaxAttempts = 10;
        private int _nCurrAttempt = 0;
        private int _nMinLength = 2;
        private int[] _nSequence;

        private enum PlayMode
        {
            TimeFixed = 1,
            TimeIncremental = 2,
            SequenceAscending = 4,
            SequenceRandom = 8
        }

        #endregion Private variables

        #region Public properties

        [Description("Maximum attempts (minimum 1 attempt)"),
        Category("Sequence properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int MaximumAttempts { get => _nMaxAttempts; set => _nMaxAttempts = value < 1 ? 1 : value; }

        [Description("Minimum digits (minimum 1 digit)"),
        Category("Sequence properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int MinimumLength { get => _nMinLength; set => _nMinLength = value < 1 ? 1 : value; }

        public int MaxScore { get => _nMaxDigit; set => _nMaxDigit = value < _nMinDigit ? _nMinDigit : value; }
        public int MinScore { get => _nMinDigit; set => _nMinDigit = value < 0 ? 0 : value; }
        public int CurrentScore { get => _nScore; set => _nScore = value < 0 ? 0 : value; }

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
            return SetSequence();
        }

        public void ReSet()
        {
            _nScore = 0;
            _nSubScore = 0;
            _nCurrAttempt = 0;
            _nSequence = null;
        }

        /// <summary>
        /// Generates a numeric secuence
        /// </summary>
        private bool SetSequence()
        {
            // Check we didn't reach the _nMaxAttempts limits and that we are whithin the allowed digits limits
            int nArrayLength = _nScore + 1;
            if((_nCurrAttempt > _nMaxAttempts) || nArrayLength > (_nMaxDigit - _nMinDigit))
            {
                _nSequence = null;
                return false;
            }
            
            // Else, create and fill the array
            _nSequence = new int[nArrayLength];
            for (int i = 0; i < nArrayLength; i++)
            {
                _nSequence[i] = _nMinDigit - 1;
                _nSequence[i] = GetRandomNumber();
            }
            Array.Sort(_nSequence); // This is not necessary since the number would be random scattered in the board control

            // Increment the current attemp counter            
            _nCurrAttempt++;
       
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
            _nSubScore = 0;

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
                nNumber = rnd.Next(_nMinDigit, _nMaxDigit);
                if (!Contains(nNumber)) break;
                
            }

            return nNumber;
        }

        public void Check(int value)
        {
            if (_nSequence[_nSubScore] != value) OnWrongSequence(new WrongEventArgs(value));
            else _nSubScore++;

            if (_nSubScore > _nScore)
            {
                _nScore++;
                if (CorrectSequence != null) OnCorrectSequence(new CorrectEventArgs(_nScore));
            }

            if (_nScore>(_nMinDigit+_nMaxDigit+1))
            {
                if (GameOver != null) OnGameOver(new OverEventArgs(_nScore - 1));
                ReSet();
            }

            return;
        }

    }
}
