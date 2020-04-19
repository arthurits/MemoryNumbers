using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

namespace Controls
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(System.ComponentModel.Design.IDesigner))]
    public partial class CountDown: Controls.RoundButton
    {
        private System.Timers.Timer t;
        private float _fStart = 10f;
        private float _fEnd = 0f;
        private double _dInterval = 1000; // 1 s
        private double _dCounter;
        private string _path;
        private System.Media.SoundPlayer _soundPlayer;

        public event EventHandler<TimerEndingEventArgs> TimerEnding;

        #region Public properties

        [Description("Starting time (generally seconds) of the countdown"),
        Category("Countdown properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public float StartingTime
        {
            get { return _fStart; }
            set { _fStart = value < 0 ? 0f : value; _dCounter = _fStart; this.Text = _fStart.ToString(); }
        }

        [Description("Ending time (generally seconds) of the countdown"),
        Category("Countdown properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public float EndingTime
        {
            get { return _fEnd; }
            set { _fEnd = value < 0 ? 0f : value; }
        }

        [Description("Time interval of the countdown in seconds"),
        Category("Countdown properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public double TimeInterval
        {
            get { return _dInterval; }
            set { _dInterval = value*1000 < 0 ? 0f : value; t.Interval = _dInterval; }
        }

        #endregion Public properties

        public CountDown()
        {
            InitializeComponent();

            // Timer initialization
            t = new System.Timers.Timer();
            t.Elapsed += onTimeEvent;

            _path = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            _soundPlayer = new System.Media.SoundPlayer(_path + @"\audio\Count down.wav");

            // Anchor the rounded rectangle for resizing purposes
            //roundButton1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //this.Region = roundButton1.Region;
            base.OnPaint(e);
        }

        private void onTimeEvent(object sender, ElapsedEventArgs e)
        {
            _dCounter -= _dInterval / 1000;

            if (_dCounter <= _fEnd)
            {
                //t.Stop();
                //System.Diagnostics.Debug.Write(_dCounter.ToString() + System.Environment.NewLine);
                if (TimerEnding != null) OnTimerEnding(this, new TimerEndingEventArgs());
                t.Stop();
            }
            else
            {
                _soundPlayer.Play();
                if (this.InvokeRequired)
                {
                    Invoke(new Action(() =>
                        {
                            this.Text = _dCounter.ToString();
                        //roundButton1.Text = _dCounter.ToString();
                    }));
                }
                else
                {
                    this.Text = _dCounter.ToString();
                }
            }
        }

        public void Start()
        {
            t.Start();
            _soundPlayer.Play();
            return;
        }

        public void Stop()
        {
            t.Stop();
            return;
        }

        // Events
        protected virtual void OnTimerEnding(object sender, TimerEndingEventArgs e)
        {
            if (TimerEnding != null) TimerEnding(this, e);
        }
    }

    /// <summary>
    /// Class to send the event data to the "listener"
    /// </summary>
    public class TimerEndingEventArgs : EventArgs
    {
        /*public readonly bool Flash;
        public readonly Int32 ButtonValue;
        public TimerEndingEventArgs(bool flash, Int32 button)
        {
            Flash = flash;
            ButtonValue = button;
        }*/
    }
}
