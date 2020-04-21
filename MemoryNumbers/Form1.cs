using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Controls;
using Utils;

namespace MemoryNumbers
{
    public partial class Form1 : Form
    {
        //RoundButton.RoundButton btn = new RoundButton.RoundButton();
        Game _game = new Game();
        string _path;
        // Program settings
        private ProgramSettings<string, string> _programSettings;
        private static readonly string _programSettingsFileName = @"Configuration.xml";

        public Form1()
        {
            // Set form icon
            _path = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            if (File.Exists(_path + @"\images\logo.ico")) this.Icon = new Icon(_path + @"\images\logo.ico");

            // Initialize components
            InitializeComponent();
            InitializeToolStripPanel();
            InitializeToolStrip();
            InitializeMenuStrip();
            InitializeStatusStrip();

            // Subscribe to events
            countDown1.TimerEnding += new EventHandler<Controls.TimerEndingEventArgs>(OnTimerEnding);
            board1.ButtonClick += new EventHandler<Board.ButtonClickEventArgs>(OnButtonClick);
            board1.RightSequence += new EventHandler<Board.SequenceEventArgs>(OnCorrectSequence);
            board1.WrongSequence += new EventHandler<Board.SequenceEventArgs>(OnWrongSequence);
            _game.CorrectSequence += new EventHandler<Game.CorrectEventArgs>(OnCorrectSequence);
            _game.WrongSequence += new EventHandler<Game.WrongEventArgs>(OnWrongSequence);
            _game.GameOver += new EventHandler<Game.OverEventArgs>(OnGameOver);

            // Read the program settings file and apply them
            _programSettings = new ProgramSettings<string, string>();
            LoadProgramSettings();
            ApplySettings();
        }

        #region Initialization ToolStrip
        
        /// <summary>
        /// Initialize the ToolStripPanel component: add the child components to it
        /// </summary>
        private void InitializeToolStripPanel()
        {
            //tspTop = new ToolStripPanel();
            //tspBottom = new ToolStripPanel();
            tspTop.Join(this.toolStripMain);
            //tspTop.Join(mnuMainFrm);
            tspBottom.Join(this.statusStrip);

            // Exit the method
            return;
        }

        /// <summary>
        /// Initialize the ToolStrip component
        /// </summary>
        private void InitializeToolStrip()
        {

            //ToolStripNumericUpDown c = new ToolStripNumericUpDown();
            //this.toolStripMain.Items.Add((ToolStripItem)c);

            toolStripMain.Renderer = new customRenderer(Brushes.SteelBlue, Brushes.LightSkyBlue);

            //var path = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            if (File.Exists(_path + @"\images\exit.ico")) this.toolStripMain_Exit.Image = new Icon(_path + @"\images\exit.ico", 48, 48).ToBitmap();
            if (File.Exists(_path + @"\images\start.ico")) this.toolStripMain_Start.Image = new Icon(_path + @"\images\start.ico", 48, 48).ToBitmap();
            if (File.Exists(_path + @"\images\stop.ico")) this.toolStripMain_Stop.Image = new Icon(_path + @"\images\stop.ico", 48, 48).ToBitmap();
            if (File.Exists(_path + @"\images\sound.ico")) this.toolStripMain_Sound.Image = new Icon(_path + @"\images\sound.ico", 48, 48).ToBitmap();
            if (File.Exists(_path + @"\images\settings.ico")) this.toolStripMain_Settings.Image = new Icon(_path + @"\images\settings.ico", 48, 48).ToBitmap();
            //if (File.Exists(path + @"\images\save.ico")) this.toolStripMain_Data.Image = new Icon(path + @"\images\save.ico", 48, 48).ToBitmap();
            //if (File.Exists(path + @"\images\picture.ico")) this.toolStripMain_Picture.Image = new Icon(path + @"\images\picture.ico", 48, 48).ToBitmap();
            //if (File.Exists(path + @"\images\reflect-horizontal.ico")) this.toolStripMain_Mirror.Image = new Icon(path + @"\images\reflect-horizontal.ico", 48, 48).ToBitmap();
            //if (File.Exists(path + @"\images\plot.ico")) this.toolStripMain_Plots.Image = new Icon(path + @"\images\plot.ico", 48, 48).ToBitmap();
            if (File.Exists(_path + @"\images\about.ico")) this.toolStripMain_About.Image = new Icon(_path + @"\images\about.ico", 48, 48).ToBitmap();

            /*
            using (Graphics g = Graphics.FromImage(this.toolStripMain_Skeleton.Image))
            {
                g.Clear(Color.PowderBlue);
            }
            */

            //this.toolStripMain_Disconnect.Enabled = false;
            //this.toolStripMain_SkeletonWidth.NumericUpDownControl.Maximum = 20;
            //this.toolStripMain_SkeletonWidth.NumericUpDownControl.Minimum = 1;

            // Exit the method
            return;
        }

