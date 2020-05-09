using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utils
{
    #region Definición de excepciones 
    class InvalidRange : Exception
    {
        // Use the default Exception constructors
        public InvalidRange() : base() { }
        public InvalidRange(string s) : base(s) { }
        public InvalidRange(string s, Exception ex) : base(s, ex) { }
    }

    class FieldLength : Exception
    {
        // Use the default Exception constructors
        public FieldLength() : base() { }
        public FieldLength(string s) : base(s) { }
        public FieldLength(string s, Exception ex) : base(s, ex) { }
    }

    class DifferentSize : Exception
    {
        // Use the default Exception constructors
        public DifferentSize() : base() { }
        public DifferentSize(string s) : base(s) { }
        public DifferentSize(string s, Exception ex) : base(s, ex) { }
    }

    class NotAnInteger : Exception
    {
        // Use the default Exception constructors
        public NotAnInteger() : base() { }
        public NotAnInteger(string s) : base(s) { }
        public NotAnInteger(string s, Exception ex) : base(s, ex) { }
    }

    class MemoryAllocation : Exception
    {
        // Use the default Exception constructors
        public MemoryAllocation() : base() { }
        public MemoryAllocation(string s) : base(s) { }
        public MemoryAllocation(string s, Exception ex) : base(s, ex) { }
    }
    #endregion

    /// <summary>
    /// Centers a dialog into its parent window
    /// </summary>
    /// https://stackoverflow.com/questions/2576156/winforms-how-can-i-make-messagebox-appear-centered-on-mainform
    /// https://stackoverflow.com/questions/1732443/center-messagebox-in-parent-form
    public class CenterWinDialog : IDisposable
    {
        private int mTries = 0;
        private Form mOwner;
        private Rectangle clientRect;

        public CenterWinDialog(Form owner)
        {
            mOwner = owner;
            clientRect = Screen.FromControl(owner).WorkingArea;

            if (owner.WindowState != FormWindowState.Minimized)
                owner.BeginInvoke(new MethodInvoker(findDialog));
        }

        private void findDialog()
        {
            // Enumerate windows to find the message box
            if (mTries < 0) return;
            EnumThreadWndProc callback = new EnumThreadWndProc(checkWindow);
            if (EnumThreadWindows(GetCurrentThreadId(), callback, IntPtr.Zero))
            {
                if (++mTries < 10) mOwner.BeginInvoke(new MethodInvoker(findDialog));
            }
        }
        private bool checkWindow(IntPtr hWnd, IntPtr lp)
        {
            // Checks if <hWnd> is a dialog
            StringBuilder sb = new StringBuilder(260);
            GetClassName(hWnd, sb, sb.Capacity);
            if (sb.ToString() != "#32770") return true;
            
            // Got it
            Rectangle frmRect = new Rectangle(mOwner.Location, mOwner.Size);
            RECT dlgRect;
            GetWindowRect(hWnd, out dlgRect);

            int x = frmRect.Left + (frmRect.Width - dlgRect.Right + dlgRect.Left) / 2;
            int y = frmRect.Top + (frmRect.Height - dlgRect.Bottom + dlgRect.Top) / 2;
            
            clientRect.Width -=  (dlgRect.Right - dlgRect.Left);
            clientRect.Height -= (dlgRect.Bottom - dlgRect.Top);
            clientRect.X = x < clientRect.X ? clientRect.X : ( x > clientRect.Right ? clientRect.Right : x);
            clientRect.Y = y < clientRect.Y ? clientRect.Y : ( y > clientRect.Bottom ? clientRect.Bottom : y);
            
            MoveWindow(hWnd, clientRect.X, clientRect.Y, dlgRect.Right - dlgRect.Left, dlgRect.Bottom - dlgRect.Top, true);
            
            return false;
        }
        public void Dispose()
        {
            mTries = -1;
        }

        // P/Invoke declarations
        private delegate bool EnumThreadWndProc(IntPtr hWnd, IntPtr lp);
        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        private static extern bool EnumThreadWindows(int tid, EnumThreadWndProc callback, IntPtr lp);
        [DllImport("kernel32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        private static extern int GetCurrentThreadId();
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder buffer, int buflen);
        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT rc);
        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int w, int h, bool repaint);
        private struct RECT { public int Left; public int Top; public int Right; public int Bottom; }
    }

    /// <summary>
    /// Subclassed RadioButton to accept double click events
    /// </summary>
    public class RadioButton : System.Windows.Forms.RadioButton
    {
        public RadioButton()
        {
            //InitializeComponent();

            this.SetStyle(ControlStyles.StandardClick | ControlStyles.StandardDoubleClick, true);
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true)]
        public new event MouseEventHandler MouseDoubleClick;

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            // raise the event
            if (this.MouseDoubleClick != null)
                this.MouseDoubleClick(this, e);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }
    }

 

    /// <summary>
    /// Defines the properties of the results forms, basically the RichTextBox visual properties
    /// </summary>
    public class ResultsOptions
    {
        #region Propiedades de la clase
        private System.Windows.Forms.RichTextBox _rtbText;
        #endregion

        public ResultsOptions(System.Windows.Forms.RichTextBox rtbText)
        {
            _rtbText = rtbText;
        }

        /// <summary>
        /// Propiedades públicas de la clase
        /// </summary>
        [Browsable(true),
        ReadOnly(false),
        Description("Font"),
        Category("Text")]
        public System.Drawing.Font Font
        {
            get
            {
                return _rtbText.Font;
                //return axis[0].Title;
            }
            set
            {
                _rtbText.Font = value;
                ((IChildResults)_rtbText.Parent.Parent.Parent).FormatText();
            }
        }

        /// <summary>
        /// Propiedades públicas de la clase
        /// </summary>
        [Browsable(true),
        ReadOnly(false),
        Description("Font color"),
        Category("Text")]
        public System.Drawing.Color FontColor
        {
            get
            {
                return _rtbText.ForeColor;
                //return axis[0].Title;
            }
            set
            {
                _rtbText.ForeColor = value;
            }
        }

        /// <summary>
        /// Propiedades públicas de la clase
        /// </summary>
        [Browsable(true),
        ReadOnly(false),
        Description("Wrap text in new line?"),
        Category("Text")]
        public bool WordWrap
        {
            get
            {
                return _rtbText.WordWrap;
                //return axis[0].Title;
            }
            set
            {
                _rtbText.WordWrap = value;
            }
        }

        /// <summary>
        /// Propiedades públicas de la clase
        /// </summary>
        [Browsable(true),
        ReadOnly(false),
        Description("Text back color"),
        Category("Text")]
        public System.Drawing.Color BackColor
        {
            get
            {
                return _rtbText.BackColor;
                //return axis[0].Title;
            }
            set
            {
                _rtbText.BackColor = value;
            }
        }

        /// <summary>
        /// Propiedades públicas de la clase
        /// </summary>
        [Browsable(true),
        ReadOnly(false),
        Description("Text zoom"),
        Category("Text")]
        public float Zoom
        {
            get
            {
                return _rtbText.ZoomFactor;
                //return axis[0].Title;
            }
            set
            {
                _rtbText.ZoomFactor = value;
            }
        }
    }

    /// <summary>
    /// Public interface for child windows
    /// </summary>
    public interface IChildResults
    {
        /// <summary>
        /// Saves the data shown in the child window into a file
        /// </summary>
        /// <param name="path">Path where the data should be saved</param>
        void Save(string path);
        
        /// <summary>
        /// Defines the enabled state for each control in frmMain's ToolBar
        /// </summary>
        /// <returns>An array with 'enabled' boolean values</returns>
        bool[] GetToolbarEnabledState();

        /// <summary>
        /// Sets the enabled/disabled state of the ToolStripButtons in the parent MDI form
        /// </summary>
        void ShowHideSettings();

        /// <summary>
        /// Gets the state of the splitContainer1.Panel1Collapsed
        /// </summary>
        /// <returns>True if the PropertyGrid is visible, false if it is collapsed</returns>
        bool PanelCollapsed();

        /// <summary>
        /// Formats the text shown in the RichtTextBox
        /// </summary>
        void FormatText();
    }

    /// <summary>
    /// Custom renderer for ToolBar button checked
    /// https://www.discussiongenerator.com/2009/08/22/33/
    /// https://stackoverflow.com/questions/2097164/how-to-change-system-windows-forms-toolstripbutton-highlight-background-color-wh
    /// https://docs.microsoft.com/en-us/dotnet/framework/winforms/controls/walkthrough-creating-a-professionally-styled-toolstrip-control#implementing-a-custom-renderer
    /// </summary>
    public class customRenderer : ToolStripProfessionalRenderer
    {
        private System.Drawing.Brush _border;
        private System.Drawing.Brush _checkedBackground;

        /// <summary>
        /// Class constructor. Sets SteelBlue and LightSkyBlue as defaults colors
        /// </summary>
        public customRenderer()
        {
            _border = System.Drawing.Brushes.SteelBlue;
            _checkedBackground = System.Drawing.Brushes.LightSkyBlue;
        }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="border">Brush for the checked border</param>
        /// <param name="checkedBackground">Brush for the checked background</param>
        public customRenderer(System.Drawing.Brush border, System.Drawing.Brush checkedBackground)
        {
            _border = border;
            _checkedBackground = checkedBackground;
        }

        /// <summary>
        /// Sets and gets the border color of the checked button
        /// </summary>
        public System.Drawing.Brush BorderColor
        {
            get { return _border; }
            set { _border = value; }
        }

        /// <summary>
        /// Sets and gets the background color of the checked button
        /// </summary>
        public System.Drawing.Brush CheckedColor
        {
            get { return _checkedBackground; }
            set { _checkedBackground = value; }
        }

        protected override void OnRenderButtonBackground(System.Windows.Forms.ToolStripItemRenderEventArgs e)
        {
            // check if the object being rendered is actually a ToolStripButton
            if (e.Item is System.Windows.Forms.ToolStripButton)
            {
                System.Windows.Forms.ToolStripButton button = e.Item as System.Windows.Forms.ToolStripButton;

                // only render checked items differently
                if (button.Checked)
                {
                    // fill the entire button with a color (will be used as a border)
                    int buttonHeight = button.Size.Height;
                    int buttonWidth = button.Size.Width;
                    System.Drawing.Rectangle rectButtonFill = new System.Drawing.Rectangle(System.Drawing.Point.Empty, new Size(buttonWidth, buttonHeight));
                    e.Graphics.FillRectangle(_border, rectButtonFill);

                    // fill the entire button offset by 1,1 and height/width subtracted by 2 used as the fill color
                    int backgroundHeight = button.Size.Height - 2;
                    int backgroundWidth = button.Size.Width - 2;
                    System.Drawing.Rectangle rectBackground = new System.Drawing.Rectangle(1, 1, backgroundWidth, backgroundHeight);
                    e.Graphics.FillRectangle(_checkedBackground, rectBackground);
                }
                // if this button is not checked, use the normal render event
                else
                    base.OnRenderButtonBackground(e);
            }
            // if this object is not a ToolStripButton, use the normal render event
            else
                base.OnRenderButtonBackground(e);
        }
    }

    public class DrawingControl
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);

        private const int WM_SETREDRAW = 11;

        public static void SuspendDrawing(Control parent)
        {
            SendMessage(parent.Handle, WM_SETREDRAW, false, 0);
        }

        public static void ResumeDrawing(Control parent)
        {
            SendMessage(parent.Handle, WM_SETREDRAW, true, 0);
            parent.Refresh();
        }
    }

}

