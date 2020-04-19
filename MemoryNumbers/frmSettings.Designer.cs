namespace MemoryNumbers
{
    partial class frmSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabSettings = new System.Windows.Forms.TabControl();
            this.tabSettingsGame = new System.Windows.Forms.TabPage();
            this.numMaxDigit = new System.Windows.Forms.NumericUpDown();
            this.numMinDigit = new System.Windows.Forms.NumericUpDown();
            this.numMaxAttempts = new System.Windows.Forms.NumericUpDown();
            this.numTime = new System.Windows.Forms.NumericUpDown();
            this.lblMaxDigit = new System.Windows.Forms.Label();
            this.lblMinDigit = new System.Windows.Forms.Label();
            this.lblMaxAttempts = new System.Windows.Forms.Label();
            this.trackTime = new System.Windows.Forms.TrackBar();
            this.lblTime = new System.Windows.Forms.Label();
            this.tabGUI = new System.Windows.Forms.TabPage();
            this.trackBorderRatio = new System.Windows.Forms.TrackBar();
            this.trackNumbersRatio = new System.Windows.Forms.TrackBar();
            this.trackCountRatio = new System.Windows.Forms.TrackBar();
            this.numBorderRatio = new System.Windows.Forms.NumericUpDown();
            this.numNumbersRatio = new System.Windows.Forms.NumericUpDown();
            this.numCountRatio = new System.Windows.Forms.NumericUpDown();
            this.lblBorderRatio = new System.Windows.Forms.Label();
            this.lblNumbersRatio = new System.Windows.Forms.Label();
            this.lblCountRatio = new System.Windows.Forms.Label();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.radSequential = new System.Windows.Forms.RadioButton();
            this.radRandom = new System.Windows.Forms.RadioButton();
            this.radFixed = new System.Windows.Forms.RadioButton();
            this.radIncremental = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabSettings.SuspendLayout();
            this.tabSettingsGame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxDigit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinDigit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxAttempts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackTime)).BeginInit();
            this.tabGUI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBorderRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackNumbersRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackCountRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBorderRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNumbersRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCountRatio)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.tabPage1);
            this.tabSettings.Controls.Add(this.tabSettingsGame);
            this.tabSettings.Controls.Add(this.tabGUI);
            this.tabSettings.Location = new System.Drawing.Point(16, 15);
            this.tabSettings.Margin = new System.Windows.Forms.Padding(4);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.SelectedIndex = 0;
            this.tabSettings.Size = new System.Drawing.Size(575, 345);
            this.tabSettings.TabIndex = 0;
            // 
            // tabSettingsGame
            // 
            this.tabSettingsGame.BackColor = System.Drawing.Color.White;
            this.tabSettingsGame.Controls.Add(this.numMaxDigit);
            this.tabSettingsGame.Controls.Add(this.numMinDigit);
            this.tabSettingsGame.Controls.Add(this.numMaxAttempts);
            this.tabSettingsGame.Controls.Add(this.numTime);
            this.tabSettingsGame.Controls.Add(this.lblMaxDigit);
            this.tabSettingsGame.Controls.Add(this.lblMinDigit);
            this.tabSettingsGame.Controls.Add(this.lblMaxAttempts);
            this.tabSettingsGame.Controls.Add(this.trackTime);
            this.tabSettingsGame.Controls.Add(this.lblTime);
            this.tabSettingsGame.Location = new System.Drawing.Point(4, 25);
            this.tabSettingsGame.Margin = new System.Windows.Forms.Padding(4);
            this.tabSettingsGame.Name = "tabSettingsGame";
            this.tabSettingsGame.Padding = new System.Windows.Forms.Padding(4);
            this.tabSettingsGame.Size = new System.Drawing.Size(567, 316);
            this.tabSettingsGame.TabIndex = 0;
            this.tabSettingsGame.Text = "Game";
            // 
            // numMaxDigit
            // 
            this.numMaxDigit.Location = new System.Drawing.Point(179, 167);
            this.numMaxDigit.Name = "numMaxDigit";
            this.numMaxDigit.Size = new System.Drawing.Size(50, 23);
            this.numMaxDigit.TabIndex = 12;
            // 
            // numMinDigit
            // 
            this.numMinDigit.Location = new System.Drawing.Point(179, 133);
            this.numMinDigit.Name = "numMinDigit";
            this.numMinDigit.Size = new System.Drawing.Size(50, 23);
            this.numMinDigit.TabIndex = 11;
            // 
            // numMaxAttempts
            // 
            this.numMaxAttempts.Location = new System.Drawing.Point(179, 99);
            this.numMaxAttempts.Name = "numMaxAttempts";
            this.numMaxAttempts.Size = new System.Drawing.Size(50, 23);
            this.numMaxAttempts.TabIndex = 10;
            // 
            // numTime
            // 
            this.numTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numTime.Location = new System.Drawing.Point(125, 39);
            this.numTime.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numTime.Name = "numTime";
            this.numTime.Size = new System.Drawing.Size(61, 23);
            this.numTime.TabIndex = 9;
            this.numTime.ValueChanged += new System.EventHandler(this.numTime_ValueChanged);
            // 
            // lblMaxDigit
            // 
            this.lblMaxDigit.AutoSize = true;
            this.lblMaxDigit.Location = new System.Drawing.Point(49, 169);
            this.lblMaxDigit.Name = "lblMaxDigit";
            this.lblMaxDigit.Size = new System.Drawing.Size(100, 17);
            this.lblMaxDigit.TabIndex = 4;
            this.lblMaxDigit.Text = "Maximum digit:";
            // 
            // lblMinDigit
            // 
            this.lblMinDigit.AutoSize = true;
            this.lblMinDigit.Location = new System.Drawing.Point(49, 135);
            this.lblMinDigit.Name = "lblMinDigit";
            this.lblMinDigit.Size = new System.Drawing.Size(97, 17);
            this.lblMinDigit.TabIndex = 3;
            this.lblMinDigit.Text = "Minimum digit:";
            // 
            // lblMaxAttempts
            // 
            this.lblMaxAttempts.AutoSize = true;
            this.lblMaxAttempts.Location = new System.Drawing.Point(49, 101);
            this.lblMaxAttempts.Name = "lblMaxAttempts";
            this.lblMaxAttempts.Size = new System.Drawing.Size(124, 17);
            this.lblMaxAttempts.TabIndex = 2;
            this.lblMaxAttempts.Text = "Maximum attemps:";
            // 
            // trackTime
            // 
            this.trackTime.LargeChange = 50;
            this.trackTime.Location = new System.Drawing.Point(196, 39);
            this.trackTime.Maximum = 2000;
            this.trackTime.Name = "trackTime";
            this.trackTime.Size = new System.Drawing.Size(269, 45);
            this.trackTime.SmallChange = 10;
            this.trackTime.TabIndex = 1;
            this.trackTime.TickFrequency = 100;
            this.trackTime.ValueChanged += new System.EventHandler(this.trackTime_ValueChanged);
            // 
            // lblTime
            // 
            this.lblTime.Location = new System.Drawing.Point(49, 39);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(75, 17);
            this.lblTime.TabIndex = 0;
            this.lblTime.Text = "Time (ms):";
            // 
            // tabGUI
            // 
            this.tabGUI.BackColor = System.Drawing.Color.White;
            this.tabGUI.Controls.Add(this.trackBorderRatio);
            this.tabGUI.Controls.Add(this.trackNumbersRatio);
            this.tabGUI.Controls.Add(this.trackCountRatio);
            this.tabGUI.Controls.Add(this.numBorderRatio);
            this.tabGUI.Controls.Add(this.numNumbersRatio);
            this.tabGUI.Controls.Add(this.numCountRatio);
            this.tabGUI.Controls.Add(this.lblBorderRatio);
            this.tabGUI.Controls.Add(this.lblNumbersRatio);
            this.tabGUI.Controls.Add(this.lblCountRatio);
            this.tabGUI.Location = new System.Drawing.Point(4, 25);
            this.tabGUI.Margin = new System.Windows.Forms.Padding(4);
            this.tabGUI.Name = "tabGUI";
            this.tabGUI.Padding = new System.Windows.Forms.Padding(4);
            this.tabGUI.Size = new System.Drawing.Size(567, 316);
            this.tabGUI.TabIndex = 1;
            this.tabGUI.Text = "Interface";
            // 
            // trackBorderRatio
            // 
            this.trackBorderRatio.Location = new System.Drawing.Point(239, 106);
            this.trackBorderRatio.Maximum = 100;
            this.trackBorderRatio.Name = "trackBorderRatio";
            this.trackBorderRatio.Size = new System.Drawing.Size(226, 45);
            this.trackBorderRatio.TabIndex = 11;
            this.trackBorderRatio.TickFrequency = 10;
            this.trackBorderRatio.ValueChanged += new System.EventHandler(this.trackBorderRatio_ValueChanged);
            // 
            // trackNumbersRatio
            // 
            this.trackNumbersRatio.Location = new System.Drawing.Point(239, 71);
            this.trackNumbersRatio.Maximum = 100;
            this.trackNumbersRatio.Name = "trackNumbersRatio";
            this.trackNumbersRatio.Size = new System.Drawing.Size(226, 45);
            this.trackNumbersRatio.TabIndex = 10;
            this.trackNumbersRatio.TickFrequency = 10;
            this.trackNumbersRatio.ValueChanged += new System.EventHandler(this.trackNumbersRatio_ValueChanged);
            // 
            // trackCountRatio
            // 
            this.trackCountRatio.Location = new System.Drawing.Point(239, 36);
            this.trackCountRatio.Maximum = 100;
            this.trackCountRatio.Name = "trackCountRatio";
            this.trackCountRatio.Size = new System.Drawing.Size(226, 45);
            this.trackCountRatio.TabIndex = 9;
            this.trackCountRatio.TickFrequency = 10;
            this.trackCountRatio.ValueChanged += new System.EventHandler(this.trackCountRatio_ValueChanged);
            // 
            // numBorderRatio
            // 
            this.numBorderRatio.DecimalPlaces = 2;
            this.numBorderRatio.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numBorderRatio.Location = new System.Drawing.Point(171, 106);
            this.numBorderRatio.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numBorderRatio.Name = "numBorderRatio";
            this.numBorderRatio.Size = new System.Drawing.Size(55, 23);
            this.numBorderRatio.TabIndex = 8;
            this.numBorderRatio.ValueChanged += new System.EventHandler(this.numBorderRatio_ValueChanged);
            // 
            // numNumbersRatio
            // 
            this.numNumbersRatio.DecimalPlaces = 2;
            this.numNumbersRatio.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numNumbersRatio.Location = new System.Drawing.Point(171, 71);
            this.numNumbersRatio.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numNumbersRatio.Name = "numNumbersRatio";
            this.numNumbersRatio.Size = new System.Drawing.Size(55, 23);
            this.numNumbersRatio.TabIndex = 7;
            this.numNumbersRatio.ValueChanged += new System.EventHandler(this.numNumbersRatio_ValueChanged);
            // 
            // numCountRatio
            // 
            this.numCountRatio.DecimalPlaces = 2;
            this.numCountRatio.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numCountRatio.Location = new System.Drawing.Point(171, 36);
            this.numCountRatio.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCountRatio.Name = "numCountRatio";
            this.numCountRatio.Size = new System.Drawing.Size(55, 23);
            this.numCountRatio.TabIndex = 6;
            this.numCountRatio.ValueChanged += new System.EventHandler(this.numCountRatio_ValueChanged);
            // 
            // lblBorderRatio
            // 
            this.lblBorderRatio.AutoSize = true;
            this.lblBorderRatio.Location = new System.Drawing.Point(52, 108);
            this.lblBorderRatio.Name = "lblBorderRatio";
            this.lblBorderRatio.Size = new System.Drawing.Size(87, 17);
            this.lblBorderRatio.TabIndex = 2;
            this.lblBorderRatio.Text = "Border ratio:";
            // 
            // lblNumbersRatio
            // 
            this.lblNumbersRatio.AutoSize = true;
            this.lblNumbersRatio.Location = new System.Drawing.Point(52, 73);
            this.lblNumbersRatio.Name = "lblNumbersRatio";
            this.lblNumbersRatio.Size = new System.Drawing.Size(101, 17);
            this.lblNumbersRatio.TabIndex = 1;
            this.lblNumbersRatio.Text = "Numbers ratio:";
            // 
            // lblCountRatio
            // 
            this.lblCountRatio.AutoSize = true;
            this.lblCountRatio.Location = new System.Drawing.Point(52, 38);
            this.lblCountRatio.Name = "lblCountRatio";
            this.lblCountRatio.Size = new System.Drawing.Size(114, 17);
            this.lblCountRatio.TabIndex = 0;
            this.lblCountRatio.Text = "Countdown ratio:";
            // 
            // btnAccept
            // 
            this.btnAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAccept.Location = new System.Drawing.Point(385, 367);
            this.btnAccept.Margin = new System.Windows.Forms.Padding(4);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(100, 28);
            this.btnAccept.TabIndex = 1;
            this.btnAccept.Text = "&Accept";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(492, 367);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(567, 316);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Play mode";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // radSequential
            // 
            this.radSequential.AutoSize = true;
            this.radSequential.Location = new System.Drawing.Point(35, 42);
            this.radSequential.Name = "radSequential";
            this.radSequential.Size = new System.Drawing.Size(92, 21);
            this.radSequential.TabIndex = 0;
            this.radSequential.TabStop = true;
            this.radSequential.Text = "Ascending";
            this.radSequential.UseVisualStyleBackColor = true;
            // 
            // radRandom
            // 
            this.radRandom.AutoSize = true;
            this.radRandom.Location = new System.Drawing.Point(35, 90);
            this.radRandom.Name = "radRandom";
            this.radRandom.Size = new System.Drawing.Size(79, 21);
            this.radRandom.TabIndex = 1;
            this.radRandom.TabStop = true;
            this.radRandom.Text = "Random";
            this.radRandom.UseVisualStyleBackColor = true;
            // 
            // radFixed
            // 
            this.radFixed.AutoSize = true;
            this.radFixed.Location = new System.Drawing.Point(47, 42);
            this.radFixed.Name = "radFixed";
            this.radFixed.Size = new System.Drawing.Size(89, 21);
            this.radFixed.TabIndex = 2;
            this.radFixed.TabStop = true;
            this.radFixed.Text = "Fixed time";
            this.radFixed.UseVisualStyleBackColor = true;
            // 
            // radIncremental
            // 
            this.radIncremental.AutoSize = true;
            this.radIncremental.Location = new System.Drawing.Point(47, 90);
            this.radIncremental.Name = "radIncremental";
            this.radIncremental.Size = new System.Drawing.Size(129, 21);
            this.radIncremental.TabIndex = 3;
            this.radIncremental.TabStop = true;
            this.radIncremental.Text = "Incremental time";
            this.radIncremental.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radSequential);
            this.groupBox1.Controls.Add(this.radRandom);
            this.groupBox1.Location = new System.Drawing.Point(28, 53);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(188, 143);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Numeric sequence";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radFixed);
            this.groupBox2.Controls.Add(this.radIncremental);
            this.groupBox2.Location = new System.Drawing.Point(267, 53);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(237, 143);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Time";
            // 
            // frmSettings
            // 
            this.AcceptButton = this.btnAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(604, 407);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.tabSettings);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MemoryNumbers - Settings";
            this.tabSettings.ResumeLayout(false);
            this.tabSettingsGame.ResumeLayout(false);
            this.tabSettingsGame.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxDigit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinDigit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxAttempts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackTime)).EndInit();
            this.tabGUI.ResumeLayout(false);
            this.tabGUI.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBorderRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackNumbersRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackCountRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBorderRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNumbersRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCountRatio)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabSettings;
        private System.Windows.Forms.TabPage tabSettingsGame;
        private System.Windows.Forms.TabPage tabGUI;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblMaxDigit;
        private System.Windows.Forms.Label lblMinDigit;
        private System.Windows.Forms.Label lblMaxAttempts;
        private System.Windows.Forms.TrackBar trackTime;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblBorderRatio;
        private System.Windows.Forms.Label lblNumbersRatio;
        private System.Windows.Forms.Label lblCountRatio;
        private System.Windows.Forms.NumericUpDown numTime;
        private System.Windows.Forms.NumericUpDown numMaxDigit;
        private System.Windows.Forms.NumericUpDown numMinDigit;
        private System.Windows.Forms.NumericUpDown numMaxAttempts;
        private System.Windows.Forms.TrackBar trackCountRatio;
        private System.Windows.Forms.NumericUpDown numBorderRatio;
        private System.Windows.Forms.NumericUpDown numNumbersRatio;
        private System.Windows.Forms.NumericUpDown numCountRatio;
        private System.Windows.Forms.TrackBar trackBorderRatio;
        private System.Windows.Forms.TrackBar trackNumbersRatio;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radFixed;
        private System.Windows.Forms.RadioButton radIncremental;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radSequential;
        private System.Windows.Forms.RadioButton radRandom;
    }
}