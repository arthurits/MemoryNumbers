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
            this.toolStripMain_Graph = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMain_Settings = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMain_About = new System.Windows.Forms.ToolStripButton();
            this.roundButton1 = new Controls.RoundButton();
            this.countDown1 = new Controls.CountDown();
            this.board1 = new Controls.Board();
            this.statusStrip.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.board1)).BeginInit();
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
            this.toolStripMain_Graph,
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
            this.toolStripMain_Stop.ToolTipText = "Stop the game";
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
            this.toolStripMain_Sound.ToolTipText = "Game sounds on";
            this.toolStripMain_Sound.CheckedChanged += new System.EventHandler(this.toolStripMain_Sound_CheckedChanged);
            // 
            // toolStripMain_Graph
            // 
            this.toolStripMain_Graph.CheckOnClick = true;
            this.toolStripMain_Graph.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMain_Graph.Image")));
            this.toolStripMain_Graph.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMain_Graph.Name = "toolStripMain_Graph";
            this.toolStripMain_Graph.Size = new System.Drawing.Size(52, 67);
            this.toolStripMain_Graph.Text = "Graph";
            this.toolStripMain_Graph.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
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
            // roundButton1
            // 
            this.roundButton1.BackColor = System.Drawing.Color.Transparent;
            this.roundButton1.BorderColor = System.Drawing.Color.Black;
            this.roundButton1.BorderWidth = 10F;
            this.roundButton1.FillColor = System.Drawing.Color.Pink;
            this.roundButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roundButton1.Location = new System.Drawing.Point(75, 91);
            this.roundButton1.Name = "roundButton1";
            this.roundButton1.Size = new System.Drawing.Size(224, 144);
            this.roundButton1.TabIndex = 1;
            this.roundButton1.Text = "0";
            this.roundButton1.Visible = false;
            this.roundButton1.VisibleBorder = true;
            this.roundButton1.VisibleText = true;
            this.roundButton1.xRadius = 50F;
            this.roundButton1.yRadius = 50F;
            // 
            // countDown1
            // 
            this.countDown1.BackColor = System.Drawing.Color.Transparent;
            this.countDown1.BorderColor = System.Drawing.Color.Blue;
            this.countDown1.BorderWidth = 7F;
            this.countDown1.EndingTime = 0F;
            this.countDown1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.countDown1.Font = new System.Drawing.Font("Microsoft Sans Serif", 35F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.countDown1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.countDown1.Location = new System.Drawing.Point(403, 91);
            this.countDown1.Margin = new System.Windows.Forms.Padding(20, 18, 20, 18);
            this.countDown1.Name = "countDown1";
            this.countDown1.PlaySounds = false;
            this.countDown1.Size = new System.Drawing.Size(100, 100);
            this.countDown1.StartingTime = 5F;
            this.countDown1.TabIndex = 2;
            this.countDown1.Text = "5";
            this.countDown1.TimeInterval = 1000D;
            this.countDown1.Visible = false;
            this.countDown1.VisibleBorder = true;
            this.countDown1.VisibleText = true;
            this.countDown1.xRadius = 50F;
            this.countDown1.yRadius = 50F;
            // 
            // board1
            // 
            this.board1.BackColor = System.Drawing.Color.Azure;
            this.board1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.board1.BorderColor = System.Drawing.Color.Black;
            this.board1.BorderRatio = 0.115F;
            this.board1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.board1.CountDownRatio = 0.37F;
            this.board1.Diameter = 37;
            this.board1.FillColor = System.Drawing.Color.Transparent;
            this.board1.FontRatio = 0.6F;
            this.board1.Location = new System.Drawing.Point(0, 70);
            this.board1.MaxNumber = 9;
            this.board1.MinNumber = 0;
            this.board1.Name = "board1";
            this.board1.NumbersRatio = 0.25F;
            this.board1.PlaySounds = false;
            this.board1.ResultRatio = 0.56F;
            this.board1.Size = new System.Drawing.Size(287, 148);
            this.board1.TabIndex = 3;
            this.board1.TabStop = false;
            this.board1.Time = 700;
            this.board1.TimeIncrement = 300;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(538, 366);
            this.Controls.Add(this.board1);
            this.Controls.Add(this.roundButton1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.tspBottom);
            this.Controls.Add(this.tspTop);
            this.Controls.Add(this.countDown1);
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
            ((System.ComponentModel.ISupportInitialize)(this.board1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Controls.RoundButton roundButton1;
        private Controls.CountDown countDown1;
        private Controls.Board board1;

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
        private System.Windows.Forms.ToolStripButton toolStripMain_Graph;

    }
}

