using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using Controls;
using Utils;

namespace MemoryNumbers;

public partial class frmMain : Form
{
    private readonly Game _game = new();
    private ClassSettings _settings = new();

    private readonly System.Resources.ResourceManager StringsRM = new("MemoryNumbers.localization.strings", typeof(frmMain).Assembly);

    public frmMain()
    {
        // Set form icon
        if (File.Exists(_settings.AppPath + @"\images\logo.ico")) this.Icon = new Icon(_settings.AppPath + @"\images\logo.ico");

        // Initialize components
        InitializeComponent();
        InitializeToolStripPanel();
        InitializeToolStrip();
        InitializeMenuStrip();
        InitializeStatusStrip();

        // Initialization extras
        splitStats.SplitterWidth = 1;   // This is a known bug

        // Subscribe to events
        board1.ButtonClick += new EventHandler<Board.ButtonClickEventArgs>(OnButtonClick);

        _game.CorrectSequence += new EventHandler<Game.CorrectEventArgs>(OnCorrectSequence);
        _game.WrongSequence += new EventHandler<Game.WrongEventArgs>(OnWrongSequence);
        _game.GameOver += new EventHandler<Game.OverEventArgs>(OnGameOver);
        //_game.GameOver += async (object s, Game.OverEventArgs e) => await OnGameOver(s, e);
        
        // Load and apply the program settings
        LoadProgramSettingsJSON();
        ApplySettingsJSON();
    }

    #region Initialization routines
    
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

        if (File.Exists(_settings.AppPath + @"\images\exit.ico")) this.toolStripMain_Exit.Image = new Icon(_settings.AppPath + @"\images\exit.ico", 48, 48).ToBitmap();
        if (File.Exists(_settings.AppPath + @"\images\start.ico")) this.toolStripMain_Start.Image = new Icon(_settings.AppPath + @"\images\start.ico", 48, 48).ToBitmap();
        if (File.Exists(_settings.AppPath + @"\images\stop.ico")) this.toolStripMain_Stop.Image = new Icon(_settings.AppPath + @"\images\stop.ico", 48, 48).ToBitmap();
        if (File.Exists(_settings.AppPath + @"\images\soundoff.ico")) this.toolStripMain_Sound.Image = new Icon(_settings.AppPath + @"\images\soundoff.ico", 48, 48).ToBitmap();
        if (File.Exists(_settings.AppPath + @"\images\graph.ico")) this.toolStripMain_Stats.Image = new Icon(_settings.AppPath + @"\images\graph.ico", 48, 48).ToBitmap();
        if (File.Exists(_settings.AppPath + @"\images\settings.ico")) this.toolStripMain_Settings.Image = new Icon(_settings.AppPath + @"\images\settings.ico", 48, 48).ToBitmap();
        if (File.Exists(_settings.AppPath + @"\images\about.ico")) this.toolStripMain_About.Image = new Icon(_settings.AppPath + @"\images\about.ico", 48, 48).ToBitmap();

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

    #endregion Initialization routines

    #region Form events

    private void Form1_Load(object sender, EventArgs e)
    {
        // https://stackoverflow.com/questions/21632642/label-without-padding-and-margin
        
        // Creates the fade in animation of the form
        //Win32.Win32API.AnimateWindow(this.Handle, 200, Win32.Win32API.AnimateWindowFlags.AW_BLEND | Win32.Win32API.AnimateWindowFlags.AW_CENTER);
    }

    private void Form1_Shown(object sender, EventArgs e)
    {
        // Send Close event
        using var closeSplashEvent = new System.Threading.EventWaitHandle(false, System.Threading.EventResetMode.ManualReset, "CloseSplashScreenEvent");
        closeSplashEvent.Set();
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

        // Save settings data
        SaveProgramSettingsJSON();
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
        if (_game.Check(e.ButtonValue, e.Seconds))
            board1.ButtonRight();
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
        ChartStatsTime_Update();
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
        FrmSettings form = new(_settings);
        form.ShowDialog(this);
        if (form.DialogResult == DialogResult.OK)
        {
            _settings = form.Settings;
            ApplySettingsJSON();
        }
    }

    private void toolStripMain_About_Click(object sender, EventArgs e)
    {
        frmAbout form = new();
        form.ShowDialog(this);
    }

    #endregion toolStripMain

    #region Application settings

    /// <summary>
    /// Update UI with settings
    /// </summary>
    /// <param name="WindowSettings">True if the window position and size should be applied. False if omitted</param>
    private void ApplySettingsJSON()
    {
        //if (_settings.WindowPosition)
        //{
        //    //var startPos = this.StartPosition;
        //    this.StartPosition = FormStartPosition.Manual;
        //    this.DesktopLocation = new Point(_settings.WindowLeft, _settings.WindowTop);
        //    this.ClientSize = new Size(_settings.WindowWidth, _settings.WindowHeight);
        //    //this.StartPosition = startPos;
        //    this.splitStats.SplitterDistance = _settings.SplitterDistance;
        //}

        this._game.MinimumLength = _settings.MinimumLength;
        this._game.MaximumAttempts = _settings.MaximumAttempts;
        this._game.MaximumDigit = _settings.MaximumDigit;
        this._game.MinimumDigit = _settings.MinimumDigit;
        this._game.PlayMode = (PlayMode)Enum.Parse(typeof(PlayMode), _settings.PlayMode.ToString());
        this._game.Time = _settings.Time;
        this._game.TimeIncrement = _settings.TimeIncrement;

        this.board1.BorderRatio = _settings.BorderRatio;
        this.board1.CountDownRatio = _settings.CountDownRatio;
        this.board1.NumbersRatio = _settings.NumbersRatio;
        this.board1.FontRatio = _settings.FontRatio;
        this.board1.ResultRatio = _settings.ResultsRatio;
        this.board1.BackColor = Color.FromArgb(_settings.BackColor);
        this.board1.Font = new Font(_settings.FontFamilyName, board1.Font.SizeInPoints);

        this.toolStripMain_Sound.Checked = _settings.Sound;
        this.toolStripMain_Stats.Checked = _settings.Stats;
        this.board1.PlaySounds = !this.toolStripMain_Sound.Checked;
    }

    /// <summary>
    /// Updates the UI language of all controls
    /// </summary>
    private void UpdateUI_Language()
    {

    }


    #endregion Application settings

}
