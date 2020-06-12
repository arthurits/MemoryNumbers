using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryNumbers
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            // Set up string.
            string measureString = "5";
            Font stringFont = new Font("Arial", 40);

            // Measure string.
            SizeF stringSize = new SizeF();
            stringSize = e.Graphics.MeasureString(measureString, stringFont);

            // Draw rectangle representing size of string.
            e.Graphics.DrawRectangle(new Pen(Color.Red, 1), 0.0F, 0.0F, stringSize.Width, stringSize.Height);

            var stringFormat =
                new StringFormat(RightToLeft == RightToLeft.Yes ? StringFormatFlags.DirectionRightToLeft : 0)
                {
                    Alignment = StringAlignment.Near,     // Horizontal alignment
                    LineAlignment = StringAlignment.Near  // Vertical alignment
                };

            // Draw string to screen.
            //e.Graphics.DrawString(measureString, stringFont, Brushes.Black, new PointF(stringSize.Width/2, stringSize.Height/2), stringFormat);
            e.Graphics.DrawString(measureString, stringFont, Brushes.Black, new RectangleF(new PointF(0, stringFont.SizeInPoints * 0.09f), stringSize), stringFormat);
        }
    }
}
