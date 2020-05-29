namespace MemoryNumbers
{
    partial class Form2
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
            this.roundButton1 = new Controls.RoundButton();
            this.countDown1 = new Controls.CountDown();
            this.SuspendLayout();
            // 
            // roundButton1
            // 
            this.roundButton1.BackColor = System.Drawing.Color.Transparent;
            this.roundButton1.BorderColor = System.Drawing.Color.Black;
            this.roundButton1.BorderWidth = 10F;
            this.roundButton1.FillColor = System.Drawing.Color.Transparent;
            this.roundButton1.Location = new System.Drawing.Point(497, 145);
            this.roundButton1.Name = "roundButton1";
            this.roundButton1.RegionOffset = 1F;
            this.roundButton1.Size = new System.Drawing.Size(150, 150);
            this.roundButton1.TabIndex = 0;
            this.roundButton1.Text = "roundButton1";
            this.roundButton1.VisibleBorder = true;
            this.roundButton1.VisibleText = true;
            this.roundButton1.xRadius = 75F;
            this.roundButton1.yRadius = 75F;
            // 
            // countDown1
            // 
            this.countDown1.BackColor = System.Drawing.Color.Transparent;
            this.countDown1.BorderColor = System.Drawing.Color.Black;
            this.countDown1.BorderWidth = 5F;
            this.countDown1.EndingTime = 0F;
            this.countDown1.FillColor = System.Drawing.Color.Transparent;
            this.countDown1.Location = new System.Drawing.Point(310, 43);
            this.countDown1.Name = "countDown1";
            this.countDown1.PlaySounds = false;
            this.countDown1.RegionOffset = 1F;
            this.countDown1.Size = new System.Drawing.Size(100, 100);
            this.countDown1.StartingTime = 0F;
            this.countDown1.TabIndex = 1;
            this.countDown1.Text = "countDown1";
            this.countDown1.TimeInterval = 1000D;
            this.countDown1.VisibleBorder = true;
            this.countDown1.VisibleText = true;
            this.countDown1.xRadius = 50F;
            this.countDown1.yRadius = 50F;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.countDown1);
            this.Controls.Add(this.roundButton1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.RoundButton roundButton1;
        private Controls.CountDown countDown1;
    }
}