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

namespace Controls
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(System.ComponentModel.Design.IDesigner))]
    public partial class Board: PictureBox
    {
        #region Private variables

        private Controls.RoundButton[] _roundButton;
        private Controls.CountDown countDown;
        private System.Windows.Forms.PictureBox pctCorrect;
        private System.Windows.Forms.PictureBox pctWrong;
        private int[] _nSequence;
        private int _nSequenceCounter = 0;
        private int _nSequenceLength = 0;
        private int _nButtons = 10;
        private int _nDiameter = 45;
        private int _nMaxNum = 9;
        private int _nMinNum = 0;
        private int _nTime = 700;
        private int _nTimeIncrement = 300;
        private float _fBorderWidth = 1f;
        private float _fCountDownFactor = 0.37f;
        private float _fNumbersFactor = 0.25f;
        private float _fPictureCorrect = 0.56f;
        private float _fFontSize = 0.60f;
        private Color _cBorderColor = Color.Black;
        private Color _cBackColor = Color.Transparent;
        private string _path;
        private bool _sound;
        private System.Media.SoundPlayer[] _soundPlayer;
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
        /// Maximum number (excluded) for the buttons (must be >=0")
        /// </summary>
        [Description("Maximum value of the buttons (must be >=0"),
        Category("Digit button properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int MaxNumber
        {
            get { return _nMaxNum; }
            set { _nMaxNum = value < 0 ? 0 : value; }
        }

        /// <summary>
        /// Minimum number (included) for the buttons (must be >=0")
        /// </summary>
        [Description("Minimum value of the buttons (must be >=0)"),
        Category("Digit button properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int MinNumber
        {
            get { return _nMinNum; }
            set { _nMinNum = value < 0 ? 0 : value; }
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
        /// Provides a time increment (miliseconds) for the flashing interval that it's added to Time after each sequence is correct.
        /// If the sequence is incorrect, then this increment is substracted from Time
        /// </summary>
        [Description("Time-flashing increment after each sequence (must be >=0)"),
        Category("Digit button properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int TimeIncrement
        {
            get { return _nTimeIncrement; }
            set { _nTimeIncrement = value < 0 ? 0 : value; }
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
            set { _fBorderWidth = value < 0 ? 0f : value; Invalidate(); }
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
            set { _fCountDownFactor = value < 0 ? 0f : value; Invalidate(); }
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
            set { _fNumbersFactor = value < 0 ? 0f : value; Invalidate(); }
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
            get { return _fFontSize; }
            set { _fFontSize = value < 0 ? 0f : value; Invalidate(); }
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
            get { return _fPictureCorrect; }
            set { _fPictureCorrect = value < 0 ? 0f : value; Invalidate(); }
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
            set { _cBorderColor = value; Invalidate(); }
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
            if (ButtonClick != null) ButtonClick(this, e);
        }
        public class ButtonClickEventArgs : EventArgs
        {
            public readonly int ButtonValue;
            public ButtonClickEventArgs(int button) { ButtonValue = button; }
        }

        public event EventHandler<SequenceEventArgs> RightSequence;
        protected virtual void OnRightSequence(Board.SequenceEventArgs e)
        {
            if (RightSequence != null) RightSequence(this, e);
        }
        public class SequenceEventArgs : EventArgs
        {
            public readonly int SequenceLength;
            public SequenceEventArgs(int length) { SequenceLength = length; }
        }

        public event EventHandler<SequenceEventArgs> WrongSequence;
        protected virtual void OnWrongSequence(Board.SequenceEventArgs e)
        {
            if (WrongSequence != null) WrongSequence(this, e);
        }
        #endregion Events

        public Board()
        {
            // WinForms automatic initialization
            InitializeComponent();
            
            // Load the sounds
            _path = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            _soundPlayer = new System.Media.SoundPlayer[Enum.GetNames(typeof(AudioSoundType)).Length];                                              // https://stackoverflow.com/questions/856154/total-number-of-items-defined-in-an-enum
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
                    //BorderColor = System.Drawing.Color.Black,
                    BackColor = Color.Transparent,
                    BorderWidth = 4F,
                    EndingTime = 0F,
                    //FillColor = System.Drawing.Color.Transparent,
                    Name = "CountDown",
                    Parent = this,
                    Size = new System.Drawing.Size(100, 100),
                    StartingTime = 3F,
                    //TabIndex = 0,
                    //Text = "3",
                    TimeInterval = 1000D,
                    Visible = false,
                    VisibleBorder = true,
                    VisibleText = true,
                    xRadius = 50F,
                    yRadius = 50F
                };
            //this.countDown.Parent = this;
            this.countDown.TimerEnding += new EventHandler<TimerEndingEventArgs>(this.OnCountDownEnding);
            this.Controls.Add(countDown);
            
            // Set the PictureBoxes
            pctCorrect = new System.Windows.Forms.PictureBox()
            {
                BackColor = Color.Transparent,
                Parent = this,
                Visible = false
            };
            pctWrong = new System.Windows.Forms.PictureBox()
            {
                BackColor = Color.Transparent,
                Parent = this,
                Visible = false
            };
            this.Controls.Add(pctCorrect);
            this.Controls.Add(pctWrong);
        }

        // https://docs.microsoft.com/en-us/dotnet/framework/winforms/automatic-scaling-in-windows-forms
        protected override void OnResize(EventArgs e)
        {
            this.SuspendLayout();

            int minDimension = Math.Min(this.Width, this.Height);
            _nDiameter = (int)(minDimension * _fNumbersFactor);

            if (countDown != null)
            {
                this.countDown.xRadius = (minDimension * _fCountDownFactor) / 2;
                this.countDown.yRadius = (minDimension * _fCountDownFactor) / 2;
                this.countDown.Size = new Size((int)(minDimension * _fCountDownFactor), (int)(minDimension * _fCountDownFactor));
                this.countDown.Location = new System.Drawing.Point((this.Size.Width - countDown.Size.Width) / 2, (this.Size.Height - countDown.Size.Height) / 2);
                this.countDown.Font = new Font(countDown.Font.FontFamily, _fFontSize * (countDown.Size.Height - 2 * countDown.BorderWidth));
            }
            if (pctCorrect != null)
            {
                Bitmap bitmap = null;
                this.pctCorrect.Size = new Size((int)(this.Size.Height * _fPictureCorrect), (int)(this.Size.Height * _fPictureCorrect));
                this.pctCorrect.Location = new System.Drawing.Point((this.Size.Width - pctCorrect.Size.Width) / 2, (this.Size.Height - pctCorrect.Size.Height) / 2);
                if(System.IO.File.Exists(_path + @"\images\Sequence correct.svg"))
                    bitmap = GetBitmapFromSVG(_path + @"\images\Sequence correct.svg", this.pctCorrect.Width, this.pctCorrect.Height);
                if (bitmap != null)
                {
                    this.pctCorrect.Image = bitmap;
                    this.pctCorrect.Region = new Region(GetRegionFromTransparentBitmap(bitmap));
                }

                this.pctWrong.Size = new Size((int)(this.Size.Height * _fPictureCorrect), (int)(this.Size.Height * _fPictureCorrect));
                this.pctWrong.Location = new System.Drawing.Point((this.Size.Width - pctWrong.Size.Width) / 2, (this.Size.Height - pctWrong.Size.Height) / 2);
                if (System.IO.File.Exists(_path + @"\images\Sequence wrong.svg"))
                    bitmap = GetBitmapFromSVG(_path + @"\images\Sequence wrong.svg", this.pctWrong.Width, this.pctWrong.Height);
                if (bitmap != null)
                {
                    this.pctWrong.Image = bitmap;
                    this.pctWrong.Region = new Region(GetRegionFromTransparentBitmap(bitmap));
                }
            }

            base.OnResize(e);

            this.ResumeLayout(true);
            this.PerformLayout();
        }

        public async Task Start(int[] numbers)
        {
            /*
            new System.Threading.Thread(() =>
            {
                CreateButtons(numbers);
            }).Start();
            */
            // Task.Run(() => this.Invoke((new Action(() => CreateButtons(numbers)))));
            this.pctWrong.Visible = false;
            this.pctCorrect.Visible = false;

            Task CreateButtonsTask = CreateButtons(numbers);
            // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/;

            await Task.Delay(400);

            countDown.StartingTime = 3;
            countDown.Visible = true;
            countDown.Start();
            //countDown.Visible = false;
            
            await CreateButtonsTask;

            return;
        }
        // https://stackoverflow.com/questions/2367718/automating-the-invokerequired-code-pattern
        private void OnCountDownEnding(object sender, TimerEndingEventArgs e)
        {
            // This event is fired by a System.Timer, which runs on another thread (not the GUI thread)
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
        }

        public async Task CreateButtons (int[] numbers)
        {
            bool bIntersection = false;
            Random rnd = new Random();
            //_roundButton = new Controls.RoundButton[numbers.Length];
            Region regTotal = new Region();
            Region regIntersec = new Region();
            Graphics g = this.CreateGraphics();
            _nSequenceLength = numbers.Length;

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
                    BorderWidth = _fBorderWidth,
                    Size = new Size(_nDiameter, _nDiameter),
                    xRadius = _nDiameter / 2,
                    yRadius = _nDiameter / 2,
                    Text = numbers[i].ToString(),
                    VisibleBorder = false,
                    Visible = false
                };
                _roundButton[i].Font = new Font(_roundButton[i].Font.FontFamily, _fFontSize * (_nDiameter - 2 * _fBorderWidth));

                do
                {
                    regIntersec = regTotal.Clone();
                    _roundButton[i].Location = new Point(rnd.Next(0, this.Width - _nDiameter), rnd.Next(0, this.Height - _nDiameter));
                    regIntersec.Intersect(_roundButton[i].Bounds);
                    bIntersection = !regIntersec.IsEmpty(g);

                } while (bIntersection);

                if (!bIntersection)
                {
                    _roundButton[i].ButtonClick += new EventHandler<RoundButton.ButtonClickEventArgs>(this.ButtonClicked);
                    this.Controls.Add(_roundButton[i]);
                    regTotal.Union(_roundButton[i].Bounds);
                    //regIntersect = reg1.Clone();
                }
                else
                {

                }

            }
            this.ResumeLayout();

            //_nSequence = new int[numbers.Length];
            //Array.Copy(numbers, _nSequence, numbers.Length);
            _nSequenceCounter = 0;

            regTotal.Dispose();
            regIntersec.Dispose();
            g.Dispose();

            return;
        }

        private async void ButtonClicked(object sender, RoundButton.ButtonClickEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("Button clicked");

            //Application.DoEvents();
            // Change the state of the clicked button
            var roundButton = (RoundButton)sender;
            roundButton.VisibleBorder = false;
            roundButton.VisibleText = true;
            roundButton.Update();
            //_roundButton[_nSequenceCounter].VisibleBorder = false;
            
            // Check whether the user clicked the correct button in the sequence
            bool result = int.Parse(_roundButton[_nSequenceCounter].Text) == e.ButtonValue ? true : false;

            // If the user missed then raise the event
            if (!result)
            {               
                await PlayAudioFile(AudioSoundType.NumberWrong, AudioSoundMode.Sync);
                pctWrong.Visible = true;
                pctWrong.Update();
                Task taskError = ShowError(_nSequenceCounter);
                await PlayAudioFile(AudioSoundType.SequenceWrong, AudioSoundMode.Async);
                await Task.Delay(100);

                OnWrongSequence(new SequenceEventArgs(_nSequenceLength - 2));

                await taskError;

                return;
            }

            // If the user is correct
            _nSequenceCounter++;

            if (_nSequenceCounter < _nSequenceLength)
            {
                PlayAudioFile(AudioSoundType.NumberCorrect, AudioSoundMode.Async);
            }
            else if (_nSequenceCounter >= _nSequenceLength)
            {
                await PlayAudioFile(AudioSoundType.NumberCorrect, AudioSoundMode.Sync);
                pctCorrect.Visible = true;
                pctCorrect.Update();
                await PlayAudioFile(AudioSoundType.SequenceCorrect, AudioSoundMode.Sync);
                await Task.Delay(100);
                OnRightSequence(new SequenceEventArgs(_nSequenceCounter));
            }

        }

        private async Task PlayAudioWrong()
        {
            await PlayAudioFile(AudioSoundType.NumberWrong, AudioSoundMode.Sync);
            await PlayAudioFile(AudioSoundType.SequenceWrong, AudioSoundMode.Sync);
        }

        private async Task PlayAudioCorrect()
        {
            await PlayAudioFile(AudioSoundType.NumberCorrect, AudioSoundMode.Sync);
            await PlayAudioFile(AudioSoundType.SequenceCorrect, AudioSoundMode.Sync);
        }

        private async Task ShowError(int sequenceError)
        {
            this.SuspendLayout();
            while (sequenceError< _roundButton.Length)
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
            // If no sounds are to be played, then exit
            if (!_sound) return;

            // Else, play the selected sound in the selected mode
            if (_soundPlayer[(int)type] != null)
            {
                if (mode == AudioSoundMode.Sync) _soundPlayer[(int)type].PlaySync();
                else if (mode == AudioSoundMode.Async) _soundPlayer[(int)type].Play();
            }
        }

        private async Task ResultCorrect()
        {

        }

        private async Task ResultWrong()
        {

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

        /// <summary>
        /// Gets a region pixel by pixel from the non-transparent pixels of the bitmap
        /// More information here: https://www.codeproject.com/Articles/617613/Fast-pixel-operations-in-NET-with-and-without-unsa
        /// and here: https://www.codeproject.com/Articles/406045/Why-the-use-of-GetPixel-and-SetPixel-is-so-ineffic
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        private System.Drawing.Drawing2D.GraphicsPath GetRegionFromTransparentBitmap (System.Drawing.Bitmap bitmap)
        {
            System.Drawing.Drawing2D.GraphicsPath region = new System.Drawing.Drawing2D.GraphicsPath();

            // Check that we have a bitmap
            if (bitmap == null) return null;

            System.Drawing.Imaging.BitmapData imageData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                                                                            System.Drawing.Imaging.ImageLockMode.ReadWrite,
                                                                            bitmap.PixelFormat);

            int bytesPerPixel = (Image.GetPixelFormatSize(bitmap.PixelFormat)) >> 3;

            unsafe
            {
                byte* PixelComponent = (byte*)imageData.Scan0;
                for (int j = 0; j < imageData.Height; j++)
                {
                    byte* row = (byte*)imageData.Scan0 + (j * imageData.Stride);

                    for (int i = 0; i < imageData.Width; i++)
                    {
                        if (row[i * bytesPerPixel + 3] > 0) region.AddRectangle(new Rectangle(i, j, 1, 1));
                    }
                }
            }

            bitmap.UnlockBits(imageData);

            /*
            // Marshal
            byte[] imageBytes = new byte[Math.Abs(imageData.Stride) * bitmap.Height];
            IntPtr scan0 = imageData.Scan0;

            System.Runtime.InteropServices.Marshal.Copy(scan0, imageBytes, 0, imageBytes.Length);

            for (int i = 0; i < imageBytes.Length; i += 4)
            {
                //if (imageBytes[i + 3] > 0) region.AddRectangle((uint)(i / imageData.Stride), , 1, 1);
            }

            System.Runtime.InteropServices.Marshal.Copy(imageBytes, 0, scan0, imageBytes.Length);

            bitmap.UnlockBits(imageData);
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
            return region;
        }

        #endregion Private routines
    }
}

