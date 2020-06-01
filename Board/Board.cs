using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Svg;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace Controls
{
    //[Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(System.ComponentModel.Design.IDesigner))]
    public partial class Board: PictureBox
    {
        #region Private variables

        // Internal variables to place the buttons on the board
        internal const int nMaxPartialAttempts = 12;
        internal const int nMaxTotalAttempts = 250;

        // Controls contained inside the control
        private readonly Controls.CountDown countDown;
        private readonly System.Windows.Forms.PictureBox pctCorrect;
        private readonly System.Windows.Forms.PictureBox pctWrong;
        private Controls.RoundButton[] _roundButton;

        // Internal controls
        private readonly System.Media.SoundPlayer[] _soundPlayer;
        private readonly Svg.SvgDocument _svgCorrect = null;
        private readonly Svg.SvgDocument _svgWrong = null;

        // Internal variables
        private string _path;                       // Path of the executable
        private DateTime _nowStart;
        private DateTime _nowEnd;
        //private int[] _nSequence;
        private int _nSequenceCounter = 0;
        private int _nSequenceLength = 0;
        //private int _nButtons = 10;
        private int _nDiameter = 1;
        private int _nMinDimension = 0;
        //private int _nMaxNum = 9;
        //private int _nMinNum = 0;
        private int _nTime = 700;
        //private int _nTimeIncrement = 300;
        private float _fBorderWidth = 0.12f;
        private float _fCountDownFactor = 0.37f;
        private float _fNumbersFactor = 0.25f;
        private float _fResultFactor = 0.56f;
        private float _fFontFactor = 0.60f;
        private Color _cBorderColor = Color.Black;
        private Color _cBackColor = Color.White;
        private bool _sound = true;
        
        private enum AudioSoundType
        {
            NumberCorrect,
            NumberWrong,
            SequenceCorrect,
            SequenceWrong,
            CountDown,
            EndGame
        }
        private enum AudioSoundMode
        {
            Sync,
            Async
        }

        #endregion Private variables

        #region Public properties

        /// <summary>
        /// Diameter of the corners of the button. For a perfect circunference, this should be 1/2 of the heigth/width.
        /// For a perfect rectangle, this should be 0
        /// </summary>
        [Description("Diameter of the circunference (0 means rectangle)"),
        Category("Digit button properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int Diameter
        {
            get { return _nDiameter; }
            set { _nDiameter = value < 0 ? 0 : value; Invalidate(); }
        }
 
        /// <summary>
        /// The time interval (miliseconds) for flashing the sequence to the player
        /// </summary>
        [Description("Time flashing the digits (must be >=0)"),
        Category("Digit button properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int Time
        {
            get { return _nTime; }
            set { _nTime = value < 0 ? 0 : value; }
        }

        /// <summary>
        /// Ratio of the width with respect to the heigth/width of the button
        /// </summary>
        [Description("Width of the border (0 means no border)"),
        Category("Digit button properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public float BorderRatio
        {
            get { return _fBorderWidth; }
            set
            {
                _fBorderWidth = value < 0 ? 0f : value;
                CountDownUpdate();
            }
        }

        /// <summary>
        /// Ratio of the heigth/width of the coun-down control with respect to the heigth/width of the board
        /// </summary>
        [Description("Ratio of the count down control"),
        Category("Digit button properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public float CountDownRatio
        {
            get { return _fCountDownFactor; }
            set
            {
                _fCountDownFactor = value < 0 ? 0f : value;
                CountDownUpdate();
            }
        }

        /// <summary>
        /// Ratio of the heigth/width of the button with respect to the heigth/width of the board
        /// </summary>
        [Description("Ratio of the digits control"),
        Category("Digit button properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public float NumbersRatio
        {
            get { return _fNumbersFactor; }
            set
            {
                _fNumbersFactor = value < 0 ? 0f : value;
                _nMinDimension = Math.Min(this.Width, this.Height);
                _nDiameter = (int)(_nMinDimension * _fNumbersFactor);
            }
        }

        /// <summary>
        /// Ratio of the heigth/width of the results picture with respect to the heigth/width of the board
        /// </summary>
        [Description("Ratio of the picture-result controls"),
        Category("Digit button properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public float ResultRatio
        {
            get { return _fResultFactor; }
            set
            {
                _fResultFactor = value < 0 ? 0f : value;
                PictureBoxResultUpdate();
            }
        }

        /// <summary>
        /// Ratio of the size of the button with respect to the heigth/width of the button
        /// </summary>
        [Description("Ratio of the font for the round controls (CountDown and Digits)"),
        Category("Digit button properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public float FontRatio
        {
            get { return _fFontFactor; }
            set
            {
                _fFontFactor = value < 0 ? 0f : value;
                CountDownUpdate();
            }
        }

        /// <summary>
        /// Border color of the circunference of the button
        /// </summary>
        [Description("Border color of the circunference"),
        Category("Digit button properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BorderColor
        {
            get { return _cBorderColor; }
            set
            {
                _cBorderColor = value;
                countDown.BorderColor = _cBorderColor;
            }
        }

        /// <summary>
        /// Back color of the circunference
        /// </summary>
        [Description("Back color of the circunference"),
        Category("Digit button properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color FillColor
        {
            get { return _cBackColor; }
            set { _cBackColor = value; Invalidate(); }
        }

        /// <summary>
        /// Specifies whether game sounds are played (true) or not (false)
        /// </summary>
        [Description("Back color of the circunference"),
        Category("Specifies whether game sounds are played (true) or not (false)"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool PlaySounds
        {
            get { return _sound; }
            set { _sound = value; countDown.PlaySounds = value; }
        }
        #endregion Public properties

        #region Events
        public event EventHandler<Board.ButtonClickEventArgs> ButtonClick;
        protected virtual void OnButtonClick(Board.ButtonClickEventArgs e)
        {
            //if (ButtonClick != null) ButtonClick(this, e);
            ButtonClick?.Invoke(this, e);
        }
        public class ButtonClickEventArgs : EventArgs
        {
            public readonly int ButtonValue;
            public readonly double Seconds;
            public ButtonClickEventArgs(int button, double seconds)
            {
                ButtonValue = button;
                Seconds = seconds;
            }
        }

        // Async events https://stackoverflow.com/questions/12451609/how-to-await-raising-an-eventhandler-event
        // https://github.com/Microsoft/SimpleStubs/issues/35
        // https://codereview.stackexchange.com/questions/133464/use-of-async-await-for-eventhandlers
        // https://stackoverflow.com/questions/1916095/how-do-i-make-an-eventhandler-run-asynchronously
        #endregion Events

        public Board()
        {
            // WinForms automatic initialization
            InitializeComponent();
            // this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
            this.BorderStyle = BorderStyle.FixedSingle;
            
            // Load the sounds
            _path = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            _soundPlayer = new System.Media.SoundPlayer[Enum.GetNames(typeof(AudioSoundType)).Length];  // https://stackoverflow.com/questions/856154/total-number-of-items-defined-in-an-enum
            _soundPlayer[0] = System.IO.File.Exists(_path + @"\audio\Correct number.wav") ? new System.Media.SoundPlayer(_path + @"\audio\Correct number.wav") : null;
            _soundPlayer[1] = System.IO.File.Exists(_path + @"\audio\Wrong number.wav") ? new System.Media.SoundPlayer(_path + @"\audio\Wrong number.wav") : null;
            _soundPlayer[2] = System.IO.File.Exists(_path + @"\audio\Correct sequence.wav") ? new System.Media.SoundPlayer(_path + @"\audio\Correct sequence.wav") : null;
            _soundPlayer[3] = System.IO.File.Exists(_path + @"\audio\Wrong sequence.wav") ? new System.Media.SoundPlayer(_path + @"\audio\Wrong sequence.wav") : null;
            _soundPlayer[4] = System.IO.File.Exists(_path + @"\audio\Count down.wav") ? new System.Media.SoundPlayer(_path + @"\audio\Count down.wav") : null;
            _soundPlayer[5] = System.IO.File.Exists(_path + @"\audio\End game.wav") ? new System.Media.SoundPlayer(_path + @"\audio\End game.wav") : null;

            // Set the array of buttons to 0 elements
            _roundButton = new Controls.RoundButton[0];
            
            // Set the CountDown control
            countDown = new Controls.CountDown()
            {
                //Anchor = AnchorStyles.None,
                BorderColor = System.Drawing.Color.Black,
                BackColor = Color.Transparent,
                BorderWidth = 4F,
                EndingTime = 0F,
                FillColor = System.Drawing.Color.Transparent,
                Location = new System.Drawing.Point(0, 0),
                Name = "CountDown",
                Parent = this,
                PlaySounds = false,
                RegionOffset = 1f,
                Size = new System.Drawing.Size(100, 100),
                StartingTime = 3F,
                TabIndex = 0,
                //Text = "3",
                TimeInterval = 1000D,
                Visible = false,
                VisibleBorder = true,
                VisibleText = true,
                xRadius = 50F,
                yRadius = 50F
            };
            this.countDown.TimerEnding += new EventHandler<TimerEndingEventArgs>(this.OnCountDownEnding);
            this.Controls.Add(countDown);

            // Set the PictureBoxes
            // https://stackoverflow.com/questions/53832933/fade-ws-ex-layered-form
            pctCorrect = new System.Windows.Forms.PictureBox()
            {
                Anchor = AnchorStyles.None,
                BackColor = Color.Transparent,
                BackgroundImageLayout = ImageLayout.Stretch,
                Dock = DockStyle.None,
                Parent = this,
                Visible = false
            };
            pctWrong = new System.Windows.Forms.PictureBox()
            {
                Anchor = AnchorStyles.None,
                BackColor = Color.Transparent,
                BackgroundImageLayout = ImageLayout.Stretch,
                Dock = DockStyle.None,
                Parent = this,
                Visible = false
            };
            this.Controls.Add(pctCorrect);
            this.Controls.Add(pctWrong);

            // Read the SVG files. This is done here because it takes some 0.2 - 0.3 seconds each to read
            // This way we avoid any possible bottle neck when overriding OnResize
            if (System.IO.File.Exists(_path + @"\images\Sequence correct.svg"))
                _svgCorrect = SvgDocument.Open(_path + @"\images\Sequence correct.svg");
            if (System.IO.File.Exists(_path + @"\images\Sequence wrong.svg"))
                _svgWrong = SvgDocument.Open(_path + @"\images\Sequence wrong.svg");
        }
        
        // https://stackoverflow.com/questions/4446478/how-do-i-create-a-colored-border-on-a-picturebox-control
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, Color.Black, ButtonBorderStyle.Solid);
        }

        // https://docs.microsoft.com/en-us/dotnet/framework/winforms/automatic-scaling-in-windows-forms
        protected override void OnResize(EventArgs e)
        {            
            base.OnResize(e);
            
            //this.SuspendLayout();
            _nMinDimension = Math.Min(this.Width, this.Height);
            _nDiameter = (int)(_nMinDimension * _fNumbersFactor);

            var form = FindForm();
            if (form == null) return;
            if (form.WindowState == FormWindowState.Minimized) return;

            // Update the controls if the board is not shrunk
            if (_nMinDimension != 0)
            {
                //if (countDown != null) CountDownUpdate();
                //if (pctCorrect != null) PictureBoxResultUpdate();
                if (countDown != null && pctCorrect != null)
                    ResizeChildControls();
            }
            
            //base.OnResize(e);
            //this.ResumeLayout(true);
            //this.PerformLayout();
        }

        public void ResizeChildControls()
        {
            PictureBoxResultUpdate();
            CountDownUpdate();
        }

        private void CountDownUpdate()
        {
            if (countDown != null)
            {
                this.countDown.BorderWidth = ((_nMinDimension * _fCountDownFactor - 1) / 2) * _fBorderWidth;
                this.countDown.xRadius = (_nMinDimension * _fCountDownFactor) / 2;
                this.countDown.yRadius = (_nMinDimension * _fCountDownFactor) / 2;
                this.countDown.Size = new Size((int)(_nMinDimension * _fCountDownFactor), (int)(_nMinDimension * _fCountDownFactor));
                this.countDown.Location = new System.Drawing.Point((this.Size.Width - countDown.Size.Width) / 2, (this.Size.Height - countDown.Size.Height) / 2);
                this.countDown.Font = new Font(countDown.Font.FontFamily, _fFontFactor * (countDown.Size.Height - 2 * countDown.BorderWidth));
                // countDown.Invalidate();
            }
        }

        private async Task PictureBoxResultUpdate()
        {
            if (pctCorrect != null && pctWrong != null)
            {
                Bitmap bmpCorrect = null;
                Region rgCorrect = null;
                Bitmap bmpWrong = null;
                Region rgWrong = null;
                
                this.pctCorrect.Size = new Size((int)(_nMinDimension * _fResultFactor), (int)(_nMinDimension * _fResultFactor));
                this.pctCorrect.Location = new System.Drawing.Point((this.Size.Width - pctCorrect.Size.Width) / 2, (this.Size.Height - pctCorrect.Size.Height) / 2);
                
                this.pctWrong.Size = new Size((int)(_nMinDimension * _fResultFactor), (int)(_nMinDimension * _fResultFactor));
                this.pctWrong.Location = new System.Drawing.Point((this.Size.Width - pctWrong.Size.Width) / 2, (this.Size.Height - pctWrong.Size.Height) / 2);

                bmpCorrect = _svgCorrect.Draw(this.pctCorrect.Width, this.pctCorrect.Height);
                bmpWrong = _svgWrong.Draw(this.pctWrong.Width, this.pctWrong.Height);

                //bmpCorrect = DrawSVG(_svgCorrect, this.pctCorrect.Width, this.pctCorrect.Height);
                //bmpWrong = DrawSVG(_svgWrong, this.pctWrong.Width, this.pctWrong.Height);

                System.Diagnostics.Debug.WriteLine("Mark stamp #1");
                await Task.Run(() =>
                {
                    System.Diagnostics.Debug.WriteLine("Mark stamp #2");
                    rgCorrect = new Region(GetRegionFromTransparentBitmap(bmpCorrect));
                    rgWrong = new Region(GetRegionFromTransparentBitmap(bmpWrong));
                    System.Diagnostics.Debug.WriteLine("pctWrong size {0}x{1}", pctWrong.Width, pctWrong.Height);
                    System.Diagnostics.Debug.WriteLine("bmpWrong size {0}x{1}", bmpWrong.Width, bmpWrong.Height);
                    System.Diagnostics.Debug.WriteLine("Mark stamp #3");

                    this.pctCorrect.Image = bmpCorrect;
                    if (this.pctCorrect.InvokeRequired) this.pctCorrect.Invoke((Action)(() => this.pctCorrect.Region = rgCorrect));
                    else this.pctCorrect.Region = rgCorrect;
                    this.pctWrong.Image = bmpWrong;
                    if (this.pctWrong.InvokeRequired) this.pctWrong.Invoke((Action)(() => this.pctWrong.Region = rgWrong));
                    else this.pctWrong.Region = rgWrong;
                });
                System.Diagnostics.Debug.WriteLine("Mark stamp #4");
                

            }

        }

        public async Task<bool> Start(int[] numbers, int Time)
        {
            /*
            new System.Threading.Thread(() =>
            {
                CreateButtons(numbers);
            }).Start();
            */
            // Task.Run(() => this.Invoke((new Action(() => CreateButtons(numbers)))));
            
            _nTime = Time;
            
            this.pctWrong.Visible = false;
            this.pctCorrect.Visible = false;

            Task<bool> CreateButtonsTask = CreateButtons(numbers);
            // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/;

            await Task.Delay(400);

            countDown.StartingTime = 3;
            countDown.Visible = true;
            countDown.Start();
            //countDown.Visible = false;
            
            bool result = await CreateButtonsTask;

            return result;
        }

        // https://stackoverflow.com/questions/2367718/automating-the-invokerequired-code-pattern
        private void OnCountDownEnding(object sender, TimerEndingEventArgs e)
        {
            // This event is fired by a System.Timer, which runs on a separated thread (not the GUI thread)
            // so we need to invoque in order to modify the GUI
            if (this.InvokeRequired)
            {
                //this.Invoke(new Action(() => OnCountDownEnding(sender, e)));
                this.Invoke(new Action(() => ShowButtons(sender)));
            }
            else
            {
                ShowButtons(sender);
            }
            
        }
        
        /// <summary>
        /// Show the buttons, wait Time (in ms), and hide only the digits
        /// </summary>
        /// <param name="sender"></param>
        private async void ShowButtons(object sender)
        {
            ((CountDown)sender).Visible = false;
            foreach (RoundButton btn in _roundButton)
            {
                btn.Visible = true;
            }
            await Task.Delay(_nTime);
            foreach (RoundButton btn in _roundButton)
            {
                btn.VisibleText = false;
                btn.VisibleBorder = true;
            }
            _nowStart = DateTime.Now;
        }

        public async Task<bool> CreateButtons (int[] numbers)
        {
            bool bIntersection = false;
            bool result = true;
            Random rnd = new Random();
            Region regTotal = new Region();
            Region regIntersec = new Region();
            Graphics g = this.CreateGraphics();
            _nSequenceLength = numbers.Length;

            int nPartialAttempts = 0;
            int nTotalAttempts = 0;
            
            regTotal.MakeEmpty();
            //reg1.Union(this.Region);

            // Delete previous buttons if any
            this.SuspendLayout();
            DeleteButtons();
            _roundButton = new Controls.RoundButton[numbers.Length];
            
            // Create the buttons
            for (int i = 0; i < _nSequenceLength; i++)
            {
                _roundButton[i] = new Controls.RoundButton
                {
                    Parent = this,
                    FillColor = _cBackColor,
                    BorderColor = _cBorderColor,
                    BorderWidth = (_nDiameter - 1f) / 2f * _fBorderWidth, // The RoundButton.cs defines the rectangle as height-1 and width-1
                    Size = new Size(_nDiameter, _nDiameter),
                    xRadius = _nDiameter / 2f,
                    yRadius = _nDiameter / 2f,
                    RegionOffset = 1f,
                    Text = numbers[i].ToString(),
                    VisibleBorder = false,
                    Visible = false
                };
                _roundButton[i].Font = new Font(_roundButton[i].Font.FontFamily, _fFontFactor * _nDiameter);

                nPartialAttempts = 0;
                do
                {
                    regIntersec = regTotal.Clone();
                    _roundButton[i].Location = new Point(rnd.Next(0, this.Width - _nDiameter), rnd.Next(0, this.Height - _nDiameter));
                    regIntersec.Intersect(_roundButton[i].Bounds);
                    bIntersection = regIntersec.IsEmpty(g);
                    nPartialAttempts++;
                    nTotalAttempts++;

                } while (!bIntersection && nPartialAttempts <= nMaxPartialAttempts && nTotalAttempts <= nMaxTotalAttempts);

                // Different scenarios
                if (bIntersection)      // The new button does not overlap the previous ones
                {
                    _roundButton[i].ButtonClick += new EventHandler<RoundButton.ButtonClickEventArgs>(this.ButtonClicked);
                    this.Controls.Add(_roundButton[i]);
                    regTotal.Union(_roundButton[i].Bounds);
                }
                else if (!bIntersection && nTotalAttempts <= nMaxTotalAttempts)     // The new button overlaps but we still have more attempts avaliable
                {
                    if ((i - 2) >= 0)   // If the 3rd button (or above) could not be placed, then delete the previous one (go back two buttons)
                    {
                        regTotal.Exclude(_roundButton[i - 1].Bounds);
                        i -= 2;
                    }
                    else   // We get here only when the 2nd button could not be placed, so just try again
                    {
                        i--;
                    }
                }
                else if (!bIntersection && nTotalAttempts > nMaxTotalAttempts)     // The new button overlaps and we have run out of attempts
                {
                    result = false;
                }

            }
            this.ResumeLayout();

            // Set index pointer to the array's beginning
            _nSequenceCounter = 0;

            // Clean up
            regTotal.Dispose();
            regIntersec.Dispose();
            g.Dispose();

            return result;
        }

        /// <summary>
        /// Play the "number correct" audio
        /// </summary>
        /// <returns></returns>
        public async void ButtonRight()
        {
            await PlayAudioFile(AudioSoundType.NumberCorrect, AudioSoundMode.Async);
        }

        /// <summary>
        /// Play the audio and show the WRONG result picture
        /// </summary>
        /// <returns></returns>
        public async Task ButtonWrong()
        {
            await PlayAudioFile(AudioSoundType.NumberWrong, AudioSoundMode.Sync);
            pctWrong.Visible = true;
            pctWrong.Update();
            Task taskError = ShowError(_nSequenceCounter);
            await PlayAudioFile(AudioSoundType.SequenceWrong, AudioSoundMode.Async);

            await Task.Delay(100);  // We wait before showing the MessageBox
            await taskError;
        }

        /// <summary>
        /// Play the audio and show the OK result picture
        /// </summary>
        /// <returns></returns>
        public async Task SequenceRight()
        {
            await PlayAudioFile(AudioSoundType.NumberCorrect, AudioSoundMode.Sync);
            pctCorrect.Visible = true;
            pctCorrect.Update();
            await PlayAudioFile(AudioSoundType.SequenceCorrect, AudioSoundMode.Sync);
        }

        /// <summary>
        /// Dispose the numeric buttons and hide any picture results
        /// </summary>
        public void ClearBoard()
        {
            countDown.Stop();
            countDown.Visible = false;
            if (pctCorrect.Visible)
                pctCorrect.Visible = false;
            if (pctWrong.Visible)
                pctWrong.Visible = false;
            DeleteButtons();
        }

        private async void ButtonClicked(object sender, RoundButton.ButtonClickEventArgs e)
        {
            //Application.DoEvents();
            // Change the state of the clicked button
            var roundButton = (RoundButton)sender;
            roundButton.VisibleBorder = false;
            roundButton.VisibleText = true;
            roundButton.Update();
            //_roundButton[_nSequenceCounter].VisibleBorder = false;

            _nowEnd = DateTime.Now;
            double SecondsElapsed = (_nowEnd.Subtract(_nowStart)).TotalSeconds;
            _nowStart = _nowEnd;
            OnButtonClick(new ButtonClickEventArgs(e.ButtonValue, SecondsElapsed));

        }

        private async Task ShowError(int sequenceError)
        {
            this.SuspendLayout();
            while (sequenceError < _roundButton.Length)
            {
                _roundButton[sequenceError].VisibleText = true;
                _roundButton[sequenceError].VisibleBorder = false;
                sequenceError++;
            }
            this.ResumeLayout();
            //this.PerformLayout();
            return;
        }

        private void DeleteButtons()
        {
            // Delete previous buttons if any
            // https://stackoverflow.com/questions/4630391/get-all-controls-of-a-specific-type

            int i = 0;
            
            while (i < _roundButton.Length)
            {
                _roundButton[i].ButtonClick -= new EventHandler<RoundButton.ButtonClickEventArgs>(this.ButtonClicked);
                this.Controls.Remove(_roundButton[i]);
                _roundButton[i].Dispose();
                i++;
            }

        }

        /// <summary>
        /// Plays a game sound
        /// </summary>
        /// <param name="type">The type of the audio sound to play</param>
        /// <param name="mode">Whether the sound is played syncrounsly or asyncronously</param>
        /// <returns></returns>
        private async Task PlayAudioFile(AudioSoundType type, AudioSoundMode mode)
        {
            // If no sounds are to be played or the sound file wasn't loaded, then wait the corresponding miliseconds
            if (!_sound || _soundPlayer[(int)type] == null)
            {
                switch ((int)type)
                {
                    case 0:
                        await Task.Delay(600);
                        break;
                    case 1:
                        await Task.Delay(339);
                        break;
                    case 2:
                        await Task.Delay(548);
                        break;
                    case 3:
                        await Task.Delay(522);
                        break;
                    case 4:
                        await Task.Delay(522);
                        break;
                    case 5:
                        await Task.Delay(724);
                        break;
                }
                return;
            }           
            else    // Else, play the selected sound in the selected mode
            {
                if (mode == AudioSoundMode.Sync) _soundPlayer[(int)type].PlaySync();
                else if (mode == AudioSoundMode.Async) _soundPlayer[(int)type].Play();
            }

        }


        #region Private routines

        /// <summary>
        /// Opens a SVG file and returns a raster image
        /// </summary>
        /// <param name="path">Path of the SVG file</param>
        /// <param name="width">Width of the destination raster image</param>
        /// <param name="height">Height of the destination raster image</param>
        /// <returns>A System.Drawing.Bitmap object</returns>
        private System.Drawing.Bitmap GetBitmapFromSVG(string path, int width, int height)
        {
            Svg.SvgDocument document;
            System.Drawing.Bitmap svgBitmap = null;
            
            if (System.IO.File.Exists(path))
            {
                document = SvgDocument.Open(path);
                svgBitmap = document.Draw(width, height);
            }
            return svgBitmap;
        }

        private System.Drawing.Bitmap DrawSVG(Svg.SvgDocument document, int width, int height)
        {
            using (var bitmap = document.Draw(width, height))
            {
                return bitmap;
            }
        }

        /// <summary>
        /// Gets a region pixel by pixel from the non-transparent pixels of the bitmap
        /// More information here: https://www.codeproject.com/Articles/617613/Fast-pixel-operations-in-NET-with-and-without-unsa
        /// and here: https://www.codeproject.com/Articles/406045/Why-the-use-of-GetPixel-and-SetPixel-is-so-ineffic
        /// and here: // https://social.msdn.microsoft.com/Forums/office/en-US/5d8220b8-a7fa-4b49-8567-7a39da8f79b7/vb2010-how-can-i-make-the-picturebox-realy-transparent?forum=vbgeneral
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns>Path</returns>
        private System.Drawing.Drawing2D.GraphicsPath GetRegionFromTransparentBitmap (System.Drawing.Bitmap bitmap)
        {
            System.Drawing.Drawing2D.GraphicsPath regionPath = new System.Drawing.Drawing2D.GraphicsPath();

            // Check that we have a bitmap
            if (bitmap == null) return null;

            System.Drawing.Imaging.BitmapData imageData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                                                                            System.Drawing.Imaging.ImageLockMode.ReadWrite,
                                                                            bitmap.PixelFormat);

            int bytesPerPixel = (Image.GetPixelFormatSize(bitmap.PixelFormat)) >> 3;
            /*
            unsafe
            {
                byte* PixelComponent = (byte*)imageData.Scan0;
                byte* row = (byte*)imageData.Scan0;
                for (int j = 0; j < imageData.Height; j++)
                {
                    //byte* row = (byte*)imageData.Scan0 + (j * imageData.Stride);
                    for (int i = 0; i < imageData.Width; i++)
                    {
                        // row[0] = Blue, row[1] = Green, row[2] = Red, row[3] = Alpha,
                        if (row[i * bytesPerPixel + 3] > 0) region.AddRectangle(new Rectangle(i, j, 1, 1));
                    }
                    row += imageData.Stride;
                }
            }
            bitmap.UnlockBits(imageData);
            */


            // Marshal
            // This is a CPU-bound operation so we run it asyncronously on another thread
            // https://www.pluralsight.com/guides/using-task-run-async-await
            //await Task.Run(() =>
            //{

                byte[] imageBytes = new byte[Math.Abs(imageData.Stride) * bitmap.Height];
                IntPtr scan0 = imageData.Scan0;
                System.Runtime.InteropServices.Marshal.Copy(scan0, imageBytes, 0, imageBytes.Length);
                int row = 0;
                int col;
                for (int j = 0; j < imageData.Height; j++)
                {
                    col = 0;
                    for (int i = 0; i < imageData.Width; i++)
                    {
                        // Loop until we find a non transparent pixel
                        // https://www.codeguru.com/csharp/csharp/cs_misc/graphicsandimages/article.php/c4259/Converting-ColorKeyed-Bitmaps-to-Custom-Regions.htm
                        int i0 = i;
                        while((i < imageData.Width) && (imageBytes[(row + col) + 3] > 0))
                        {
                            i++;
                            col += bytesPerPixel;
                        }
                        regionPath.AddRectangle(new Rectangle(i0, j, i-i0, 1));
                        col += bytesPerPixel;
                    }
                    row += imageData.Stride;
                }
                System.Runtime.InteropServices.Marshal.Copy(imageBytes, 0, scan0, imageBytes.Length);
                
            //});

            bitmap.UnlockBits(imageData);



            /*
            // Another Marshal option. Slower than the previous
            System.Runtime.InteropServices.Marshal.Copy(scan0, imageBytes, 0, imageBytes.Length);
            int row = 0;
            int col = 0;
            for (int i = 0; i < imageBytes.Length; i += bytesPerPixel)
            {
                row = (int)(i / imageData.Stride);
                col = (i - fila * imageData.Stride) / bytesPerPixel;
                if (imageBytes[i + 3] > 0) region.AddRectangle(new Rectangle(row, col, 1, 1));
            }
            */

            /*
            // Slow for big images
            for (int j = 0; j < bitmap.Height; j++)
            {
                for (int i = 0; i < bitmap.Width; i++)
                {
                    if (bitmap.GetPixel(i, j).A > 0) region.AddRectangle(new Rectangle(i, j, 1, 1));
                }
            }
            */

            return regionPath; 
            
        }

        #endregion Private routines
    }

}

