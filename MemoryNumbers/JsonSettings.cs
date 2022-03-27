using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace MemoryNumbers;

partial class frmMain
{
    /// <summary>
    /// Loads all settings from file _sett.FileName into class instance _settings
    /// Shows MessageBox error if unsuccessful
    /// </summary>
    private void LoadProgramSettingsJSON()
    {
        try
        {
            var jsonString = File.ReadAllText(_settings.FileName);
            _settings = JsonSerializer.Deserialize<ClassSettings>(jsonString) ?? _settings;

            if (_settings.WindowPosition)
            {
                this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
                this.DesktopLocation = new Point(_settings.WindowLeft, _settings.WindowTop);
                this.ClientSize = new Size(_settings.WindowWidth, _settings.WindowHeight);
            }
            this.splitStats.SplitterDistance = _settings.SplitterDistance;
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

}

