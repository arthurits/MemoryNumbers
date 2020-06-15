using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
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
        private ProgramSettings<string, string> _defaultSettings;
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
            InitializeChartStats();

            splitStats.SplitterWidth = 1;   // This is a known bug

            //board1.Parent = this;

            // Subscribe to events
            //countDown1.TimerEnding += new EventHandler<Controls.TimerEndingEventArgs>(OnTimerEnding);
            board1.ButtonClick += new EventHandler<Board.ButtonClickEventArgs>(OnButtonClick);
            //board1.RightSequence += new EventHandler<Board.SequenceEventArgs>(OnCorrectSequence);
            //board1.WrongSequence += new EventHandler<Board.SequenceEventArgs>(OnWrongSequence);
            _game.CorrectSequence += new EventHandler<Game.CorrectEventArgs>(OnCorrectSequence);
            _game.WrongSequence += new EventHandler<Game.WrongEventArgs>(OnWrongSequence);
            _game.GameOver += new EventHandler<Game.OverEventArgs>(OnGameOver);
            //_game.CorrectSequence += async (object s, Game.CorrectEventArgs e) => await OnCorrectSequence(s, e);
            //_game.WrongSequence += async (object s, Game.WrongEventArgs e) => await OnWrongSequence(s, e);
            //_game.GameOver += async (object s, Game.OverEventArgs e) => await OnGameOver(s, e);

            // Read the program settings file and apply them
            LoadProgramSettings(ref _programSettings);
            
            _defaultSettings = new ProgramSettings<string, string>();
            LoadDefaultSettings(_defaultSettings);

            if (_programSettings == null) _programSettings = _defaultSettings;

            ApplySettings(_programSettings, _defaultSettings, true);

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

            if (File.Exists(_path + @"\images\exit.ico")) this.toolStripMain_Exit.Image = new Icon(_path + @"\images\exit.ico", 48, 48).ToBitmap();
            if (File.Exists(_path + @"\images\start.ico")) this.toolStripMain_Start.Image = new Icon(_path + @"\images\start.ico", 48, 48).ToBitmap();
            if (File.Exists(_path + @"\images\stop.ico")) this.toolStripMain_Stop.Image = new Icon(_path + @"\images\stop.ico", 48, 48).ToBitmap();
            if (File.Exists(_path + @"\images\soundoff.ico")) this.toolStripMain_Sound.Image = new Icon(_path + @"\images\soundoff.ico", 48, 48).ToBitmap();
            if (File.Exists(_path + @"\images\graph.ico")) this.toolStripMain_Stats.Image = new Icon(_path + @"\images\graph.ico", 48, 48).ToBitmap();
            if (File.Exists(_path + @"\images\settings.ico")) this.toolStripMain_Settings.Image = new Icon(_path + @"\images\settings.ico", 48, 48).ToBitmap();
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

        private void InitializeChartStats ()
        {
            // Chart1.Series[0].IsVisibleInLegend = false; 
            this.chartStatsNumbers.Legends["Legend1"].Enabled = false;
            this.chartStatsTime.Legends["Legend1"].Enabled = false;

            // this.chartStatsNumbers.Series["Series1"].Points.Clear();
            /*this.chartStatsNumbers.Series["Right"].Points.AddXY("#1", 60);
            this.chartStatsNumbers.Series["Wrong"].Points.AddXY("#1", 40);
            this.chartStatsNumbers.Series["Right"].Points.AddXY("#2", 68);
            this.chartStatsNumbers.Series["Wrong"].Points.AddXY("#2", 32);
            this.chartStatsNumbers.Series["Right"].Points.AddXY("#3", 82);
            this.chartStatsNumbers.Series["Wrong"].Points.AddXY("#3", 28);
            */
            this.chartStatsTime.Series.Clear();
            this.chartStatsTime.ChartAreas[0].AxisX.Crossing = 0;
            this.chartStatsTime.ChartAreas[0].AxisX.Minimum = 0;
            this.chartStatsNumbers.ChartAreas[0].AxisX.Crossing = 0;
            this.chartStatsNumbers.ChartAreas[0].AxisX.Interval = 1;

            /*
            this.chartStatsTime.Series.Add(new Series() { ChartType = SeriesChartType.StackedColumn });
            this.chartStatsTime.Series.Add(new Series() { ChartType = SeriesChartType.StackedColumn });
            
            this.chartStatsTime.Series[0].Points.AddXY(1, 0.3852591);
            this.chartStatsTime.Series[1].Points.AddXY(1, 0.7029007);

            this.chartStatsTime.Series.Add(new Series() { ChartType = SeriesChartType.StackedColumn });
            this.chartStatsTime.Series[2].Points.AddXY(1, 0);

            this.chartStatsTime.Series[0].Points.AddXY(2, 0.3323844);
            this.chartStatsTime.Series[1].Points.AddXY(2, 0.3194352);
            this.chartStatsTime.Series[2].Points.AddXY(2, 0.8386591);

            this.chartStatsTime.Series.Add(new Series() { ChartType = SeriesChartType.StackedColumn });
            this.chartStatsTime.Series[3].Points.AddXY(1, 0);
            this.chartStatsTime.Series[3].Points.AddXY(2, 0);

            this.chartStatsTime.Series[0].Points.AddXY(3, 1.5502257);
            this.chartStatsTime.Series[1].Points.AddXY(3, 0.6354501);
            this.chartStatsTime.Series[2].Points.AddXY(3, 0.4801333);
            this.chartStatsTime.Series[3].Points.AddXY(3, 0.4382886);
            */

            //this.chartStatsTime.Series["Time"].Points.AddXY("2", 5);
            //this.chartStatsTime.Series["Time"].Points.AddXY("2", 10);

            /*
            
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();

            // 
            // chartStatsNumbers
            // 
            chartArea1.AxisY.Title = "Percentage";
            chartArea1.Name = "ChartArea1";
            this.chartStatsNumbers.ChartAreas.Add(chartArea1);
            this.chartStatsNumbers.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.chartStatsNumbers.Legends.Add(legend1);
            this.chartStatsNumbers.Location = new System.Drawing.Point(0, 0);
            this.chartStatsNumbers.Name = "chartStatsNumbers";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn100;
            series1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(0)))));
            series1.Legend = "Legend1";
            series1.Name = "Right";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn100;
            series2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            series2.Legend = "Legend1";
            series2.Name = "Wrong";
            this.chartStatsNumbers.Series.Add(series1);
            this.chartStatsNumbers.Series.Add(series2);
            this.chartStatsNumbers.Size = new System.Drawing.Size(262, 256);
            this.chartStatsNumbers.TabIndex = 0;
            this.chartStatsNumbers.Text = "chartStatsNumber";
            // 
            // chartStatsTime
            // 
            chartArea2.AxisY.Title = "Time in seconds";
            chartArea2.Name = "ChartArea1";
            this.chartStatsTime.ChartAreas.Add(chartArea2);
            this.chartStatsTime.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Enabled = false;
            legend2.Name = "Legend1";
            this.chartStatsTime.Legends.Add(legend2);
            this.chartStatsTime.Location = new System.Drawing.Point(0, 0);
            this.chartStatsTime.Name = "chartStatsTime";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            series3.Legend = "Legend1";
            series3.Name = "Time";
            this.chartStatsTime.Series.Add(series3);
            this.chartStatsTime.Size = new System.Drawing.Size(260, 256);
            this.chartStatsTime.TabIndex = 0;
            this.chartStatsTime.Text = "chartStatsTime";
             */


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
            SaveProgramSettings(_programSettings);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            //this.SuspendLayout();
            //board1.Top = tspTop.Height;
            //board1.Left = 0;
            //board1.Size = new Size(this.ClientSize.Width, this.ClientSize.Height - tspTop.Height - tspBottom.Height);
            //this.ResumeLayout();
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {

        }

        #endregion Form events

        #region Events subscription

        private void OnButtonClick(object sender, Board.ButtonClickEventArgs e)
        {
            // Add series when needed
            if (_game.CurrentScore > this.chartStatsTime.Series.Count)
            {
                for (int i = this.chartStatsTime.Series.Count; i < _game.CurrentScore; i++)
                {
                    this.chartStatsTime.Series.Add(new Series()
                    {
                        ChartType = SeriesChartType.StackedColumn
                    });
                }
            }
            // Check all series have the same number of points
            for (int i = 0; i < this.chartStatsTime.Series.Count; i++)
            {
                for (int j = this.chartStatsTime.Series[i].Points.Count; j < _game.GetCurrentAttempt; j++)
                {
                    this.chartStatsTime.Series[i].Points.AddXY(j, 0);
                }
            }
            // Update the current data
            this.chartStatsTime.Series[_game.GetSequenceIndex].Points.ElementAt(_game.GetCurrentAttempt - 1).SetValueXY(_game.GetCurrentAttempt, e.Seconds);

            if (_game.Check(e.ButtonValue, e.Seconds)) board1.ButtonRight();
            
        }

        private async void OnCorrectSequence(object sender, Game.CorrectEventArgs e)
        {
            // Perform the GUI tasks corresponding to a right sequence (sounds, images, buttons, etc)
            Task t = board1.SequenceRight();

            // Show the score in the status bar
            this.toolStripStatusLabel_Secuence.Text = e.Score.ToString();
            this.toolStripStatusLabel_Secuence.Invalidate();

            // Wait before starting a new sequence
            await Task.Delay(100);
            await t;

            // Keep the game going on
            if (_game.Start())
            {
                if (await board1.Start(_game.GetSequence, _game.TimeTotal) == false)
                {
                    toolStripMain_Stop_Click(null, null);
                    using (new CenterWinDialog(this))
                        MessageBox.Show("Could not place the buttons on the screen.\n" +
                            "Please, try reducing the 'numbers ratio' paremeter in\n" +
                            "the Settings (between 0.25 - 0.30).",
                            "Error placing numbers",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                }
            }

        }
        private async void OnWrongSequence(object sender, Game.WrongEventArgs e)
        {
            // Perform the GUI tasks corresponding to a wrong sequence (sounds, images, buttons, etc)
            Task t = board1.ButtonWrong();

            // Show the score in the status bar
            this.toolStripStatusLabel_Secuence.Text = e.Score.ToString();
            this.toolStripStatusLabel_Secuence.Invalidate();

            using (new CenterWinDialog(this))
                MessageBox.Show("Wrong sequence", "Error");

            // Wait before starting a new sequence
            //await Task.Delay(100);
            await t;        

            _game.CurrentScore -= 2;
            if (_game.Start())
            {
                if (await board1.Start(_game.GetSequence, _game.TimeTotal) == false)
                {
                    toolStripMain_Stop_Click(null, null);
                    using (new CenterWinDialog(this))
                        MessageBox.Show("Could not place the buttons on the screen.\nPlease, try reducing the 'numbers ratio' paremeter in\nthe Settings (between 0.25 - 0.30).", "Error placing numbers", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void OnGameOver(object sender, Game.OverEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("OnGameOver subscription event");
            using (new CenterWinDialog(this))
                MessageBox.Show("You reached the\nend of the game", "Congratulations!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Update GUI
            board1.ClearBoard();
            ChartStatsNumbers_Update();
            ChartStatsTime_Update();

            // Commute the visibility of the strip buttons
            this.toolStripMain_Start.Enabled = true;
            this.toolStripMain_Settings.Enabled = true;
            this.toolStripMain_Stats.Checked = true;
            this.toolStripMain_Stats.Enabled = true;

            // Show the stats
            this.tabGame.SelectedIndex = 1;
        }

        #endregion Events subscription

        #region toolStripMain

        private void toolStripMain_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void toolStripMain_Start_Click(object sender, EventArgs e)
        {
            // Commute visibility of the strip buttons
            this.toolStripMain_Start.Enabled = false;
            this.toolStripMain_Settings.Enabled = false;
            this.toolStripMain_Stats.Checked = false;
            this.toolStripMain_Stats.Enabled = false;

            // Show the board
            this.tabGame.SelectedIndex = 0;

            // Always reset before starting a new game
            _game.ReSet();

            // Show the score in the status bar
            this.toolStripStatusLabel_Secuence.Text = _game.CurrentScore.ToString();
            this.toolStripStatusLabel_Secuence.Invalidate();

            if (_game.Start())
            {
                board1.Visible = true;
                if (await board1.Start(_game.GetSequence, _game.TimeTotal) == false)
                {
                    toolStripMain_Stop_Click(null, null);
                    using (new CenterWinDialog(this))
                        MessageBox.Show("Could not place the buttons on the screen.\nPlease, try reducing the 'numbers ratio' paremeter in\nthe Settings (between 0.25 - 0.30).", "Error placing numbers", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                using (new CenterWinDialog(this))
                    MessageBox.Show("Could not start the game.\nUnexpected error.", "Error StartClick", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void toolStripMain_Stop_Click(object sender, EventArgs e)
        {
            board1.ClearBoard();
            this.toolStripStatusLabel_Secuence.Text = "";
            this.toolStripStatusLabel_Secuence.Invalidate();
            
            // Commute visibility of the strip buttons
            this.toolStripMain_Start.Enabled = true;
            this.toolStripMain_Settings.Enabled = true;
            this.toolStripMain_Stats.Enabled = true;

            // Update the charts
            ChartStatsNumbers_Update();
        }

        private void toolStripMain_Sound_CheckedChanged(object sender, EventArgs e)
        {
            this.board1.PlaySounds = !toolStripMain_Sound.Checked;
        }

        private void toolStripMain_Stats_CheckedChanged(object sender, EventArgs e)
        {
            this.tabGame.SelectedIndex = toolStripMain_Stats.Checked ? 1 : 0;
        }

        private void toolStripMain_Settings_Click(object sender, EventArgs e)
        {
            frmSettings form = new frmSettings(_programSettings, _defaultSettings);
            form.ShowDialog(this);
            if (form.DialogResult == DialogResult.OK)
            {
                _programSettings = form.GetSettings;
                ApplySettings(_programSettings, _defaultSettings, false);
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
        private void LoadProgramSettings(ref ProgramSettings<string, string> settings)
        {
            // Load the saved window settings and resize the window.
            TextReader textReader = StreamReader.Null;
            try
            {
                textReader = new StreamReader(_programSettingsFileName);
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(ProgramSettings<string, string>));
                settings = (ProgramSettings<string, string>)serializer.Deserialize(textReader);
                textReader.Close();
            }
            catch (Exception ex)
            {

                if (!(ex is FileNotFoundException))
                {
                    using (new CenterWinDialog(this))
                    {
                        MessageBox.Show(this,
                                        "Unexpected error while\nloading settings data.",
                                        "Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                }
                //LoadDefaultSettings();
            }
            finally
            {
                if (textReader != null) textReader.Close();
            }
        }

        /// <summary>
        /// Saves the current program settings.
        /// </summary>
        private void SaveProgramSettings(ProgramSettings<string, string> settings)
        {
            settings["WindowLeft"] = this.DesktopLocation.X.ToString();
            settings["WindowTop"] = this.DesktopLocation.Y.ToString();
            settings["WindowWidth"] = this.ClientSize.Width.ToString();
            settings["WindowHeight"] = this.ClientSize.Height.ToString();

            settings["Sound"] = this.toolStripMain_Sound.Checked == true ? "0" : "1";
            settings["Stats"] = this.toolStripMain_Stats.Checked == true ? "1" : "0";

            if (settings["SplitterDistance"] == "0.5")
                settings["SplitterDistance"] = ((int)(this.splitStats.Size.Width / 2)).ToString();
            else
                settings["SplitterDistance"] = this.splitStats.SplitterDistance.ToString();

            // Save window settings.
            TextWriter textWriter = StreamWriter.Null;
            try
            {
                textWriter = new StreamWriter(_programSettingsFileName, false);
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(ProgramSettings<string, string>));
                serializer.Serialize(textWriter, settings);
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
        private void ApplySettings(ProgramSettings<string, string> programSettings, ProgramSettings<string, string> defaultSettings, bool WindowSettings = false)
        {
            if (WindowSettings)
            {
                if (Convert.ToInt32(programSettings.ContainsKey("WindowPosition") ? programSettings["WindowPosition"] : defaultSettings["WindowPosition"]) == 1)
                {
                    //var startPos = this.StartPosition;
                    this.StartPosition = FormStartPosition.Manual;
                    this.DesktopLocation = new Point(Convert.ToInt32(programSettings.ContainsKey("WindowLeft") ? programSettings["WindowLeft"] : defaultSettings["WindowLeft"]),
                                        Convert.ToInt32(programSettings.ContainsKey("WindowTop") ? programSettings["WindowTop"] : defaultSettings["WindowTop"]));
                    this.ClientSize = new Size(Convert.ToInt32(programSettings.ContainsKey("WindowWidth") ? programSettings["WindowWidth"] : defaultSettings["WindowWidth"]),
                                        Convert.ToInt32(programSettings.ContainsKey("WindowHeight") ? programSettings["WindowHeight"] : defaultSettings["WindowHeight"]));
                    //this.StartPosition = startPos;
                    this.splitStats.SplitterDistance = Convert.ToInt32(programSettings.ContainsKey("SplitterDistance") ? programSettings["SplitterDistance"] : defaultSettings["SplitterDistance"]);
                }
            }

            this._game.MinimumLength = Convert.ToInt32(programSettings.ContainsKey("MinimumLength") ? programSettings["MinimumLength"] : defaultSettings["MinimumLength"]);
            this._game.MaximumAttempts = Convert.ToInt32(programSettings.ContainsKey("MaximumAttempts") ? programSettings["MaximumAttempts"] : defaultSettings["MaximumAttempts"]);
            this._game.MaximumDigit = Convert.ToInt32(programSettings.ContainsKey("MaximumDigit") ? programSettings["MaximumDigit"] : defaultSettings["MaximumDigit"]);
            this._game.MinimumDigit = Convert.ToInt32(programSettings.ContainsKey("MinimumDigit") ? programSettings["MinimumDigit"] : defaultSettings["MinimumDigit"]);
            this._game.PlayMode = (PlayMode)Enum.Parse(typeof(PlayMode), programSettings.ContainsKey("PlayMode") ? programSettings["PlayMode"] : defaultSettings["PlayMode"]);
            this._game.Time = Convert.ToInt32(programSettings.ContainsKey("Time") ? programSettings["Time"] : defaultSettings["Time"]);
            this._game.TimeIncrement = Convert.ToInt32(programSettings.ContainsKey("TimeIncrement") ? programSettings["TimeIncrement"] : defaultSettings["TimeIncrement"]);
            //this.board1.Time = Convert.ToInt32(programSettings.ContainsKey("Time") ? programSettings["Time"] : defaultSettings["Time"]);
            this.board1.BorderRatio = Convert.ToSingle(programSettings.ContainsKey("BorderRatio") ? programSettings["BorderRatio"] : defaultSettings["BorderRatio"], System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
            this.board1.CountDownRatio = Convert.ToSingle(programSettings.ContainsKey("CountDownRatio") ? programSettings["CountDownRatio"] : defaultSettings["CountDownRatio"], System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
            this.board1.NumbersRatio = Convert.ToSingle(programSettings.ContainsKey("NumbersRatio") ? programSettings["NumbersRatio"] : defaultSettings["NumbersRatio"], System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
            this.board1.FontRatio = Convert.ToSingle(programSettings.ContainsKey("FontRatio") ? programSettings["FontRatio"] : defaultSettings["FontRatio"], System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
            this.board1.ResultRatio = Convert.ToSingle(programSettings.ContainsKey("ResultsRatio") ? programSettings["ResultsRatio"] : defaultSettings["ResultsRatio"], System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
            this.board1.BackColor = Color.FromArgb(Convert.ToInt32(programSettings.ContainsKey("BackColor") ? programSettings["BackColor"] : defaultSettings["BackColor"]));
            this.board1.Font = new Font(programSettings.ContainsKey("FontFamilyName") ? programSettings["FontFamilyName"] : defaultSettings["FontFamilyName"], board1.Font.SizeInPoints);

            this.toolStripMain_Sound.Checked = Convert.ToInt32((programSettings.ContainsKey("Sound") ? programSettings["Sound"] : defaultSettings["Sound"])) == 0 ? true : false;
            this.toolStripMain_Stats.Checked = Convert.ToInt32((programSettings.ContainsKey("Stats") ? programSettings["Stats"] : defaultSettings["Stats"])) == 0 ? false : true;
            //this.toolStripMain_Sound.Checked = programSettings.ContainsKey("Sound") ? (Convert.ToInt32(programSettings["Sound"]) == 0 ? true : false) : false;
            this.board1.PlaySounds = !this.toolStripMain_Sound.Checked;
        }

        /// <summary>
        /// Set default settings. This is called when no settings file has been found
        /// </summary>
        private void LoadDefaultSettings(ProgramSettings<string, string> settings)
        {
            // Set default settings
            settings["WindowLeft"] = this.DesktopLocation.X.ToString();    // Get current form coordinates
            settings["WindowTop"] = this.DesktopLocation.Y.ToString();
            settings["WindowWidth"] = this.ClientSize.Width.ToString();    // Get current form size
            settings["WindowHeight"] = this.ClientSize.Height.ToString();

            settings["Time"] = "700";
            settings["TimeIncrement"] = "0";
            settings["MaximumDigit"] = "9";
            settings["MinimumDigit"] = "0";
            settings["MaximumAttempts"] = "10";
            settings["MinimumLength"] = "2";

            settings["CountDownRatio"] = "0.37";
            settings["NumbersRatio"] = "0.25";
            settings["BorderRatio"] = "0.12";
            settings["FontRatio"] = "0.55";
            settings["ResultsRatio"] = "0.56";
            settings["FontFamilyName"] = "Microsoft Sans Serif";
            settings["BackColor"] = Color.White.ToArgb().ToString();
            settings["WindowPosition"] = "0";   // Remember windows position

            settings["PlayMode"] = "9";     //Fixed time (1) & random sequence (8)

            settings["Sound"] = "1";        // Soundoff unchecked
            settings["Stats"] = "0";        // Stats unchecked

            settings["SplitterDistance"] = "265";
        }



        #endregion Application settings

        #region ChartUpdate routines
        /// <summary>
        /// Cleans and updates the chartStatsNumbers with the data returned from _game.GetStats
        /// </summary>
        private void ChartStatsNumbers_Update()
        {
            // Chart update
            this.chartStatsNumbers.Series["Right"].Points.Clear();
            this.chartStatsNumbers.Series["Wrong"].Points.Clear();
            _game.GetStats.Sort((x, y) => x.Number.CompareTo(y.Number));

            foreach (var num in _game.GetStats)
            {
                this.chartStatsNumbers.Series["Right"].Points.AddXY("#" + num.Number.ToString(), 100 * (float)num.Correct / num.Total);
                this.chartStatsNumbers.Series["Wrong"].Points.AddXY("#" + num.Number.ToString(), 100 * (1.0 - (float)num.Correct / num.Total));
            }
        }

        /// <summary>
        /// Cleans and updates the chartStatsNumbers with the data returned from _game.GetStats
        /// </summary>
        private void ChartStatsTime_Update()
        {
            // Chart update
            //this.chartStatsTime.Series.Clear();
            var data = _game.GetStatsTime;

            // Get the maximum number of columns
            int numSeries = data.Aggregate(0, (max, next) => next.Count > max ? next.Count : max);

            // Pad with 0 each row
            data.ForEach(x => x.AddRange(Enumerable.Repeat(0.0, numSeries - x.Count())));

            // Traspose to get the data ready to plot
            var plotData = data
                    .SelectMany(inner => inner.Select((item, index) => new { item, index }))
                    .GroupBy(i => i.index, i => i.item)
                    .Select(g => g.ToList())
                    .ToList();

            for (int i=0;i<numSeries; i++)
            {
                this.chartStatsTime.Series.Add(new Series()
                {
                    ChartType = SeriesChartType.StackedColumn
                });
                //this.chartStatsTime.Series[i].Points.AddXY
            }

        }
        #endregion ChartUpdate routines
    }
}
