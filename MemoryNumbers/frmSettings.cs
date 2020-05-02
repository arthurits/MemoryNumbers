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
        // The return value
        public ProgramSettings<string, string> settings;

        public frmSettings()
        {
            InitializeComponent();

            // Set form icons and images
            var path = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            if (System.IO.File.Exists(path + @"\images\settings.ico")) this.Icon = new Icon(path + @"\images\settings.ico");
        }

        public frmSettings(ProgramSettings<string, string> _settings)
            :this()
        {
            try
            {
                PlayMode play = (PlayMode)Convert.ToInt32(_settings["PlayMode"]);
                this.radProgressive.Checked = ((play & PlayMode.SequenceProgressive) == PlayMode.SequenceProgressive);
                this.radRandom.Checked = ((play & PlayMode.SequenceRandom) == PlayMode.SequenceRandom);
                this.radFixed.Checked = ((play & PlayMode.TimeFixed) == PlayMode.TimeFixed);
                this.radIncremental.Checked = ((play & PlayMode.TimeIncremental) == PlayMode.TimeIncremental);
                
                this.numTime.Value = Convert.ToInt32(_settings["Time"]);
                this.numTimeIncrement.Value = Convert.ToInt32(_settings["TimeIncrement"]);
                this.numMaxDigit.Value = Convert.ToInt32(_settings["MaximumDigit"]);
                this.numMinDigit.Value = Convert.ToInt32(_settings["MinimumDigit"]);
                this.numMaxAttempts.Value = Convert.ToInt32(_settings["MaximumAttempts"]);

                this.numCountRatio.Value = Convert.ToDecimal(_settings["CountDownRatio"]);
                this.numNumbersRatio.Value = Convert.ToDecimal(_settings["NumbersRatio"]);
                this.numBorderRatio.Value = Convert.ToDecimal(_settings["BorderRatio"]);
                this.numFontRatio.Value = Convert.ToDecimal(_settings["FontRatio"]);
                this.numResultsRatio.Value = Convert.ToDecimal(_settings["ResultsRatio"]);
                this.checkBox1.Checked = Convert.ToInt32(_settings["WindowPosition"]) == 1 ? true : false;

            }
            catch (KeyNotFoundException e)
            {
                
            }

            settings = _settings;
            //_programSettings.ContainsKey("Sound") ?

        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            settings["Time"] = this.numTime.Value.ToString();
            settings["TimeIncrement"] = this.radIncremental.Checked ? this.numTimeIncrement.Value.ToString() : "0";
            settings["MaximumDigit"] = this.numMaxDigit.Value.ToString();
            settings["MinimumDigit"] = this.numMinDigit.Value.ToString();
            settings["MaximumAttempts"] = this.numMaxAttempts.Value.ToString();

            settings["CountDownRatio"] = this.numCountRatio.Value.ToString();
            settings["NumbersRatio"] = this.numNumbersRatio.Value.ToString();
            settings["BorderRatio"] = this.numBorderRatio.Value.ToString();
            settings["FontRatio"] = this.numFontRatio.Value.ToString();
            settings["ResultsRatio"] = this.numResultsRatio.Value.ToString();
            settings["WindowPosition"] = (this.checkBox1.Checked ? 1 : 0).ToString();

            settings["PlayMode"] = (
                                    (this.radFixed.Checked ? 1 : 0) * 1 +
                                    (this.radIncremental.Checked ? 1 : 0) * 2 +
                                    (this.radProgressive.Checked ? 1 : 0) * 4 +
                                    (this.radRandom.Checked ? 1 : 0) * 8
                                    ).ToString();

            Close(); 
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


        // https://stackoverrun.com/es/q/1915473
    }
}
