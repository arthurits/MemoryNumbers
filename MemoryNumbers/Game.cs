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
        private int _nSequenceLength = 0;   // It's the current length of the _nSequence array
        private int _nSequenceIndex = 0;    // index (button clicked) whithin the _nSequence array
        private int _nMaxAttempts = 10;
        private int _nCurrAttempt = 0;      // The accumulated number of attempts
        private int _nMinLength = 2;        // The minimum length of the initial _nSequence array
        private int[] _nSequence;
        private PlayMode _playMode;         // The current play mode
        private int _nTime;
        private int _nTimeIncrement;
        private int _nTotalTime;
        private List<(int Number, int Total, int Correct)> _listStats;
        private List<List<double>> _listTimes;

        #endregion Private variables

        #region Public properties
        /// <summary>
        /// Maximum attempts allowed to the player.
        /// Only positive numbers > 0. Default is 10.
        /// </summary>
        [Description("Maximum attempts (minimum 1 attempt)"),
        Category("Game properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int MaximumAttempts { get => _nMaxAttempts; set => _nMaxAttempts = value < 1 ? 1 : value; }

        /// <summary>
        /// Minimum length of the initial sequence: minimum numbers of digits to display at the begining of the game.
        /// Only positive numbers > 0. Default is 2.
        /// </summary>
        [Description("Minimum initial length (minimum 1 digit)"),
        Category("Game properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int MinimumLength { get => _nMinLength; set => _nMinLength = value < 1 ? 1 : value; }

        /// <summary>
        /// Maximum excluded number (digit) of the sequence.
        /// Only positive numbers > 0 and >= MinimumDigit. Default is 10.
        /// </summary>
        [Description("Maximum excluded number (digit) of the sequence"),
        Category("Game properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int MaximumDigit { get => _nMaxDigit; set => _nMaxDigit = value < _nMinDigit ? _nMinDigit : value; }

        /// <summary>
        /// Minimum included number (digit) of the sequence.
        /// Only positive numbers >= 0. Default is 0.
        /// </summary>
        [Description("Minimum initial length (minimum 1 digit)"),
        Category("Game properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int MinimumDigit { get => _nMinDigit; set => _nMinDigit = value < 0 ? 0 : value; }

        /// <summary>
        /// Current score of the game: the length of the sequence correctly guessed by the player (the length of the array).
        /// Only positive numbers >= 0.
        /// </summary>
        [Description("Current score (length og the numeric sequence"),
        Category("Game properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int CurrentScore { get => _nSequenceLength; set => _nSequenceLength = value < 0 ? 0 : value; }

        /// <summary>
        /// Gets the numeric array containing the current sequence (read only).
        /// Its length corresponds to the CurrentScore property.
        /// </summary>
        [Description("Gets the numeric sequence to represent (read only)"),
        Category("Game properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int[] GetSequence { get => _nSequence; }

        /// <summary>
        /// Index (button clicked) whithin the _nSequence array.
        /// </summary>
        [Description("Current score (length og the numeric sequence"),
        Category("Game properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int GetCurrentAttempt { get => _nCurrAttempt; }

        /// <summary>
        /// Index (button clicked) whithin the _nSequence array.
        /// </summary>
        [Description("Current score (length og the numeric sequence"),
        Category("Game properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int GetSequenceIndex { get => _nSequenceIndex; }

        /// <summary>
        /// The actual play-mode selected by the user (time and sequence mode).
        /// </summary>
        [Description("The actual play-mode selected by the user (time and sequence mode)"),
        Category("Game properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public PlayMode PlayMode { get => _playMode; set => _playMode = value; }

        /// <summary>
        /// The time (ms) each sequence is shown to the player.
        /// </summary>
        [Description("The time (ms) each sequence is shown to the player"),
        Category("Game properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int Time { get => _nTime; set => _nTime = value < 0 ? 0 : value; }

        /// <summary>
        /// The time increment (ms) after each correct sequence.
        /// </summary>
        [Description("The time increment (ms) after each correct sequence"),
        Category("Game properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int TimeIncrement { get => _nTimeIncrement; set => _nTimeIncrement = value < 0 ? 0 : value; }

        /// <summary>
        /// The total time (ms) for showing the buttons. This may oscillate if the play mode is Time Incremental
        /// </summary>
        [Description("The total time (ms) for showing the buttons"),
        Category("Game properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int TimeTotal { get => _nTotalTime;}

        /// <summary>
        /// Gets the descriptive statistics (right, wrong percentages) regarding the buttons clicked by the player
        /// </summary>
        [Description("Gets the descriptive statistics (right, wrong percentages) regarding the buttons clicked by the player"),
        Category("Game properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public List<(int Number, int Total, int Correct)> GetStats { get => _listStats; }

        /// <summary>
        /// Gets the elapsed time (miliseconds) used to click each button/number in each attempt
        /// </summary>
        [Description("Gets the elapsed time (miliseconds) used to click each button/number in each attempt"),
        Category("Game properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public List<List<double>> GetStatsTime { get => _listTimes; }

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
            if (Tick is not null) Tick(this, e);
        }
        protected virtual void OnWrongSequence(WrongEventArgs e)
        {
            if (WrongSequence is not null) WrongSequence(this, e);
        }
        protected virtual void OnCorrectSequence(CorrectEventArgs e)
        {
            if (CorrectSequence is not null) CorrectSequence(this, e);
        }
        protected virtual void OnGameOver(OverEventArgs e)
        {
            if (GameOver is not null) GameOver(this, e);
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


        public Game()
        {
            //_nScore = 5;
        }

        public bool Start()
        {
            // Check we didn't reach the _nMaxAttempts limits and that the sequence length is whithin the allowed digits limits
            // The game is over if we are above the maximum number of attemps or
            // if the sequence is longer than the number of digits allowed
            if ((_nCurrAttempt >= _nMaxAttempts) || _nSequenceLength >= (_nMaxDigit - _nMinDigit + 1))
            {
                _nSequence = null;
                OnGameOver(new OverEventArgs(_nSequenceLength - 1));
                return false;
            }

            // Update variables
            _nSequenceLength++;     // Increment the length
            _nCurrAttempt++;        // Increment the current attempts counter
            _nSequenceIndex = 0;    // Restart the index pointing to the first element of _nSequence
            _listTimes.Add(new List<double>()); // Add a new List for storing the times

            // Generate the numeric sequence
            return SetSequence();
        }

        public void ReSet()
        {
            _nSequenceLength = _nMinLength - 1;
            _nSequenceIndex = 0;
            _nCurrAttempt = 0;
            _nSequence = null;
            _nTotalTime = _nTime;
            _listStats = new List<(int, int, int)>();
            _listTimes = new List<List<double>>();
        }

        /// <summary>
        /// Generates a numeric secuence
        /// </summary>
        private bool SetSequence()
        {
            // Create and fill the array depending on the current playmode
            _nSequence = new int[_nSequenceLength];
            if ((_playMode & PlayMode.SequenceRandom) == PlayMode.SequenceRandom)
            {
                for (int i = 0; i < _nSequenceLength; i++)
                {
                    _nSequence[i] = _nMinDigit - 1;
                    _nSequence[i] = GetRandomNumber();
                }
                Array.Sort(_nSequence); // This is not necessary since the numbers would be random scattered in the board control
            }
            else if ((_playMode & PlayMode.SequenceProgressive) == PlayMode.SequenceProgressive)
            {
                for (int i = 0; i < _nSequenceLength; i++)
                {
                    _nSequence[i] = _nMinDigit + i;
                }
            }

            foreach (int i in _nSequence)
                if (_listStats.FindIndex(x => x.Number == i) == -1) _listStats.Add((i, 0, 0));

            return true;
        }

        public void Sequence(int length)
        {
            if (length >= _nMaxAttempts) length = 1;

            _nMaxAttempts++;

            int nLength = _nSequence == null ? 0 : _nSequence.Length;
            int nNewLength = length + 1;
            Random rnd = new();

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
            Random rnd = new();
            int nNumber;

            bool Contains(int value)
            {
                for (int i = 0; i < _nSequence.Length; i++)
                {
                    if (_nSequence[i] == value) return true;
                }
                return false;
            }

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
        public bool Check(int value, double seconds)
        {
            //int index = _listStats.FindIndex(x => x.Number == _nSequence[_nSequenceIndex]);
            int index = _listStats.FindIndex(x => x.Number == value);
            _listTimes[_nCurrAttempt - 1].Add(seconds);

            // First check if the value clicked is wrong
            if (_nSequence[_nSequenceIndex] != value)
            {
                // If the playmode is Time Incremental, reduce the total time
                if ((_playMode & PlayMode.TimeIncremental) == PlayMode.TimeIncremental)
                    _nTotalTime -= _nTimeIncrement;

                //System.Diagnostics.Debug.WriteLine(index.ToString() + " — " + _nSequence[_nSequenceIndex].ToString() + " — " + value.ToString());
                if (index !=-1) _listStats[index] = (_listStats[index].Number, _listStats[index].Total+1, _listStats[index].Correct);

                OnWrongSequence(new WrongEventArgs(_nSequenceLength - 2));

                return false;
            }
            
            // Increase the counter whithin the _nSequence
            _nSequenceIndex++;
            if (index != -1) _listStats[index] = (_listStats[index].Number, _listStats[index].Total + 1, _listStats[index].Correct + 1);

            // Check if this is the last button, i.e. the last available value in the _nSequence array
            if (_nSequenceIndex > _nSequence.Length-1)
            {
                // If the playmode is Time Incremental, increase the total time
                if ((_playMode & PlayMode.TimeIncremental) == PlayMode.TimeIncremental)
                    _nTotalTime += _nTimeIncrement;

                //_nSequenceLength++;
                OnCorrectSequence(new CorrectEventArgs(_nSequenceLength));

                return false;
            }

            // The OnGameOver is checked at the Start function. It could be also implemented here

            // If we get here, it means the player guessed correctly and that there are still numbers left in the sequence
            return true;
        }

    }
}
