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

            board1.Parent = this;

            // Subscribe to events
            //countDown1.TimerEnding += new EventHandler<Controls.TimerEndingEventArgs>(OnTimerEnding);
            board1.ButtonClick += new EventHandler<Board.ButtonClickEventArgs>(OnButtonClick);
            //board1.RightSequence += new EventHandler<Board.SequenceEventArgs>(OnCorrectSequence);
            //board1.RightSequence += async (object s, Board.SequenceEventArgs e) => await OnCorrectSequence(s, e);
            //board1.WrongSequence += new EventHandler<Board.SequenceEventArgs>(OnWrongSequence);
            //_game.CorrectSequence += new EventHandler<Game.CorrectEventArgs>(OnCorrectSequence);
            //_game.WrongSequence += new EventHandler<Game.WrongEventArgs>(OnWrongSequence);
            //_game.GameOver += new EventHandler<Game.OverEventArgs>(OnGameOver);
            _game.CorrectSequence += async (object s, Game.CorrectEventArgs e) => await OnCorrectSequence(s, e);
            _game.WrongSequence += async (object s, Game.WrongEventArgs e) => await OnWrongSequence(s, e);
            _game.GameOver += async (object s, Game.OverEventArgs e) => await OnGameOver(s, e);

            // Read the program settings file and apply them
            _programSettings = new ProgramSettings<string, string>();
            LoadProgramSettings();
            ApplySettings(true);
            board1.Update();
            //board1.Focus();
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
            if (File.Exists(_path + @"\images\soundoff.ico")) this.toolStripMain_Sound.Image = new Icon(_path + @"\images\soundoff.ico", 48, 48).ToBitmap();
            if (File.Exists(_path + @"\images\graph.ico")) this.toolStripMain_Graph.Image = new Icon(_path + @"\images\graph.ico", 48, 48).ToBitmap();
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

        private async void Form1_Resize(object sender, EventArgs e)
        {
            this.SuspendLayout();
            board1.Top = tspTop.Height;
            board1.Left = 0;
            board1.Size = new Size(this.ClientSize.Width, this.ClientSize.Height - tspTop.Height - tspBottom.Height);
            this.ResumeLayout();
        }

        private async void Form1_ResizeEnd(object sender, EventArgs e)
        {

        }

        private void roundButton1_Click(object sender, EventArgs e)
        {
            roundButton1.VisibleBorder = false;
        }

        #endregion Form events

        #region Events subscription

        private void OnButtonClick(object sender, Board.ButtonClickEventArgs e)
        {

            if (_game.Check(e.ButtonValue)) board1.ButtonRight();
            // hide button
        }

        private async Task OnCorrectSequence(object sender, Game.CorrectEventArgs e)
        {
            // Perform the GUI tasks corresponding to a right sequence (sounds, images, buttons, etc)
            board1.SequenceRight();

            // Show the score in the status bar
            this.toolStripStatusLabel_Secuence.Text = e.Score.ToString();
            this.toolStripStatusLabel_Secuence.Invalidate();

            // Wait before starting a new sequence
            await Task.Delay(100);

            int increment = 0;

            // Keep the game going on
            if (_game.Start())
            {
                if ((_game.PlayMode & PlayMode.TimeIncremental) == PlayMode.TimeIncremental)
                    increment = (_game.GetSequence.Length - _game.MinimumLength) * _game.TimeIncrement;

                board1.Start(_game.GetSequence, increment);
            }

        }
        private async Task OnWrongSequence(object sender, Game.WrongEventArgs e)
        {
            // Perform the GUI tasks corresponding to a wrong sequence (sounds, images, buttons, etc)
            board1.ButtonWrong();
            MessageBox.Show("Wrong sequence", "Error");

            // Show the score in the status bar
            this.toolStripStatusLabel_Secuence.Text = e.Score.ToString();
            this.toolStripStatusLabel_Secuence.Invalidate();

            // Wait before starting a new sequence
            await Task.Delay(100);

            int increment = 0;

            _game.CurrentScore -= 2;
            if (_game.Start())
            {
                if ((_game.PlayMode & PlayMode.TimeIncremental) == PlayMode.TimeIncremental)
                    increment = (_game.GetSequence.Length - _game.MinimumLength) * _game.TimeIncrement;

                board1.Start(_game.GetSequence, increment);
            }
        }

        private async Task OnGameOver(object sender, Game.OverEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("OnGameOver subscription event");
            MessageBox.Show("You reached the\nend of the game", "Congratulations!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion Events subscription

        #region toolStripMain

        private void toolStripMain_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripMain_Start_Click(object sender, EventArgs e)
        {
            //countDown1.Start();
            //_game.CurrentScore = _game.MinimumLength - 1;
            _game.ReSet();

            if (!_game.Start())
            {
                MessageBox.Show("Could not start the game\nUnexpected error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            board1.Visible = true;
            board1.Start(_game.GetSequence, 0);
        }

        private void toolStripMain_Sound_CheckedChanged(object sender, EventArgs e)
        {
            this.board1.PlaySounds = !toolStripMain_Sound.Checked;
        }

        private void toolStripMain_Settings_Click(object sender, EventArgs e)
        {
            frmSettings form = new frmSettings(_programSettings);
            form.ShowDialog(this);
            if (form.DialogResult == DialogResult.OK)
            {
                _programSettings = form.settings;
                ApplySettings(false);
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

            _programSettings["Sound"] = this.toolStripMain_Sound.Checked == true ? "0" : "1";

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
        /// <param name="WindowSettings">True if the window position and size should be applied. False if omitted</param>
        private void ApplySettings(bool WindowSettings = false)
        {
            if (WindowSettings)
            {
                if (Convert.ToInt32(_programSettings["WindowPosition"]) == 1 ? true : false)
                {
                    var startPos = this.StartPosition;
                    this.StartPosition = FormStartPosition.Manual;
                    this.DesktopLocation = new Point(Convert.ToInt32(_programSettings["WindowLeft"]), Convert.ToInt32(_programSettings["WindowTop"]));
                    this.ClientSize = new Size(Convert.ToInt32(_programSettings["WindowWidth"]), Convert.ToInt32(_programSettings["WindowHeight"]));
                    this.StartPosition = startPos;
                }
            }

            this._game.MaximumAttempts = Convert.ToInt32(_programSettings["MaximumAttempts"]);
            this._game.MaximumDigit = Convert.ToInt32(_programSettings["MaximumDigit"]);
            this._game.MinimumDigit = Convert.ToInt32(_programSettings["MinimumDigit"]);
            this._game.PlayMode = (PlayMode)Enum.Parse(typeof(PlayMode), _programSettings["PlayMode"]);
            this._game.Time = Convert.ToInt32(_programSettings["Time"]);
            this._game.TimeIncrement = Convert.ToInt32(_programSettings["TimeIncrement"]);
            this.board1.Time = Convert.ToInt32(_programSettings["Time"]);
            this.board1.TimeIncrement = _programSettings.ContainsKey("TimeIncrement") ? Convert.ToInt32(_programSettings["TimeIncrement"]) : 0;
            this.board1.BorderRatio = Convert.ToSingle(_programSettings["BorderRatio"]);
            this.board1.CountDownRatio = Convert.ToSingle(_programSettings["CountDownRatio"]);
            this.board1.NumbersRatio = Convert.ToSingle(_programSettings["NumbersRatio"]);
            this.board1.FontRatio = Convert.ToSingle(_programSettings["FontRatio"]);
            this.board1.ResultRatio = Convert.ToSingle(_programSettings["ResultsRatio"]);

            this.toolStripMain_Sound.Checked = _programSettings.ContainsKey("Sound") ? (Convert.ToInt32(_programSettings["Sound"]) == 0 ? true : false) : false;
            this.board1.PlaySounds = !this.toolStripMain_Sound.Checked;
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

            _programSettings["MaximumAttempts"] = "10";
            _programSettings["MaximumDigit"] = "9";
            _programSettings["MinimumDigit"] = "0";
            _programSettings["Time"] = "700";
            _programSettings["TimeIncrement"] = "0";

            _programSettings["BorderRatio"] = "0.12";
            _programSettings["CountDownRatio"] = "0.37";
            _programSettings["NumbersRatio"] = "0.25";
            _programSettings["FontRatio"] = "0.55";
            _programSettings["ResultsRatio"] = "0.56";
            _programSettings["WindowPosition"] = "1";

            _programSettings["PlayMode"] = "9";

            _programSettings["Sound"] = "1";     // Sound on unchecked
        }


        #endregion Application settings


    }
}
