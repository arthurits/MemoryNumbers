namespace MemoryNumbers
{
    partial class Form1
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
            if (disposing && (components != null))
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
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
            this.chartStatsNumbers = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartStatsTime = new System.Windows.Forms.DataVisualization.Charting.Chart();
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
            ((System.ComponentModel.ISupportInitialize)(this.chartStatsNumbers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartStatsTime)).BeginInit();
            this.SuspendLayout();
            // 
            // tspTop
            // 
            this.tspTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.tspTop.Location = new System.Drawing.Point(0, 0);
            this.tspTop.Name = "tspTop";
            this.tspTop.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.tspTop.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.tspTop.Size = new System.Drawing.Size(538, 0);
            // 
            // tspBottom
            // 
            this.tspBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tspBottom.Location = new System.Drawing.Point(0, 366);
            this.tspBottom.Name = "tspBottom";
            this.tspBottom.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.tspBottom.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.tspBottom.Size = new System.Drawing.Size(538, 0);
            // 
            // statusStrip
            // 
            this.statusStrip.AutoSize = false;
            this.statusStrip.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_Secuence});
            this.statusStrip.Location = new System.Drawing.Point(0, 341);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip.Size = new System.Drawing.Size(538, 25);
            this.statusStrip.TabIndex = 4;
            // 
            // toolStripStatusLabel_Secuence
            // 
            this.toolStripStatusLabel_Secuence.AutoSize = false;
            this.toolStripStatusLabel_Secuence.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabel_Secuence.Name = "toolStripStatusLabel_Secuence";
            this.toolStripStatusLabel_Secuence.Size = new System.Drawing.Size(50, 20);
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
            this.toolStripMain.Size = new System.Drawing.Size(538, 70);
            this.toolStripMain.TabIndex = 3;
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
            this.tabGame.Size = new System.Drawing.Size(538, 271);
            this.tabGame.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabGame.TabIndex = 7;
            // 
            // tabBoard
            // 
            this.tabBoard.Controls.Add(this.board1);
            this.tabBoard.Location = new System.Drawing.Point(4, 5);
            this.tabBoard.Margin = new System.Windows.Forms.Padding(0);
            this.tabBoard.Name = "tabBoard";
            this.tabBoard.Size = new System.Drawing.Size(530, 262);
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
            this.board1.Diameter = 65;
            this.board1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.board1.FillColor = System.Drawing.Color.White;
            this.board1.FontRatio = 0.6F;
            this.board1.Location = new System.Drawing.Point(0, 0);
            this.board1.Name = "board1";
            this.board1.NumbersRatio = 0.25F;
            this.board1.PlaySounds = true;
            this.board1.ResultRatio = 0.56F;
            this.board1.Size = new System.Drawing.Size(530, 262);
            this.board1.TabIndex = 11;
            this.board1.TabStop = false;
            this.board1.Time = 700;
            // 
            // tabStats
            // 
            this.tabStats.Controls.Add(this.splitStats);
            this.tabStats.Location = new System.Drawing.Point(4, 5);
            this.tabStats.Margin = new System.Windows.Forms.Padding(0);
            this.tabStats.Name = "tabStats";
            this.tabStats.Size = new System.Drawing.Size(530, 262);
            this.tabStats.TabIndex = 1;
            this.tabStats.Text = "Stats";
            this.tabStats.UseVisualStyleBackColor = true;
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
            this.splitStats.Panel1.Controls.Add(this.chartStatsNumbers);
            // 
            // splitStats.Panel2
            // 
            this.splitStats.Panel2.Controls.Add(this.chartStatsTime);
            this.splitStats.Size = new System.Drawing.Size(530, 262);
            this.splitStats.SplitterDistance = 265;
            this.splitStats.SplitterWidth = 1;
            this.splitStats.TabIndex = 0;
            // 
            // chartStatsNumbers
            // 
            this.chartStatsNumbers.BorderlineColor = System.Drawing.Color.Black;
            this.chartStatsNumbers.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea1.AxisY.Title = "Percentage";
            chartArea1.Name = "ChartArea1";
            this.chartStatsNumbers.ChartAreas.Add(chartArea1);
            this.chartStatsNumbers.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.chartStatsNumbers.Legends.Add(legend1);
            this.chartStatsNumbers.Location = new System.Drawing.Point(0, 0);
            this.chartStatsNumbers.Name = "chartStatsNumbers";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn100;
            series1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(0)))));
            series1.Legend = "Legend1";
            series1.Name = "Right";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn100;
            series2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            series2.Legend = "Legend1";
            series2.Name = "Wrong";
            this.chartStatsNumbers.Series.Add(series1);
            this.chartStatsNumbers.Series.Add(series2);
            this.chartStatsNumbers.Size = new System.Drawing.Size(265, 262);
            this.chartStatsNumbers.TabIndex = 0;
            this.chartStatsNumbers.Text = "chartStatsNumber";
            // 
            // chartStatsTime
            // 
            this.chartStatsTime.BorderlineColor = System.Drawing.Color.Black;
            this.chartStatsTime.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea2.AxisY.Title = "Time in seconds";
            chartArea2.Name = "ChartArea1";
            this.chartStatsTime.ChartAreas.Add(chartArea2);
            this.chartStatsTime.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Enabled = false;
            legend2.Name = "Legend1";
            this.chartStatsTime.Legends.Add(legend2);
            this.chartStatsTime.Location = new System.Drawing.Point(0, 0);
            this.chartStatsTime.Name = "chartStatsTime";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            series3.Legend = "Legend1";
            series3.Name = "Time";
            this.chartStatsTime.Series.Add(series3);
            this.chartStatsTime.Size = new System.Drawing.Size(264, 262);
            this.chartStatsTime.TabIndex = 0;
            this.chartStatsTime.Text = "chartStatsTime";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(538, 366);
            this.Controls.Add(this.tabGame);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.tspBottom);
            this.Controls.Add(this.tspTop);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(550, 400);
            this.Name = "Form1";
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
            ((System.ComponentModel.ISupportInitialize)(this.chartStatsNumbers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartStatsTime)).EndInit();
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
        private System.Windows.Forms.DataVisualization.Charting.Chart chartStatsNumbers;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartStatsTime;
        private Controls.Board board1;
    }
}

