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
            this.SuspendLayout();
            // 
            // roundButton1
            // 
            this.roundButton1.BackColor = System.Drawing.Color.Transparent;
            this.roundButton1.BorderColor = System.Drawing.Color.Black;
            this.roundButton1.BorderWidth = 5F;
            this.roundButton1.FillColor = System.Drawing.Color.Transparent;
            this.roundButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 80F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roundButton1.Location = new System.Drawing.Point(427, 104);
            this.roundButton1.Name = "roundButton1";
            this.roundButton1.RegionOffset = 1F;
            this.roundButton1.Size = new System.Drawing.Size(100, 100);
            this.roundButton1.TabIndex = 0;
            this.roundButton1.Text = "5";
            this.roundButton1.VisibleBorder = true;
            this.roundButton1.VisibleText = true;
            this.roundButton1.xRadius = 0F;
            this.roundButton1.yRadius = 0F;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.roundButton1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.RoundButton roundButton1;
    }
}