        /// <summary>
        /// Initialize the MenuStrip component
        /// </summary>
        private void InitializeMenuStrip()
        {
            return;
        }

        /// <summary>
        /// Initialize the StatusStrip component
        /// </summary>
        private void InitializeStatusStrip()
        {
            return;
        }

        #endregion Initialization ToolStrip

        #region Form events

        private void Form1_Load(object sender, EventArgs e)
        {
            // https://stackoverflow.com/questions/21632642/label-without-padding-and-margin
            
            // Creates the fade in animation of the form
            Win32.Win32API.AnimateWindow(this.Handle, 200, Win32.Win32API.AnimateWindowFlags.AW_BLEND | Win32.Win32API.AnimateWindowFlags.AW_CENTER);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            // Send Close event
            using (var closeSplashEvent = new System.Threading.EventWaitHandle(false, System.Threading.EventResetMode.ManualReset, "CloseSplashScreenEvent"))
            {
                closeSplashEvent.Set();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (new CenterWinDialog(this))
            {
                if (DialogResult.No == MessageBox.Show(this,
                                                        "Are you sure you want to exit\nthe application?",
                                                        "Exit?",
                                                        MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Question,
                                                        MessageBoxDefaultButton.Button2))
                {
                    // Cancelar el cierre de la ventana
                    e.Cancel = true;
                }
                else
                    Win32.Win32API.AnimateWindow(this.Handle, 200, Win32.Win32API.AnimateWindowFlags.AW_BLEND | Win32.Win32API.AnimateWindowFlags.AW_HIDE);
            }

            // Guardar los datos de configuración
            SaveProgramSettings();
        }

        private void roundButton1_Click(object sender, EventArgs e)
        {
            roundButton1.VisibleBorder = false;
        }

        #endregion Form events

        #region Events subscription

        private void OnCorrectSequence(object sender, Board.SequenceEventArgs e)
        {
            // Show the score in the status bar
            this.toolStripStatusLabel_Secuence.Text = e.SequenceLength.ToString();
            this.toolStripStatusLabel_Secuence.Invalidate();
            
            // Keep the game going on
            _game.CurrentScore = e.SequenceLength;
            if (_game.Start())
            {
                board1.Start(_game.GetSequence);
            }
            

        }

        private void OnCorrectSequence(object sender, Game.CorrectEventArgs e)
        {

        }
        private void OnWrongSequence(object sender, Game.WrongEventArgs e)
        {

        }
        private void OnGameOver(object sender, Game.OverEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("OnGameOver subscription event");
            MessageBox.Show("You reached the\nend of the game", "Congratulations!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void OnWrongSequence(object sender, Board.SequenceEventArgs e)
        {
            MessageBox.Show("Wrong sequence","Error");
            OnCorrectSequence(sender, e);
        }

        private void OnButtonClick(object sender, Board.ButtonClickEventArgs e)
        {

            _game.Check(e.ButtonValue);
            // hide button
        }

        private void OnTimerEnding(object sender, TimerEndingEventArgs e)
        {
            // countDown1.roundButton1.BorderColor = Color.Transparent;
            //countDown1.VisibleText = false;
            // Since the Sytem.Timer runs in another thread, we need to use Invoke to get back to the control thread (which is the UI thread)
            ((Controls.CountDown)sender).Invoke((new Action(() => ((Controls.CountDown)sender).Visible = false)));
        }
        
        #endregion Events subscription

        #region toolStripMain

        private void toolStripMain_Start_Click(object sender, EventArgs e)
        {
            //countDown1.Start();
            _game.CurrentScore = _game.MinimumLength - 1;
            if (!_game.Start())
            {
                MessageBox.Show("Could not start the game\nUnexpected error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            board1.Visible = true;
            board1.Start(_game.GetSequence);
        }

        private void toolStripMain_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripMain_Settings_Click(object sender, EventArgs e)
        {
            frmSettings form = new frmSettings(_programSettings);
            form.ShowDialog(this);
            if (form.DialogResult == DialogResult.OK)
            {
                _programSettings = form.settings;
            }
        }

        private void toolStripMain_About_Click(object sender, EventArgs e)
        {
            frmAbout form = new frmAbout();
            form.ShowDialog(this);
            
        }

        #endregion toolStripMain

        #region Application settings

        /// <summary>
        /// Loads any saved program settings.
        /// </summary>
        private void LoadProgramSettings()
        {
            // Load the saved window settings and resize the window.
            TextReader textReader = StreamReader.Null;
            try
            {
                textReader = new StreamReader(_programSettingsFileName);
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(ProgramSettings<string, string>));
                _programSettings = (ProgramSettings<string, string>)serializer.Deserialize(textReader);
                textReader.Close();
            }
            catch (Exception ex)
            {

                if (!(ex is FileNotFoundException))
                {
                    using (new CenterWinDialog(this))
                    {
                        MessageBox.Show(this,
                                        "Unexpected error while\nloading settings data",
                                        "Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                }
                LoadDefaultSettings();
            }
            finally
            {
                if (textReader != null) textReader.Close();
            }
        }

        /// <summary>
        /// Saves the current program settings.
        /// </summary>
        private void SaveProgramSettings()
        {
            _programSettings["WindowLeft"] = this.DesktopLocation.X.ToString();
            _programSettings["WindowTop"] = this.DesktopLocation.Y.ToString();
            _programSettings["WindowWidth"] = this.ClientSize.Width.ToString();
            _programSettings["WindowHeight"] = this.ClientSize.Height.ToString();

            _programSettings["MaximumDigit"] = this.board1.MaxNumber.ToString();
            _programSettings["MinimumDigit"] = this.board1.MinNumber.ToString();
            _programSettings["Time"] = this.board1.Time.ToString();
            _programSettings["BorderRatio"] = this.board1.BorderRatio.ToString();
            _programSettings["CountDownRatio"] = this.board1.CountDownRatio.ToString();
            _programSettings["NumbersRatio"] = this.board1.NumbersRatio.ToString();
            _programSettings["FontRatio"] = this.board1.FontRatio.ToString();
            _programSettings["ResultsRatio"] = this.board1.ResultRatio.ToString();

            _programSettings["MaximumAttempts"] = this._game.MaximumAttempts.ToString();


            //_programSettings["MirrorChecked"] = (this.toolStripMain_Mirror.Checked ? 1 : 0).ToString();
            //_programSettings["PlotsChecked"] = (this.toolStripMain_Plots.Checked ? 1 : 0).ToString();
            //_programSettings["SkeletonChecked"] = (this.toolStripMain_Skeleton.Checked ? 1 : 0).ToString();
            //_programSettings["SkeletonColor"]
            //_programSettings["SkeletonWidth"] = ((Int32)this.toolStripMain_SkeletonWidth.NumericUpDownControl.Value).ToString();
            //_programSettings["JointChecked"] = (this.toolStripMain_Joint.Checked ? 1 : 0).ToString(); ;
            //_programSettings["JointColor"] = this._jointColor.ToArgb().ToString();
            //_programSettings["JointSize"] = this._jointSize.ToString();
            //_programSettings["AngleChecked"] = (this.toolStripMain_Angle.Checked ? 1 : 0).ToString();
            //_programSettings["AngleColor"] = this._angleColor.ToArgb().ToString();
            //_programSettings["AngleFill"] = this._angleFill.ToArgb().ToString();
            //_programSettings["AngleSize"] = this._angleSize.ToString();
            // _programSettings[""] =


            // Save window settings.
            TextWriter textWriter = StreamWriter.Null;
            try
            {
                textWriter = new StreamWriter(_programSettingsFileName, false);
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(ProgramSettings<string, string>));
                serializer.Serialize(textWriter, _programSettings);
                textWriter.Close();
            }
            catch (Exception ex)
            {
                using (new CenterWinDialog(this))
                {
                    MessageBox.Show(this,
                                    "Unexpected error while\nsaving settings data",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
            finally
            {
                if (textWriter != null) textWriter.Close();
            }

        }

        /// <summary>
        /// Update UI with settings 
        /// </summary>
        private void ApplySettings()
        {
            /*
            this.StartPosition = FormStartPosition.Manual;
            this.DesktopLocation = new Point(Convert.ToInt32(_programSettings["WindowLeft"]), Convert.ToInt32(_programSettings["WindowTop"]));
            this.ClientSize = new Size(Convert.ToInt32(_programSettings["WindowWidth"]), Convert.ToInt32(_programSettings["WindowHeight"]));
            this.toolStripMain_Video.Checked = Convert.ToInt32(_programSettings["DisplayFrame"]) == 1 ? true : false;
            statusStripLabelVideo.Enabled = toolStripMain_Video.Checked;
            this.toolStripMain_Mirror.Checked = Convert.ToInt32(_programSettings["MirrorChecked"]) == 1 ? true : false;
            statusStripLabelMirror.Enabled = toolStripMain_Mirror.Checked;
            this.toolStripMain_Plots.Checked = Convert.ToInt32(_programSettings["PlotsChecked"]) == 1 ? true : false;
            statusStripLabelPlots.Enabled = toolStripMain_Plots.Checked;


            this._boneColor = Color.FromArgb(Convert.ToInt32(_programSettings["SkeletonColor"]));
            this._boneThickness = Convert.ToInt32(_programSettings["SkeletonWidth"]);
            this._boneDraw = Convert.ToInt32(_programSettings["SkeletonChecked"]) == 1 ? true : false;
            this.toolStripMain_Skeleton.Checked = _boneDraw;
            statusStripLabelSkeleton.Enabled = _boneDraw;
            Graphics.FromImage(this.toolStripMain_Skeleton.Image).Clear(_boneColor);
            this.toolStripMain_SkeletonWidth.NumericUpDownControl.Value = Convert.ToInt32(_boneThickness);
            this.toolStripMain_Skeleton.Invalidate();

            this._jointColor = Color.FromArgb(Convert.ToInt32(_programSettings["JointColor"]));
            this._jointSize = Convert.ToInt32(_programSettings["JointSize"]);
            this._jointDraw = Convert.ToInt32(_programSettings["JointChecked"]) == 1 ? true : false;
            this.toolStripMain_Joint.Checked = _jointDraw;
            statusStripLabelJoint.Enabled = _jointDraw;
            Graphics.FromImage(this.toolStripMain_Joint.Image).Clear(_jointColor);
            this.toolStripMain_JointWidth.NumericUpDownControl.Value = Convert.ToInt32(_jointSize);
            this.toolStripMain_Joint.Invalidate();

            this._angleFill = Color.FromArgb(Convert.ToInt32(_programSettings["AngleFill"]));
            this._angleColor = Color.FromArgb(Convert.ToInt32(_programSettings["AngleColor"]));
            this._angleSize = Convert.ToInt32(_programSettings["AngleSize"]);
            this._angleDraw = Convert.ToInt32(_programSettings["AngleChecked"]) == 1 ? true : false;
            this.toolStripMain_Angle.Checked = _angleDraw;
            statusStripLabelAngle.Enabled = _angleDraw;
            Graphics.FromImage(this.toolStripMain_Angle.Image).Clear(_angleColor);
            this.toolStripMain_AngleWidth.NumericUpDownControl.Value = Convert.ToInt32(_angleSize);
            this.toolStripMain_Angle.Invalidate();
            */
        }

        /// <summary>
        /// Set default settings. This is called when no settings file has been found
        /// </summary>
        private void LoadDefaultSettings()
        {
            // Set default settings
            _programSettings["WindowLeft"] = this.DesktopLocation.X.ToString();    // Get current form coordinates
            _programSettings["WindowTop"] = this.DesktopLocation.Y.ToString();
            _programSettings["WindowWidth"] = this.ClientSize.Width.ToString();    // Get current form size
            _programSettings["WindowHeight"] = this.ClientSize.Height.ToString();
            //_programSettings["DisplayFrame"] = "1";     // Checked
            //_programSettings["MirrorChecked"] = "0";    // Unchecked
            //_programSettings["PlotsChecked"] = "1";     // Checked
            //_programSettings["SkeletonChecked"] = "1";  // Checked
            //_programSettings["SkeletonColor"] = Color.Red.ToArgb().ToString();
            //_programSettings["SkeletonWidth"] = "3";
            //_programSettings["JointChecked"] = "1";     // Checked
            //_programSettings["JointColor"] = Color.Purple.ToArgb().ToString();
            //_programSettings["JointSize"] = "5";
            //_programSettings["AngleChecked"] = "1";     // Checked
            //_programSettings["AngleColor"] = Color.Green.ToArgb().ToString();
            //_programSettings["AngleFill"] = Color.FromArgb(127, Color.Green).ToArgb().ToString();
            //_programSettings["AngleSize"] = "3";
        }



        #endregion Application settings


    }
}
