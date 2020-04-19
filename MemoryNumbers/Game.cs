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
        private int _nMaxScore = 10;
        private int _nMinScore = 0;
        private int _nScore = 0;
        private int _nSubScore = 0;
        private int _nMaxAttempts = 10;
        private int _nMinLength = 2;
        private int[] _nSequence;

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

        public int MaxScore { get => _nMaxScore; set => _nMaxScore = value < _nMinScore ? _nMinScore : value; }
        public int MinScore { get => _nMinScore; set => _nMinScore = value < 0 ? 0 : value; }

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

        public void Start()
        {
            SetSequence();
            return;
        }

        public void ReSet()
        {
            _nScore = 0;
            _nSubScore = 0;
            // Clear the array if it's not empty
            if (_nSequence.Length > 0) Array.Clear(_nSequence, 0, _nSequence.Length);
        }

        private void SetSequence()
        {
            int nLength = _nSequence == null ? 0 : _nSequence.Length;
            int nNewLength = _nScore + 1;
            Random rnd = new Random();

            // Clear the array if it's not empty
            if (nLength > 0) Array.Clear(_nSequence, 0, nLength);

            _nSequence = (new int[nNewLength]).Select(i => _nMaxScore).ToArray<int>();
            _nSubScore = 0;

            for (int i = 0; i < nNewLength; i++)
            {
                _nSequence[i] = _nMinScore - 1;
                _nSequence[i] = GetRandomNumber();
            }

            Array.Sort(_nSequence);

            return;
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

            _nSequence = (new int[nNewLength]).Select(i => _nMaxScore).ToArray<int>();
            _nSubScore = 0;

            for (int i = 0; i < nNewLength; i++)
            {
                _nSequence[i] = GetRandomNumber();
            }

            Array.Sort(_nSequence);

            return;
        }

        private int GetRandomNumber()
        {
            Random rnd = new Random();
            int val;
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
                val = rnd.Next(_nMinScore, _nMaxScore);
                if (!Contains(val)) break;
                
            }

            return val;
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

            if (_nScore>(_nMinScore+_nMaxScore+1))
            {
                if (GameOver != null) OnGameOver(new OverEventArgs(_nScore - 1));
                ReSet();
            }

            return;
        }

    }
}
