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

public partial class FrmMain : Form
{
    private readonly Game _game = new();
    private ClassSettings _settings = new();

    private readonly System.Resources.ResourceManager StringsRM = new("MemoryNumbers.localization.strings", typeof(FrmMain).Assembly);

    public FrmMain()
    {
        // Set form icon
        this.Icon = GraphicsResources.Load<Icon>(GraphicsResources.AppLogo);

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
        bool result = LoadProgramSettingsJSON();
        if (result)
            ApplySettingsJSON(_settings.WindowPosition);
        else
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

        this.toolStripMain_Exit.Image = new Icon(GraphicsResources.IconExit, 48, 48).ToBitmap();
        this.toolStripMain_Start.Image = new Icon(GraphicsResources.IconStart, 48, 48).ToBitmap();
        this.toolStripMain_Stop.Image = new Icon(GraphicsResources.IconStop, 48, 48).ToBitmap();
        this.toolStripMain_Sound.Image = new Icon(GraphicsResources.IconSound, 48, 48).ToBitmap();
        this.toolStripMain_Stats.Image = new Icon(GraphicsResources.IconStats, 48, 48).ToBitmap();
        this.toolStripMain_Settings.Image = new Icon(GraphicsResources.IconSettings, 48, 48).ToBitmap();
        this.toolStripMain_About.Image = new Icon(GraphicsResources.IconAbout, 48, 48).ToBitmap();
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

    private void Form1_Load(object? sender, EventArgs e)
    {
        // https://stackoverflow.com/questions/21632642/label-without-padding-and-margin
        
        // Creates the fade in animation of the form
        //Win32.Win32API.AnimateWindow(this.Handle, 200, Win32.Win32API.AnimateWindowFlags.AW_BLEND | Win32.Win32API.AnimateWindowFlags.AW_CENTER);
    }

    private void Form1_Shown(object? sender, EventArgs e)
    {
        // Send Close event
        using var closeSplashEvent = new System.Threading.EventWaitHandle(false, System.Threading.EventResetMode.ManualReset, "CloseSplashScreenEvent");
        closeSplashEvent.Set();
    }

    private void Form1_FormClosing(object? sender, FormClosingEventArgs e)
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

    private void OnButtonClick(object? sender, Board.ButtonClickEventArgs e)
    {
        if (_game.Check(e.ButtonValue, e.Seconds))
            board1.ButtonRight();
    }

    private async void OnCorrectSequence(object? sender, Game.CorrectEventArgs e)
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
                Stop_Click(null, null);
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
    private async void OnWrongSequence(object? sender, Game.WrongEventArgs e)
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
                Stop_Click(null, EventArgs.Empty);
                using (new CenterWinDialog(this))
                    MessageBox.Show("Could not place the buttons on the screen.\nPlease, try reducing the 'numbers ratio' paremeter in\nthe Settings (between 0.25 - 0.30).", "Error placing numbers", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }

    private void OnGameOver(object? sender, Game.OverEventArgs e)
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
}
