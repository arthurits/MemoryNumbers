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
            this.numTime.Value = Convert.ToInt32(_settings["Time"]);
            this.numMaxDigit.Value = Convert.ToInt32(_settings["MaximumDigit"]);
            this.numMinDigit.Value = Convert.ToInt32(_settings["MinimumDigit"]);
            this.numMaxAttempts.Value = Convert.ToInt32(_settings["MaximumAttempts"]);

            this.numCountRatio.Value = Convert.ToDecimal(_settings["CountDownRatio"]);
            this.numNumbersRatio.Value = Convert.ToDecimal(_settings["NumbersRatio"]);
            //this.numBorderRatio.Value = Convert.ToDecimal(_settings["BorderRatio"]);

            settings = _settings;
            
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            settings["Time"] = this.numTime.Value.ToString();
            settings["MaximumDigit"] = this.numMaxDigit.Value.ToString();
            settings["MinimumDigit"] = this.numMinDigit.Value.ToString();
            settings["MaximumAttempts"] = this.numMaxAttempts.Value.ToString();

            settings["CountDownRatio"] = this.numCountRatio.Value.ToString();
            settings["NumbersRatio"] = this.numNumbersRatio.Value.ToString();
            //settings["BorderRatio"] = this.numBorderRatio.Value.ToString();

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
