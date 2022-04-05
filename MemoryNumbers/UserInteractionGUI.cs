using System;
using System.Windows.Forms;
using Utils;

namespace MemoryNumbers;

partial class FrmMain
{
    private void Exit_Click(object sender, EventArgs e)
    {
        Close();
    }

    private async void Start_Click(object sender, EventArgs e)
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
                Stop_Click(null, null);
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

    private void Stop_Click(object sender, EventArgs e)
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

    private void Sound_CheckedChanged(object sender, EventArgs e)
    {
        this.board1.PlaySounds = !toolStripMain_Sound.Checked;
    }

    private void Stats_CheckedChanged(object sender, EventArgs e)
    {
        this.tabGame.SelectedIndex = toolStripMain_Stats.Checked ? 1 : 0;
    }

    private void Settings_Click(object sender, EventArgs e)
    {
        FrmSettings form = new(_settings);
        form.ShowDialog(this);
        if (form.DialogResult == DialogResult.OK)
        {
            _settings = form.Settings;
            ApplySettingsJSON();
        }
    }

    private void About_Click(object sender, EventArgs e)
    {
        FrmAbout form = new();
        form.ShowDialog(this);
    }

}
