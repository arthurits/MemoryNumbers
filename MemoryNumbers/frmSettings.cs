using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Utils;

namespace MemoryNumbers;

public partial class FrmSettings : Form
{
    public ClassSettings Settings { get; private set; }

    public FrmSettings()
    {
        InitializeComponent();

        // Set form icons and images
        this.Icon = GraphicsResources.Load<Icon>(GraphicsResources.IconSettings);
    }

    public FrmSettings(ClassSettings settings)
        : this()
    {
        UpdateControls(settings);
    }   

    private void Accept_Click(object sender, EventArgs e)
    {
        Settings.Time = Convert.ToInt32(numTime.Value);
        Settings.TimeIncrement = Convert.ToInt32(numTimeIncrement.Value);
        Settings.MaximumDigit = Convert.ToInt32(numMaxDigit.Value);
        Settings.MinimumDigit = Convert.ToInt32(numMinDigit.Value);
        Settings.MaximumAttempts = Convert.ToInt32(numMaxAttempts.Value);
        Settings.MinimumLength = Convert.ToInt32(numMinLength.Value);

        Settings.CountDownRatio = Convert.ToSingle(numCountRatio.Value);
        Settings.NumbersRatio = Convert.ToSingle(numNumbersRatio.Value);
        Settings.BorderRatio = Convert.ToSingle(numBorderRatio.Value);
        Settings.FontRatio = Convert.ToSingle(numFontRatio.Value);
        Settings.ResultsRatio = Convert.ToSingle(numResultsRatio.Value);
        Settings.BackColor = pctBackColor.BackColor.ToArgb();
        Settings.FontFamilyName = lblFontFamily.Text.Remove(0, 6);
        Settings.WindowPosition=chkStartUp.Checked;
        Settings.PlayMode = (this.radFixed.Checked ? 1 : 0) * 1 +
                             (this.radIncremental.Checked ? 1 : 0) * 2 +
                             (this.radProgressive.Checked ? 1 : 0) * 4 +
                             (this.radRandom.Checked ? 1 : 0) * 8;

        Close(); 
    }

    private void Reset_Click(object sender, EventArgs e)
    {
        // Ask for overriding confirmation
        DialogResult DlgResult;
        using (new CenterWinDialog(this))
        {
            DlgResult = MessageBox.Show(this, "You are about to override the actual settings\n" +
                                            "with the default values.\n\n" +
                                            "Are you sure you want to continue?",
                                            "Override settings",
                                            MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
        }

        // If "Yes", then reset values to default
        if (DlgResult == DialogResult.Yes)
        {
            UpdateControls(new ClassSettings());
            Settings.SplitterDistance = this.Parent.Width / 2;
            //_settings["SplitterDistance"] = "0.5";
        }
    }

    /// <summary>
    /// Updates the form's controls with values from the settings class
    /// </summary>
    /// <param name="settings">Class containing the values to show on the form's controls</param>
    private void UpdateControls(ClassSettings settings)
    {
        Settings = settings;

        try
        {
            PlayMode play = (PlayMode)Settings.PlayMode;
            this.radProgressive.Checked = ((play & PlayMode.SequenceProgressive) == PlayMode.SequenceProgressive);
            this.radRandom.Checked = ((play & PlayMode.SequenceRandom) == PlayMode.SequenceRandom);
            this.radFixed.Checked = ((play & PlayMode.TimeFixed) == PlayMode.TimeFixed);
            this.radIncremental.Checked = ((play & PlayMode.TimeIncremental) == PlayMode.TimeIncremental);
            this.numTimeIncrement.Enabled = this.radIncremental.Checked;
            this.trackTimeIncrement.Enabled = this.radIncremental.Checked;

            this.numTime.Value = Settings.Time;
            this.numTimeIncrement.Value = Settings.TimeIncrement;

            int value = Settings.MaximumDigit;
            this.numMaxDigit.Value = value > numMaxDigit.Maximum ? numMaxDigit.Maximum : (value < numMaxDigit.Minimum ? numMaxDigit.Minimum : value);
            this.numMinDigit.Maximum = this.numMaxDigit.Value;
            value = Settings.MinimumDigit;
            this.numMinDigit.Value = value > numMinDigit.Maximum ? numMinDigit.Maximum : (value < numMinDigit.Minimum ? numMinDigit.Minimum : value);

            this.numMaxAttempts.Value = Settings.MaximumAttempts;
            this.numMinLength.Value = Settings.MinimumLength;

            this.numCountRatio.Value = Convert.ToDecimal(Settings.CountDownRatio);
            this.numNumbersRatio.Value = Convert.ToDecimal(Settings.NumbersRatio);
            this.numBorderRatio.Value = Convert.ToDecimal(Settings.BorderRatio);
            this.numFontRatio.Value = Convert.ToDecimal(Settings.FontRatio);
            this.numResultsRatio.Value = Convert.ToDecimal(Settings.ResultsRatio);
            this.pctBackColor.BackColor = Color.FromArgb(Settings.BackColor);
            this.chkStartUp.Checked = Settings.WindowPosition;
            this.roundSample.Font = new Font(Settings.FontFamilyName, roundSample.Font.SizeInPoints);
            this.lblFontFamily.Text = "Font: " + this.roundSample.Font.FontFamily.Name;
        }
        catch (Exception)
        {
            using (new CenterWinDialog(this))
                MessageBox.Show(this, "Unexpected error while applying settings.\nPlease report the error to the engineer.", "Settings error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        roundSample.Invalidate();
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
        roundSample.Invalidate();
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
        ColorDialog ColorPicker = new();
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
        roundSample.Invalidate();
    }

    private void numMaxDigit_ValueChanged(object sender, EventArgs e)
    {
        roundSample.Text = numMaxDigit.Value.ToString();
        roundSample.Invalidate();
        numMinDigit.Maximum = numMaxDigit.Value;
    }

    private void btnFontFamily_Click(object sender, EventArgs e)
    {
        FontDialog frmFont = new()
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
                roundSample.Font = new Font(frmFont.Font.FontFamily, this.roundSample.Font.SizeInPoints);
                roundSample.Invalidate();
                lblFontFamily.Text = "Font: " + frmFont.Font.FontFamily.Name;
            }
        }
        // https://stackoverflow.com/questions/2207709/convert-font-to-string-and-back-again
    }


    // https://stackoverrun.com/es/q/1915473
}
