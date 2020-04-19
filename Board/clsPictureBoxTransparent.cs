using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;

// https://social.msdn.microsoft.com/Forums/office/en-US/5d8220b8-a7fa-4b49-8567-7a39da8f79b7/vb2010-how-can-i-make-the-picturebox-realy-transparent?forum=vbgeneral
namespace Controls
{
    public class PictureBoxTransparent: UserControl
    {
        string _strImagePath;
        public PictureBoxTransparent(string ImagePath)
        {
            _strImagePath = ImagePath;
        }

        protected override void OnLoad(EventArgs e)
        {
            this.DoubleBuffered = true;
            this.BackgroundImageLayout = ImageLayout.None;
            this.BackgroundImage = Image.FromFile(_strImagePath);
        }

        protected override void OnBackgroundImageChanged(EventArgs e)
        {
            base.OnBackgroundImageChanged(e);
            if (this.BackgroundImage!=null)
            {
                using (var gp = new GraphicsPath())
                {
                    using (var bm = new Bitmap(this.BackgroundImage))
                    {
                        for (int j = 0; j < bm.Height; j++)
                        {
                            for (int i = 0; i < bm.Width; i++)
                            {
                                if (bm.GetPixel(i, j).A > 0) gp.AddRectangle(new Rectangle(i, j, 1, 1));
                            }
                        }
                    }
                }
            }
        }
    }
}
