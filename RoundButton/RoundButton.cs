using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;

namespace Controls
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(System.ComponentModel.Design.IDesigner))]
    public partial class RoundButton : UserControl
    {
        #region GDI API functions

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
            (
                    int nLeftRect,      // x-coordinate of upper-left corner
                    int nTopRect,       // y-coordinate of upper-left corner
                    int nRightRect,     // x-coordinate of lower-right corner
                    int nBottomRect,    // y-coordinate of lower-right corner
                    int nWidthEllipse,  // height of ellipse
                    int nHeightEllipse  // width of ellipse
            );
        [DllImport("Gdi32.dll", EntryPoint = "FillRgn")]
        private static extern IntPtr FillRgn
            (IntPtr hdc,
            IntPtr hrgn,
            IntPtr hbr
            );
        [DllImport("Gdi32.dll", EntryPoint = "FrameRgn")]
        public static extern int FrameRgn(
            IntPtr hDC,
            IntPtr hRgn,
            IntPtr hBrush,
            int nWidth,
            int nHeight
            );

        [DllImport("Gdi32.dll", EntryPoint = "CreateSolidBrush")]
        private static extern IntPtr CreateSolidBrush(uint crColor);

        [DllImport("Gdi32.dll", EntryPoint = "DeleteObject")]
        private static extern bool DeleteObject(IntPtr hObject);

        #endregion #region GDI API functions

        #region Private variables

        private float _fBorderWidth = 1f;
        private float _xRadius = 0f;
        private float _yRadius = 0f;
        private float _fRegionOffset = 0f;
        private Color _cBorderColor = Color.Black;
        private Color _cFillColor = Color.Transparent;
        private int _nWidth;
        private int _nHeight;
        private string _sText ="";
        private bool _showText = true;
        private bool _showBorder = true;

        #endregion Private variables

        #region Public interface

        [Description("Width of the border (0 means no border)"),
        Category("Rounded properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public float BorderWidth
        {
            get { return _fBorderWidth; }
            set { _fBorderWidth = value < 0 ? 0f : value; Invalidate(); }
        }

        [Description("Border radius (0 means no rounded corner)"),
        Category("Rounded properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public float xRadius
        {
            get { return _xRadius; }
            set { _xRadius = value < 0 ? 0f : value; Invalidate(); }
        }

        [Description("Border radius (0 means no rounded corner)"),
        Category("Rounded properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public float yRadius
        {
            get { return _yRadius; }
            set { _yRadius = value < 0 ? 0f : value; Invalidate(); }
        }

        [Description("Region offset of the control (typically 1 px)"),
        Category("Rounded properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public float RegionOffset
        {
            get { return _fRegionOffset; }
            set { _fRegionOffset = value; Invalidate(); }
        }

        [Description("Border color"),
        Category("Rounded properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BorderColor
        {
            get { return _cBorderColor; }
            set { _cBorderColor = value; Invalidate(); }
        }

        [Description("Fill color"),
        Category("Rounded properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color FillColor
        {
            get { return _cFillColor; }
            set { _cFillColor = value; Invalidate(); }
        }

        [Description("Inner width inside stroke"),
        Category("Rounded properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int InnerWidth
        {
            get { return (int)(this.ClientRectangle.Width - 1 - 2 * _fBorderWidth); }
        }

        [Description("Inner height inside stroke"),
        Category("Rounded properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int InnerHeight
        {
            get { return (int)(this.ClientRectangle.Height - 1 - 2 * _fBorderWidth); }
        }

        [Description("Text to display"),
        Category("Rounded properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get { return _sText; }
            set { _sText = value.ToString(); lblText.Text = _sText; }
        }

        [Description("Show text"),
        Category("Rounded properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool VisibleText
        {
            get { return _showText; }
            set { _showText = value; lblText.Visible = _showText; }
        }

        [Description("Show border"),
        Category("Rounded properties"),
        Browsable(true),
        EditorBrowsable(EditorBrowsableState.Always),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool VisibleBorder
        {
            get { return _showBorder; }
            set { _showBorder = value; Invalidate(); }
        }

        #endregion Public interface

        #region Public events
        public event EventHandler<ButtonClickEventArgs> ButtonClick;
        protected virtual void OnButtonClick(ButtonClickEventArgs e)
        {
            if (ButtonClick != null) ButtonClick(this, e);
        }
        public class ButtonClickEventArgs : EventArgs
        {
            public readonly int ButtonValue;
            public ButtonClickEventArgs(int button) { ButtonValue = button; }
        }
        #endregion Public events

        public RoundButton()
        {
            InitializeComponent();

            // To ensure that your control is redrawn every time it is resized
            // https://msdn.microsoft.com/en-us/library/b818z6z6(v=vs.110).aspx
            SetStyle(ControlStyles.ResizeRedraw |
                    ControlStyles.UserPaint |
                    ControlStyles.SupportsTransparentBackColor |
                    ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.AllPaintingInWmPaint,
                    true);

            this.DoubleBuffered = true;

            // Set default size
            //this.ClientSize = new System.Drawing.Size(100, 100);

            //this.AutoSize = false;
            //this.BackColor= Color.Transparent;
            //this.TextAlign = ContentAlignment.MiddleCenter;
            //this.BorderStyle = BorderStyle.None;

            //this.OnMouseClick += new System.Windows.Forms.MouseEventHandler(this.RoundButton_MouseClick);
            //this.Click += new EventHandler(OnMouseClick);

        }

        private void RoundButton_Load(object sender, EventArgs e)
        {
            // Some default label properties
            lblText.Text = Text;
            lblText.Text = _sText;
        }

        /// <summary>
        /// Overrides the paint event. Comprises 3 parts: the fill, the border, and the definition of the control's region
        /// </summary>
        /// <param name="e">Paint event argument</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics dc = e.Graphics;
            dc.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            //RectangleF rectOut = new RectangleF(0.5f, 0.5f, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);
            RectangleF rectOut = new RectangleF(_fRegionOffset / 2, _fRegionOffset / 2, this.ClientRectangle.Width - _fRegionOffset, this.ClientRectangle.Height - _fRegionOffset);
            RectangleF rectIn = RectangleF.Inflate(rectOut, -_fBorderWidth, -_fBorderWidth);                // Mantains the rectangle's geometric center.
            //RectangleF rectRegion = RectangleF.Inflate(rectOut, 0.5f, 0.5f);
            RectangleF rectRegion = RectangleF.Inflate(rectOut, _fRegionOffset / 2, _fRegionOffset / 2);    // Inflates in both + and - directions, hence _fRegionOffset / 2 for a total of _fRegionOffset

            GraphicsPath path = MakeRoundedRect(rectOut, _xRadius, _yRadius);
            dc.FillPath(new SolidBrush(_cFillColor), path);

            if (_showBorder)
            {
                path.AddPath(MakeRoundedRect(rectIn, _xRadius - _fBorderWidth, _yRadius - _fBorderWidth), false);
                dc.FillPath(new SolidBrush(_cBorderColor), path);
            }

            //this.Region = new Region(MakeRoundedRect(rectRegion, _xRadius + 0.5f, _yRadius + 0.5f));
            this.Region = new Region(MakeRoundedRect(rectRegion, _xRadius + _fRegionOffset / 2, _yRadius + _fRegionOffset / 2));

            // Draw text
            if (_showText) DrawText(dc);

            this.lblText.Padding = new Padding((int)(this.lblText.Font.SizeInPoints / 6), 0, 0, 0);
            //this.lblText.Region = this.Region;

            base.OnPaint(e);

        }

        protected override void OnClick(EventArgs e)
        {
            //_showBorder = !_showBorder;
            //Invalidate();
            OnButtonClick(new ButtonClickEventArgs(int.Parse(lblText.Text)));

            //base.OnClick(e);
        }

        private void lblText_Click(object sender, EventArgs e)
        {
            //_showBorder = !_showBorder;
            //Invalidate();
            //if (ButtonClick != null) OnButtonClick(new ButtonClickEventArgs(int.Parse(lblText.Text)));
        }

        protected virtual void DrawText(Graphics g)
        {
            if (Text == string.Empty) return;

            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            
            // The upper left corner coordinates and the size of the rectangle where the text will be drawn
            var point = PointF.Empty;
            SizeF size = this.Size;

            //point = AddPoint(point, InnerMargin);
            //size = AddSize(size, -2 * InnerMargin);

            point.X += _fRegionOffset + _fBorderWidth;
            point.Y += _fRegionOffset + _fBorderWidth;
            size.Width -= 2 * (_fRegionOffset + _fBorderWidth);
            size.Height -= 2 * (_fRegionOffset + _fBorderWidth);

            System.Diagnostics.Debug.WriteLine(point.ToString() + " — " + size.ToString());

            var stringFormat =
                new StringFormat(RightToLeft == RightToLeft.Yes ? StringFormatFlags.DirectionRightToLeft : 0)
                {
                    Alignment = StringAlignment.Center,     // Horizontal alignment
                    LineAlignment = StringAlignment.Center  // Vertical alignment
                };
            var textSize = g.MeasureString(Text, Font);
            System.Diagnostics.Debug.WriteLine(textSize.ToString());
            var textPoint = new PointF(
                point.X + (size.Width - textSize.Width) / 2,
                point.Y + (size.Height - textSize.Height) / 2);
            
            System.Diagnostics.Debug.WriteLine(textPoint.ToString());

            /*
            if (SubscriptText != string.Empty || SuperscriptText != string.Empty)
            {
                float maxSWidth = 0;
                var supSize = SizeF.Empty;
                var subSize = SizeF.Empty;

                if (SuperscriptText != string.Empty)
                {
                    supSize = g.MeasureString(SuperscriptText, SecondaryFont);
                    maxSWidth = Math.Max(supSize.Width, maxSWidth);
                    supSize.Width -= SuperscriptMargin.Right;
                    supSize.Height -= SuperscriptMargin.Bottom;
                }

                if (SubscriptText != string.Empty)
                {
                    subSize = g.MeasureString(SubscriptText, SecondaryFont);
                    maxSWidth = Math.Max(subSize.Width, maxSWidth);
                    subSize.Width -= SubscriptMargin.Right;
                    subSize.Height -= SubscriptMargin.Bottom;
                }

                textPoint.X -= maxSWidth / 4;

                if (SuperscriptText != string.Empty)
                {
                    var supPoint = new PointF(
                        textPoint.X + textSize.Width - supSize.Width / 2,
                        textPoint.Y - supSize.Height * 0.85f);
                    supPoint.X += SuperscriptMargin.Left;
                    supPoint.Y += SuperscriptMargin.Top;
                    g.DrawString(
                        SuperscriptText,
                        SecondaryFont,
                        new SolidBrush(SuperscriptColor),
                        new RectangleF(supPoint, supSize),
                        stringFormat);
                }

                if (SubscriptText != string.Empty)
                {
                    var subPoint = new PointF(
                        textPoint.X + textSize.Width - subSize.Width / 2,
                        textPoint.Y + textSize.Height * 0.85f);
                    subPoint.X += SubscriptMargin.Left;
                    subPoint.Y += SubscriptMargin.Top;
                    g.DrawString(
                        SubscriptText,
                        SecondaryFont,
                        new SolidBrush(SubscriptColor),
                        new RectangleF(subPoint, subSize),
                        stringFormat);
                }
            }
            */

            g.DrawString(
                Text,
                Font,
                new SolidBrush(ForeColor),
                new RectangleF(textPoint, textSize),
                stringFormat);
            //g.DrawRectangle(new Pen(Color.Red, 3), 0.0F, 0.0F, textSize.Width, textSize.Height);
        }

        /// <summary>
        /// Draw a rectangle in the indicated Rectangle (the container box) rounding the indicated corners.
        /// http://csharphelper.com/blog/2016/01/draw-rounded-rectangles-in-c/
        /// </summary>
        /// <param name="rect">Rectangle structure to be rounded</param>
        /// <param name="xradius">Horizonal radius in pixels</param>
        /// <param name="yradius">Vertical radius in pixels</param>
        /// <param name="round_ul">True (default value) if upper left corner is to be rounded</param>
        /// <param name="round_ur">True (default value) if upper right corner is to be rounded</param>
        /// <param name="round_lr">True (default value) if lower right corner is to be rounded</param>
        /// <param name="round_ll">True (default value) if lower left corner is to be rounded</param>
        /// <returns>The graphic path defining the rounded rectangle</returns>
        private GraphicsPath MakeRoundedRect(RectangleF rect, float xradius, float yradius, bool round_ul = true, bool round_ur = true, bool round_lr = true, bool round_ll = true)
        {
            // Make a GraphicsPath to draw the rectangle.
            PointF point1, point2;
            GraphicsPath path = new GraphicsPath();

            // Upper left corner.
            if (round_ul)
            {
                if (xradius > 0 && yradius > 0)
                {
                    RectangleF corner = new RectangleF(
                        rect.X, rect.Y,
                        2 * xradius, 2 * yradius);
                    path.AddArc(corner, 180, 90);
                }
                point1 = new PointF(rect.X + xradius, rect.Y);
            }
            else point1 = new PointF(rect.X, rect.Y);

            // Top side.
            if (round_ur)
                point2 = new PointF(rect.Right - xradius, rect.Y);
            else
                point2 = new PointF(rect.Right, rect.Y);
            path.AddLine(point1, point2);

            // Upper right corner.
            if (round_ur)
            {
                if (xradius > 0 && yradius > 0)
                {
                    RectangleF corner = new RectangleF(
                        rect.Right - 2 * xradius, rect.Y,
                        2 * xradius, 2 * yradius);
                    path.AddArc(corner, 270, 90);
                }
                point1 = new PointF(rect.Right, rect.Y + yradius);
            }
            else point1 = new PointF(rect.Right, rect.Y);

            // Right side.
            if (round_lr)
                point2 = new PointF(rect.Right, rect.Bottom - yradius);
            else
                point2 = new PointF(rect.Right, rect.Bottom);
            path.AddLine(point1, point2);

            // Lower right corner.
            if (round_lr)
            {
                if (xradius > 0 && yradius > 0)
                {
                    RectangleF corner = new RectangleF(
                    rect.Right - 2 * xradius,
                    rect.Bottom - 2 * yradius,
                    2 * xradius, 2 * yradius);
                    path.AddArc(corner, 0, 90);
                }
                point1 = new PointF(rect.Right - xradius, rect.Bottom);
            }
            else point1 = new PointF(rect.Right, rect.Bottom);

            // Bottom side.
            if (round_ll)
                point2 = new PointF(rect.X + xradius, rect.Bottom);
            else
                point2 = new PointF(rect.X, rect.Bottom);
            path.AddLine(point1, point2);

            // Lower left corner.
            if (round_ll)
            {
                if (xradius > 0 && yradius > 0)
                {
                    RectangleF corner = new RectangleF(
                    rect.X, rect.Bottom - 2 * yradius,
                    2 * xradius, 2 * yradius);
                    path.AddArc(corner, 90, 90);
                }
                point1 = new PointF(rect.X, rect.Bottom - yradius);
            }
            else point1 = new PointF(rect.X, rect.Bottom);

            // Left side.
            if (round_ul)
                point2 = new PointF(rect.X, rect.Y + yradius);
            else
                point2 = new PointF(rect.X, rect.Y);
            path.AddLine(point1, point2);

            // Join with the start point.
            path.CloseFigure();

            return path;
        }

    }
}
// https://stackoverflow.com/questions/33878184/c-sharp-how-to-make-smooth-arc-region-using-graphics-path

