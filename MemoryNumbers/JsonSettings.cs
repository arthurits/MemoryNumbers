using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace MemoryNumbers;

partial class FrmMain
{
    /// <summary>
    /// Loads all settings from file _sett.FileName into class instance _settings
    /// Shows MessageBox error if unsuccessful
    /// </summary>
    /// <returns><see langword="True"/> if successful, <see langword="false"/> otherwise</returns>
    private bool LoadProgramSettingsJSON()
    {
        bool result = false;
        try
        {
            var jsonString = File.ReadAllText(_settings.FileName);
            _settings = JsonSerializer.Deserialize<ClassSettings>(jsonString) ?? _settings;
            result = true;
        }
        catch (FileNotFoundException)
        {
        }
        catch (Exception ex)
        {
            using (new Utils.CenterWinDialog(this))
            {
                MessageBox.Show(this,
                    StringsRM.GetString("strErrorDeserialize", _settings.AppCulture) ?? $"Error loading settings file.\n\n{ex.Message}\n\nDefault values will be used instead.",
                    StringsRM.GetString("strErrorDeserializeTitle", _settings.AppCulture) ?? "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        return result;
    }

    /// <summary>
    /// Saves data from class instance _sett into _sett.FileName
    /// </summary>
    private void SaveProgramSettingsJSON()
    {
        _settings.WindowLeft = DesktopLocation.X;
        _settings.WindowTop = DesktopLocation.Y;
        _settings.WindowWidth = ClientSize.Width;
        _settings.WindowHeight = ClientSize.Height;

        _settings.Sound = this.toolStripMain_Sound.Checked;
        _settings.Stats = this.toolStripMain_Stats.Checked;

        _settings.SplitterDistance = this.splitStats.SplitterDistance;

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        var jsonString = JsonSerializer.Serialize(_settings, options);
        File.WriteAllText(_settings.FileName, jsonString);
    }

    /// <summary>
    /// Update UI with settings
    /// </summary>
    /// <param name="WindowSettings"><see langword="True"/> if the window position and size should be applied. <see langword="False"/> if omitted</param>
    private void ApplySettingsJSON(bool WindowPosition = false)
    {
        if (WindowPosition)
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.DesktopLocation = new Point(_settings.WindowLeft, _settings.WindowTop);
            this.ClientSize = new Size(_settings.WindowWidth, _settings.WindowHeight);
        }
        this.splitStats.SplitterDistance = _settings.SplitterDistance;

        // Game configuration
        this._game.MinimumLength = _settings.MinimumLength;
        this._game.MaximumAttempts = _settings.MaximumAttempts;
        this._game.MaximumDigit = _settings.MaximumDigit;
        this._game.MinimumDigit = _settings.MinimumDigit;
        this._game.PlayMode = (PlayMode)Enum.Parse(typeof(PlayMode), _settings.PlayMode.ToString());
        this._game.Time = _settings.Time;
        this._game.TimeIncrement = _settings.TimeIncrement;

        // Board configuration
        this.board1.BorderRatio = _settings.BorderRatio;
        this.board1.CountDownRatio = _settings.CountDownRatio;
        this.board1.NumbersRatio = _settings.NumbersRatio;
        this.board1.FontRatio = _settings.FontRatio;
        this.board1.ResultRatio = _settings.ResultsRatio;
        this.board1.BackColor = Color.FromArgb(_settings.BackColor);
        this.board1.Font = new Font(_settings.FontFamilyName, board1.Font.SizeInPoints);

        // Toolstrip status
        this.toolStripMain_Sound.Checked = _settings.Sound;
        this.toolStripMain_Stats.Checked = _settings.Stats;
        this.board1.PlaySounds = !this.toolStripMain_Sound.Checked;
    }
}