namespace System.Windows.Forms
{
    /// <summary>
    /// Declare a class that inherits from ToolStripControlHost.
    /// https://docs.microsoft.com/en-us/dotnet/framework/winforms/controls/how-to-wrap-a-windows-forms-control-with-toolstripcontrolhost
    /// </summary>
    [System.Windows.Forms.Design.ToolStripItemDesignerAvailability(System.Windows.Forms.Design.ToolStripItemDesignerAvailability.ToolStrip)]
    public class ToolStripNumericUpDown : ToolStripControlHost
    {
        /// <summary>
        /// // Call the base constructor passing in a NumericUpDown instance.
        /// </summary>
        public ToolStripNumericUpDown() : base(new NumericUpDown())
        {
            this.Margin = new Padding(5, 0, 5, 0);
            this.NumericUpDownControl.DecimalPlaces = 0;
        }

        public NumericUpDown NumericUpDownControl
        {
            get { return Control as NumericUpDown; }
        }

        /// <summary>
        /// Subscribe and unsubscribe the ValueChanged event
        /// </summary>
        /// <param name="control"></param>
        protected override void OnSubscribeControlEvents(Control control)
        {
            base.OnSubscribeControlEvents(control);
            ((NumericUpDown)control).ValueChanged += new EventHandler(OnValueChanged);
        }

        /// <summary>
        /// Unsubscribe and unsubscribe the ValueChanged event
        /// </summary>
        /// <param name="control"></param>
        protected override void OnUnsubscribeControlEvents(Control control)
        {
            base.OnUnsubscribeControlEvents(control);
            ((NumericUpDown)control).ValueChanged -= new EventHandler(OnValueChanged);
        }

        // Declare the ValueChanged event.
        public event EventHandler ValueChanged;

        // Raise the ValueChanged event.
        public void OnValueChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, e);
            }
        }
    }

}