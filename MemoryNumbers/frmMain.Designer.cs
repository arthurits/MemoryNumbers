namespace MemoryNumbers
{
    partial class frmMain
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components is not null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.tspTop = new System.Windows.Forms.ToolStripPanel();
            this.tspBottom = new System.Windows.Forms.ToolStripPanel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_Secuence = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripMain_Exit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMain_Start = new System.Windows.Forms.ToolStripButton();
            this.toolStripMain_Stop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMain_Sound = new System.Windows.Forms.ToolStripButton();
            this.toolStripMain_Stats = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMain_Settings = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMain_About = new System.Windows.Forms.ToolStripButton();
            this.tabGame = new System.Windows.Forms.TabControl();
            this.tabBoard = new System.Windows.Forms.TabPage();
            this.board1 = new Controls.Board();
            this.tabStats = new System.Windows.Forms.TabPage();
            this.splitStats = new System.Windows.Forms.SplitContainer();
            this.StatsNumbers = new ScottPlot.FormsPlot();
            this.StatsTime = new ScottPlot.FormsPlot();
            this.statusStrip.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.tabGame.SuspendLayout();
            this.tabBoard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.board1)).BeginInit();
            this.tabStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitStats)).BeginInit();
            this.splitStats.Panel1.SuspendLayout();
            this.splitStats.Panel2.SuspendLayout();
            this.splitStats.SuspendLayout();
            this.SuspendLayout();
            // 
            // tspTop
            // 
            this.tspTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.tspTop.Location = new System.Drawing.Point(0, 0);
            this.tspTop.Name = "tspTop";
            this.tspTop.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.tspTop.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.tspTop.Size = new System.Drawing.Size(628, 0);
            // 
            // tspBottom
            // 
            this.tspBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tspBottom.Location = new System.Drawing.Point(0, 422);
            this.tspBottom.Name = "tspBottom";
            this.tspBottom.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.tspBottom.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.tspBottom.Size = new System.Drawing.Size(628, 0);
            // 
            // statusStrip
            // 
            this.statusStrip.AutoSize = false;
            this.statusStrip.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_Secuence});
            this.statusStrip.Location = new System.Drawing.Point(0, 393);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip.Size = new System.Drawing.Size(628, 29);
            this.statusStrip.TabIndex = 2;
            // 
            // toolStripStatusLabel_Secuence
            // 
            this.toolStripStatusLabel_Secuence.AutoSize = false;
            this.toolStripStatusLabel_Secuence.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabel_Secuence.Name = "toolStripStatusLabel_Secuence";
            this.toolStripStatusLabel_Secuence.Size = new System.Drawing.Size(50, 24);
            // 
            // toolStripMain
            // 
            this.toolStripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripMain.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMain_Exit,
            this.toolStripSeparator1,
            this.toolStripMain_Start,
            this.toolStripMain_Stop,
            this.toolStripSeparator2,
            this.toolStripMain_Sound,
            this.toolStripMain_Stats,
            this.toolStripSeparator3,
            this.toolStripMain_Settings,
            this.toolStripSeparator4,
            this.toolStripMain_About});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStripMain.Size = new System.Drawing.Size(628, 70);
            this.toolStripMain.TabIndex = 0;
            // 
            // toolStripMain_Exit
            // 
            this.toolStripMain_Exit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMain_Exit.Image")));
            this.toolStripMain_Exit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMain_Exit.Name = "toolStripMain_Exit";
            this.toolStripMain_Exit.Size = new System.Drawing.Size(52, 67);
            this.toolStripMain_Exit.Text = "Exit";
            this.toolStripMain_Exit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripMain_Exit.ToolTipText = "Exit application";
            this.toolStripMain_Exit.Click += new System.EventHandler(this.toolStripMain_Exit_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 70);
            // 
            // toolStripMain_Start
            // 
            this.toolStripMain_Start.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMain_Start.Image")));
            this.toolStripMain_Start.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMain_Start.Name = "toolStripMain_Start";
            this.toolStripMain_Start.Size = new System.Drawing.Size(52, 67);
            this.toolStripMain_Start.Text = "Start";
            this.toolStripMain_Start.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripMain_Start.ToolTipText = "Start the game";
            this.toolStripMain_Start.Click += new System.EventHandler(this.toolStripMain_Start_Click);
            // 
            // toolStripMain_Stop
            // 
            this.toolStripMain_Stop.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMain_Stop.Image")));
            this.toolStripMain_Stop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMain_Stop.Name = "toolStripMain_Stop";
            this.toolStripMain_Stop.Size = new System.Drawing.Size(52, 67);
            this.toolStripMain_Stop.Text = "Stop";
            this.toolStripMain_Stop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripMain_Stop.ToolTipText = "Stop the game and reset";
            this.toolStripMain_Stop.Click += new System.EventHandler(this.toolStripMain_Stop_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 70);
            // 
            // toolStripMain_Sound
            // 
            this.toolStripMain_Sound.Checked = true;
            this.toolStripMain_Sound.CheckOnClick = true;
            this.toolStripMain_Sound.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMain_Sound.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMain_Sound.Image")));
            this.toolStripMain_Sound.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMain_Sound.Name = "toolStripMain_Sound";
            this.toolStripMain_Sound.Size = new System.Drawing.Size(52, 67);
            this.toolStripMain_Sound.Text = "Sound";
            this.toolStripMain_Sound.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripMain_Sound.ToolTipText = "Game sounds off";
            this.toolStripMain_Sound.CheckedChanged += new System.EventHandler(this.toolStripMain_Sound_CheckedChanged);
            // 
            // toolStripMain_Stats
            // 
            this.toolStripMain_Stats.CheckOnClick = true;
            this.toolStripMain_Stats.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMain_Stats.Image")));
            this.toolStripMain_Stats.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMain_Stats.Name = "toolStripMain_Stats";
            this.toolStripMain_Stats.Size = new System.Drawing.Size(52, 67);
            this.toolStripMain_Stats.Text = "Stats";
            this.toolStripMain_Stats.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripMain_Stats.ToolTipText = "Show stats plot";
            this.toolStripMain_Stats.CheckedChanged += new System.EventHandler(this.toolStripMain_Stats_CheckedChanged);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 70);
            // 
            // toolStripMain_Settings
            // 
            this.toolStripMain_Settings.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMain_Settings.Image")));
            this.toolStripMain_Settings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMain_Settings.Name = "toolStripMain_Settings";
            this.toolStripMain_Settings.Size = new System.Drawing.Size(53, 67);
            this.toolStripMain_Settings.Text = "Settings";
            this.toolStripMain_Settings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripMain_Settings.Click += new System.EventHandler(this.toolStripMain_Settings_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 70);
            // 
            // toolStripMain_About
            // 
            this.toolStripMain_About.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMain_About.Image")));
            this.toolStripMain_About.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMain_About.Name = "toolStripMain_About";
            this.toolStripMain_About.Size = new System.Drawing.Size(52, 67);
            this.toolStripMain_About.Text = "About";
            this.toolStripMain_About.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripMain_About.ToolTipText = "About this software";
            this.toolStripMain_About.Click += new System.EventHandler(this.toolStripMain_About_Click);
            // 
            // tabGame
            // 
            this.tabGame.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabGame.Controls.Add(this.tabBoard);
            this.tabGame.Controls.Add(this.tabStats);
            this.tabGame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabGame.ItemSize = new System.Drawing.Size(0, 1);
            this.tabGame.Location = new System.Drawing.Point(0, 70);
            this.tabGame.Margin = new System.Windows.Forms.Padding(0);
            this.tabGame.Name = "tabGame";
            this.tabGame.Padding = new System.Drawing.Point(0, 0);
            this.tabGame.SelectedIndex = 0;
            this.tabGame.Size = new System.Drawing.Size(628, 323);
            this.tabGame.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabGame.TabIndex = 1;
            // 
            // tabBoard
            // 
            this.tabBoard.Controls.Add(this.board1);
            this.tabBoard.Location = new System.Drawing.Point(4, 5);
            this.tabBoard.Margin = new System.Windows.Forms.Padding(0);
            this.tabBoard.Name = "tabBoard";
            this.tabBoard.Size = new System.Drawing.Size(620, 314);
            this.tabBoard.TabIndex = 0;
            this.tabBoard.Text = "Board";
            this.tabBoard.UseVisualStyleBackColor = true;
            // 
            // board1
            // 
            this.board1.BackColor = System.Drawing.Color.White;
            this.board1.BorderColor = System.Drawing.Color.Black;
            this.board1.BorderRatio = 0.12F;
            this.board1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.board1.CountDownRatio = 0.37F;
            this.board1.Diameter = 78;
            this.board1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.board1.FillColor = System.Drawing.Color.White;
            this.board1.FontRatio = 0.6F;
            this.board1.Location = new System.Drawing.Point(0, 0);
            this.board1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.board1.Name = "board1";
            this.board1.NumbersRatio = 0.25F;
            this.board1.PlaySounds = true;
            this.board1.ResultRatio = 0.56F;
            this.board1.Size = new System.Drawing.Size(620, 314);
            this.board1.TabIndex = 11;
            this.board1.TabStop = false;
            this.board1.Time = 700;
            // 
            // tabStats
            // 
            this.tabStats.BackColor = System.Drawing.SystemColors.Control;
            this.tabStats.Controls.Add(this.splitStats);
            this.tabStats.Location = new System.Drawing.Point(4, 5);
            this.tabStats.Margin = new System.Windows.Forms.Padding(0);
            this.tabStats.Name = "tabStats";
            this.tabStats.Size = new System.Drawing.Size(620, 314);
            this.tabStats.TabIndex = 1;
            this.tabStats.Text = "Stats";
            // 
            // splitStats
            // 
            this.splitStats.BackColor = System.Drawing.Color.Silver;
            this.splitStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitStats.Location = new System.Drawing.Point(0, 0);
            this.splitStats.Margin = new System.Windows.Forms.Padding(0);
            this.splitStats.Name = "splitStats";
            // 
            // splitStats.Panel1
            // 
            this.splitStats.Panel1.Controls.Add(this.StatsNumbers);
            // 
            // splitStats.Panel2
            // 
            this.splitStats.Panel2.Controls.Add(this.StatsTime);
            this.splitStats.Size = new System.Drawing.Size(620, 314);
            this.splitStats.SplitterDistance = 310;
            this.splitStats.SplitterWidth = 1;
            this.splitStats.TabIndex = 0;
            // 
            // StatsNumbers
            // 
            this.StatsNumbers.BackColor = System.Drawing.Color.White;
            this.StatsNumbers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StatsNumbers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StatsNumbers.Location = new System.Drawing.Point(0, 0);
            this.StatsNumbers.Margin = new System.Windows.Forms.Padding(0);
            this.StatsNumbers.Name = "StatsNumbers";
            this.StatsNumbers.Size = new System.Drawing.Size(310, 314);
            this.StatsNumbers.TabIndex = 1;
            // 
            // StatsTime
            // 
            this.StatsTime.BackColor = System.Drawing.Color.White;
            this.StatsTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StatsTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StatsTime.Location = new System.Drawing.Point(0, 0);
            this.StatsTime.Margin = new System.Windows.Forms.Padding(0);
            this.StatsTime.Name = "StatsTime";
            this.StatsTime.Size = new System.Drawing.Size(309, 314);
            this.StatsTime.TabIndex = 2;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(628, 422);
            this.Controls.Add(this.tabGame);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.tspBottom);
            this.Controls.Add(this.tspTop);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(639, 456);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Memory numbers";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.tabGame.ResumeLayout(false);
            this.tabBoard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.board1)).EndInit();
            this.tabStats.ResumeLayout(false);
            this.splitStats.Panel1.ResumeLayout(false);
            this.splitStats.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitStats)).EndInit();
            this.splitStats.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        
        private System.Windows.Forms.ToolStripPanel tspTop;
        private System.Windows.Forms.ToolStripPanel tspBottom;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton toolStripMain_Exit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripMain_Start;
        private System.Windows.Forms.ToolStripButton toolStripMain_Stop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripMain_Settings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripMain_About;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_Secuence;
        private System.Windows.Forms.ToolStripButton toolStripMain_Sound;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripMain_Stats;
        private System.Windows.Forms.TabControl tabGame;
        private System.Windows.Forms.TabPage tabBoard;
        private System.Windows.Forms.TabPage tabStats;
        private System.Windows.Forms.SplitContainer splitStats;
        private ScottPlot.FormsPlot StatsNumbers;
        private ScottPlot.FormsPlot StatsTime;
        private Controls.Board board1;
    }
}

