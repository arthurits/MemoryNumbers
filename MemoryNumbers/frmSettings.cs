using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utils;

namespace MemoryNumbers
{
    public partial class frmSettings : Form
    {
        // Internal variables
        private ProgramSettings<string, string> _settings;
        private ProgramSettings<string, string> _defaultSettings;

        // Public interface
        public ProgramSettings<string, string> GetSettings => _settings;

        // Constructors
        public frmSettings()
        {
            InitializeComponent();

            // Set form icons and images
            var path = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            if (System.IO.File.Exists(path + @"\images\settings.ico")) this.Icon = new Icon(path + @"\images\settings.ico");
        }

        public frmSettings(ProgramSettings<string, string> settings, ProgramSettings<string, string> defSets)
            :this()
        {
            
            try
            {
                PlayMode play = (PlayMode)Convert.ToInt32(settings.ContainsKey("PlayMode") ? settings["PlayMode"] : defSets["PlayMode"]);
                this.radProgressive.Checked = ((play & PlayMode.SequenceProgressive) == PlayMode.SequenceProgressive);
                this.radRandom.Checked = ((play & PlayMode.SequenceRandom) == PlayMode.SequenceRandom);
                this.radFixed.Checked = ((play & PlayMode.TimeFixed) == PlayMode.TimeFixed);
                this.radIncremental.Checked = ((play & PlayMode.TimeIncremental) == PlayMode.TimeIncremental);
                this.numTimeIncrement.Enabled = this.radIncremental.Checked;
                this.trackTimeIncrement.Enabled = this.radIncremental.Checked;

                this.numTime.Value = Convert.ToInt32(settings.ContainsKey("Time") ? settings["Time"] : defSets["Time"]);
                this.numTimeIncrement.Value = Convert.ToInt32(settings.ContainsKey("TimeIncrement") ? settings["TimeIncrement"] : defSets["TimeIncrement"]);
                this.numMaxDigit.Value = Convert.ToInt32(settings.ContainsKey("MaximumDigit") ? settings["MaximumDigit"] : defSets["MaximumDigit"]);
                this.numMinDigit.Value = Convert.ToInt32(settings.ContainsKey("MinimumDigit") ? settings["MinimumDigit"] : defSets["MinimumDigit"]);
                this.numMaxAttempts.Value = Convert.ToInt32(settings.ContainsKey("MaximumAttempts") ? settings["MaximumAttempts"] : defSets["MaximumAttempts"]);
                this.numMinLength.Value = Convert.ToInt32(settings.ContainsKey("MinimumLength") ? settings["MinimumLength"] : defSets["MinimumLength"]);

                this.numCountRatio.Value = Convert.ToDecimal(settings.ContainsKey("CountDownRatio") ? settings["CountDownRatio"]: defSets["CountDownRatio"], System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                this.numNumbersRatio.Value = Convert.ToDecimal(settings.ContainsKey("NumbersRatio") ? settings["NumbersRatio"] : defSets["NumbersRatio"], System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                this.numBorderRatio.Value = Convert.ToDecimal(settings.ContainsKey("BorderRatio") ? settings["BorderRatio"] : defSets["BorderRatio"], System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                this.numFontRatio.Value = Convert.ToDecimal(settings.ContainsKey("FontRatio") ? settings["FontRatio"] : defSets["FontRatio"], System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                this.numResultsRatio.Value = Convert.ToDecimal(settings.ContainsKey("ResultsRatio") ? settings["ResultsRatio"] : defSets["ResultsRatio"], System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                this.pctBackColor.BackColor = Color.FromArgb(Convert.ToInt32(settings.ContainsKey("BackColor") ? settings["BackColor"] : defSets["BackColor"]));
                this.chkStartUp.Checked = Convert.ToInt32(settings.ContainsKey("WindowPosition") ? settings["WindowPosition"] : defSets["WindowPosition"]) == 1 ? true : false;
                this.roundSample.Font = new Font(settings.ContainsKey("FontFamilyName") ? settings["FontFamilyName"] : defSets["FontFamilyName"], roundSample.Font.SizeInPoints);
                this.lblFontFamily.Text = this.roundSample.Font.FontFamily.Name;
            }
            catch (KeyNotFoundException e)
            {
                using (new CenterWinDialog(this))
                    MessageBox.Show(this, "Unexpected error while applying settings.\nPlease report the error to the engineer.", "Settings error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            //ApplySettings(_settings);
            _settings = settings;
            _defaultSettings = defSets;
            //_programSettings.ContainsKey("Sound") ?

        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            _settings["Time"] = this.numTime.Value.ToString();
            _settings["TimeIncrement"] = this.numTimeIncrement.Value.ToString();
            _settings["MaximumDigit"] = this.numMaxDigit.Value.ToString();
            _settings["MinimumDigit"] = this.numMinDigit.Value.ToString();
            _settings["MaximumAttempts"] = this.numMaxAttempts.Value.ToString();
            _settings["MinimumLength"] = this.numMinLength.Value.ToString();

            _settings["CountDownRatio"] = this.numCountRatio.Value.ToString(System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
            _settings["NumbersRatio"] = this.numNumbersRatio.Value.ToString(System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
            _settings["BorderRatio"] = this.numBorderRatio.Value.ToString(System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
            _settings["FontRatio"] = this.numFontRatio.Value.ToString(System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
            _settings["ResultsRatio"] = this.numResultsRatio.Value.ToString(System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
            _settings["BackColor"] = this.pctBackColor.BackColor.ToArgb().ToString();
            _settings["WindowPosition"] = (this.chkStartUp.Checked ? 1 : 0).ToString();
            _settings["FontFamilyName"] = this.lblFontFamily.Text;

            _settings["PlayMode"] = (
                                    (this.radFixed.Checked ? 1 : 0) * 1 +
                                    (this.radIncremental.Checked ? 1 : 0) * 2 +
                                    (this.radProgressive.Checked ? 1 : 0) * 4 +
                                    (this.radRandom.Checked ? 1 : 0) * 8
                                    ).ToString();

            Close(); 
        }

        private void ApplySettings(ProgramSettings<string, string> _settings)
        {
            try
            {
                PlayMode play = (PlayMode)Convert.ToInt32(_settings["PlayMode"]);
                this.radProgressive.Checked = ((play & PlayMode.SequenceProgressive) == PlayMode.SequenceProgressive);
                this.radRandom.Checked = ((play & PlayMode.SequenceRandom) == PlayMode.SequenceRandom);
                this.radFixed.Checked = ((play & PlayMode.TimeFixed) == PlayMode.TimeFixed);
                this.radIncremental.Checked = ((play & PlayMode.TimeIncremental) == PlayMode.TimeIncremental);
                this.numTimeIncrement.Enabled = this.radIncremental.Checked;
                this.trackTimeIncrement.Enabled = this.radIncremental.Checked;

                this.numTime.Value = Convert.ToInt32(_settings["Time"]);
                this.numTimeIncrement.Value = Convert.ToInt32(_settings["TimeIncrement"]);
                this.numMaxDigit.Value = Convert.ToInt32(_settings["MaximumDigit"]);
                this.numMinDigit.Value = Convert.ToInt32(_settings["MinimumDigit"]);
                this.numMaxAttempts.Value = Convert.ToInt32(_settings["MaximumAttempts"]);
                this.numMinLength.Value = Convert.ToInt32(_settings["MinimumLength"]);

                this.numCountRatio.Value = Convert.ToDecimal(_settings["CountDownRatio"], System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                this.numNumbersRatio.Value = Convert.ToDecimal(_settings["NumbersRatio"], System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                this.numBorderRatio.Value = Convert.ToDecimal(_settings["BorderRatio"], System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                this.numFontRatio.Value = Convert.ToDecimal(_settings["FontRatio"], System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                this.numResultsRatio.Value = Convert.ToDecimal(_settings["ResultsRatio"], System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                this.pctBackColor.BackColor = Color.FromArgb(Convert.ToInt32(this._settings["BackColor"]));
                this.chkStartUp.Checked = Convert.ToInt32(_settings["WindowPosition"]) == 1 ? true : false;
                this.roundSample.Font = new Font(_settings["FontFamilyName"], roundSample.Font.SizeInPoints);
                this.lblFontFamily.Text = this.roundSample.Font.FontFamily.Name;

            }
            catch (KeyNotFoundException e)
            {
                using (new CenterWinDialog(this))
                {
                    MessageBox.Show(this,
                        "Unexpected error while applying settings.\nPlease report the error to the engineer.",
                        "Settings error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // Ask for overriding confirmation
            DialogResult result;
            using (new CenterWinDialog(this))
            {
                result = MessageBox.Show(this, "You are about to override the actual settings\n" +
                                                "with the default values.\n\n" +
                                                "Are you sure you want to continue?",
                                                "Override settings",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            }

            // If "Yes", then reset values to default
            if (result == DialogResult.Yes)
            {
                ApplySettings(_defaultSettings);
                _settings["SplitterDistance"] = "0.5";
            }
        }

        private void trackTime_ValueChanged(object sender, EventArgs e)
        {
            if (numTime.Value != trackTime.Value) numTime.Value = trackTime.Value;
        }

        private void numTime_ValueChanged(object sender, EventArgs e)
        {
            if (trackTime.Value != (int)numTime.Value) trackTime.Value = Convert.ToInt32(numTime.Value);
        }

        private void trackTimeIncrement_ValueChanged(object sender, EventArgs e)
        {
            if (numTimeIncrement.Value != trackTimeIncrement.Value) numTimeIncrement.Value = trackTimeIncrement.Value;
        }

        private void numTimeIncrement_ValueChanged(object sender, EventArgs e)
        {
            if (trackTimeIncrement.Value != (int)numTimeIncrement.Value) trackTimeIncrement.Value = Convert.ToInt32(numTimeIncrement.Value);
        }

        private void trackCountRatio_ValueChanged(object sender, EventArgs e)
        {
            decimal ratio = Decimal.Round((decimal)trackCountRatio.Value / 100, 2, MidpointRounding.AwayFromZero);
            if (numCountRatio.Value != ratio) numCountRatio.Value = ratio;
        }

        private void numCountRatio_ValueChanged(object sender, EventArgs e)
        {
            int ratio = Convert.ToInt32(100 * numCountRatio.Value);
            if (trackCountRatio.Value != ratio) trackCountRatio.Value = ratio;
        }

        private void trackNumbersRatio_ValueChanged(object sender, EventArgs e)
        {
            decimal ratio = Decimal.Round((decimal)trackNumbersRatio.Value / 100, 2, MidpointRounding.AwayFromZero);
            if (numNumbersRatio.Value != ratio) numNumbersRatio.Value = ratio;
        }

        private void numNumbersRatio_ValueChanged(object sender, EventArgs e)
        {
            int ratio = Convert.ToInt32(100 * numNumbersRatio.Value);
            if (trackNumbersRatio.Value != ratio) trackNumbersRatio.Value = ratio;
        }

        private void trackBorderRatio_ValueChanged(object sender, EventArgs e)
        {
            decimal ratio = Decimal.Round((decimal)trackBorderRatio.Value / 100, 2, MidpointRounding.AwayFromZero);
            if (numBorderRatio.Value != ratio) numBorderRatio.Value = ratio;
        }

        private void numBorderRatio_ValueChanged(object sender, EventArgs e)
        {
            int ratio = Convert.ToInt32(100 * numBorderRatio.Value);
            if (trackBorderRatio.Value != ratio) trackBorderRatio.Value = ratio;

            roundSample.BorderWidth = ((roundSample.Width - roundSample.RegionOffset) / 2) * (float)numBorderRatio.Value;
        }

        private void trackFontRatio_ValueChanged(object sender, EventArgs e)
        {
            decimal ratio = Decimal.Round((decimal)trackFontRatio.Value / 100, 2, MidpointRounding.AwayFromZero);
            if (numFontRatio.Value != ratio) numFontRatio.Value = ratio;
        }

        private void numFontRatio_ValueChanged(object sender, EventArgs e)
        {
            int ratio = Convert.ToInt32(100 * numFontRatio.Value);
            if (trackFontRatio.Value != ratio) trackFontRatio.Value = ratio;

            //roundSample.Font = new Font(roundSample.Font.FontFamily, (float)numFontRatio.Value * (roundSample.Width - 2 * roundSample.BorderWidth));
            roundSample.Font = new Font(roundSample.Font.FontFamily, (float)numFontRatio.Value * roundSample.Width);
        }

        private void trackResultsRatio_ValueChanged(object sender, EventArgs e)
        {
            decimal ratio = Decimal.Round((decimal)trackResultsRatio.Value / 100, 2, MidpointRounding.AwayFromZero);
            if (numResultsRatio.Value != ratio) numResultsRatio.Value = ratio;
        }

        private void numResultsRatio_ValueChanged(object sender, EventArgs e)
        {
            int ratio = Convert.ToInt32(100 * numResultsRatio.Value);
            if (trackResultsRatio.Value != ratio) trackResultsRatio.Value = ratio;
        }

        private void radIncremental_CheckedChanged(object sender, EventArgs e)
        {
            if (radIncremental.Checked==true)
            {
                numTimeIncrement.Enabled = true;
                trackTimeIncrement.Enabled = true;
            }
            else
            {
                numTimeIncrement.Enabled = false;
                trackTimeIncrement.Enabled = false;
            }
        }

        private void pctBackColor_Click(object sender, EventArgs e)
        {
            ColorDialog ColorPicker = new ColorDialog();
            ColorPicker.AllowFullOpen = true;
            ColorPicker.AnyColor = true;
            ColorPicker.FullOpen = true;
            ColorPicker.SolidColorOnly = false;
            ColorPicker.ShowHelp = true;
            
            // Sets the initial color select to the current text color.
            ColorPicker.Color = pctBackColor.BackColor;
            using (new CenterWinDialog(this.Owner))
            {
                // Update the text box color if the user clicks OK 
                if (ColorPicker.ShowDialog() == DialogResult.OK)
                    pctBackColor.BackColor = ColorPicker.Color;
            }
        }

        private void pctBackColor_BackColorChanged(object sender, EventArgs e)
        {
            pctSample.BackColor = pctBackColor.BackColor;
            roundSample.BackColor = pctBackColor.BackColor;
        }

        private void numMaxDigit_ValueChanged(object sender, EventArgs e)
        {
            roundSample.Text = numMaxDigit.Value.ToString();
        }

        private void lblFontFamily_DoubleClick(object sender, EventArgs e)
        {
            FontDialog frmFont = new FontDialog()
            {
                FontMustExist = true,
                Font = roundSample.Font,
                ShowApply = false,
                ShowColor = false,
                ShowEffects = false,
                ShowHelp = false
            };

            using (new CenterWinDialog(this))
            {
                if (frmFont.ShowDialog() == DialogResult.OK)
                {
                    this.roundSample.Font = new Font(frmFont.Font.FontFamily, this.roundSample.Font.SizeInPoints);
                    this.lblFontFamily.Text = frmFont.Font.FontFamily.Name;
                }
            }
            // https://stackoverflow.com/questions/2207709/convert-font-to-string-and-back-again
        }


        // https://stackoverrun.com/es/q/1915473
    }
}
