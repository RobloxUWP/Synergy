using System;
using System.Drawing;
using System.Windows.Forms;

using Synergy.SDK;

namespace Synergy.Windows
{
    public partial class Titlebar : Form
    {
        public Panel TitlePanel;

        //public SolidGylphButton ExitPanel;
        //public SolidGylphButton MinimizePanel;
        //public SolidGylphButton SettingsPanel;

        public bool Resizable = true;

        // Segoe Fluent Icons - \uf8b0

        public Titlebar()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(16, 16, 16);

            this.SetStyle(ControlStyles.ResizeRedraw, true);
            DoubleBuffered = true;

            this.Text = "SampleTitlebar - Synergy";

            this.Resize += Titlebar_Resize;
            this.ResizeEnd += Titlebar_ResizeEnd;
            this.ResizeBegin += Titlebar_ResizeBegin;

            #region UI

            this.TitlePanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 32,
                BackColor = Color.FromArgb(32, 32, 32)
            };
            this.TitlePanel.MouseDown += TitlePanel_MouseDown;
            this.TitlePanel.MouseMove += TitlePanel_MouseMove;
            this.TitlePanel.Paint += TitlePanel_Paint;

            //SettingsPanel = new SolidGylphButton
            //{
            //    Dock = DockStyle.Right,
            //    ForeColor = Color.White,
            //    FontSize = 10,
            //    Width = 26,
            //    Content = "\uf8b0" // \uf8b0
            //};
            //this.TitlePanel.Controls.Add(SettingsPanel);
            //
            //MinimizePanel = new SolidGylphButton
            //{
            //    Dock = DockStyle.Right,
            //    Width = 26,
            //    ForeColor = Color.White,
            //    Content = "\uef2d" // \uef2d
            //};
            //this.TitlePanel.Controls.Add(MinimizePanel);
            //
            //ExitPanel = new SolidGylphButton
            //{
            //    Dock = DockStyle.Right,
            //    Width = 26,
            //    ForeColor = Color.White,
            //    Content = "\uef2c" // \uef2c
            //};
            //this.TitlePanel.Controls.Add(ExitPanel);

            this.Controls.Add(TitlePanel);

            #endregion
        }

        #region Rounded Corners

        private void Titlebar_ResizeBegin(object sender, EventArgs e)
        {
            this.Region = null;
            this.Refresh();
        }

        private void Titlebar_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void Titlebar_ResizeEnd(object sender, EventArgs e)
        {
            this.Region = new Region(Utils.GetRoundedRectangle(new Rectangle(0, 0, this.Width, this.Height), 10));
            this.Refresh();
        }

        protected override void OnLoad(EventArgs e) => Titlebar_ResizeEnd(null, new EventArgs());

        #endregion

        #region Titlebar

        Bitmap icon = null;
        private void TitlePanel_Paint(object sender, PaintEventArgs e)
        {
            var titleFont = new Font("Segoe UI Bold", 9.5f);
            var titleSize = e.Graphics.MeasureString(this.Text, titleFont);
            var titlePosition = new Point((TitlePanel.Width - (int)titleSize.Width) / 2, (TitlePanel.Height - (int)titleSize.Height) / 2);
            e.Graphics.DrawString(this.Text, titleFont, Brushes.White, titlePosition);

            var iconSize = new Size(24,24);
            if (icon == null)
                icon = new Bitmap(this.Icon.ToBitmap(), iconSize);
            e.Graphics.DrawImage(icon, 10, (TitlePanel.Height - iconSize.Height) / 2);
        }

        private Point mouseOffset; // no static variables inside methods is DUMB
        private void TitlePanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                mouseOffset = e.Location;
        }

        private void TitlePanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(
                    this.Location.X + e.X - mouseOffset.X,
                    this.Location.Y + e.Y - mouseOffset.Y
                );
            }
        }

        #endregion

        #region Resizable Border

        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x84;
            const int HTLEFT = 10;
            const int HTRIGHT = 11;
            const int HTTOP = 12;
            const int HTBOTTOM = 15;
            const int HTTOPLEFT = 13;
            const int HTTOPRIGHT = 14;
            const int HTBOTTOMLEFT = 16;
            const int HTBOTTOMRIGHT = 17;

            base.WndProc(ref m);

            if (Resizable && m.Msg == WM_NCHITTEST)
            {
                var cursorPosition = PointToClient(Cursor.Position);
                int widthThreshold = 10;
                int heightThreshold = 10;

                if (cursorPosition.X <= widthThreshold && cursorPosition.Y <= heightThreshold)
                {
                    m.Result = (IntPtr)HTTOPLEFT;
                }
                else if (cursorPosition.X >= ClientSize.Width - widthThreshold && cursorPosition.Y <= heightThreshold)
                {
                    m.Result = (IntPtr)HTTOPRIGHT;
                }
                else if (cursorPosition.X <= widthThreshold && cursorPosition.Y >= ClientSize.Height - heightThreshold)
                {
                    m.Result = (IntPtr)HTBOTTOMLEFT;
                }
                else if (cursorPosition.X >= ClientSize.Width - widthThreshold && cursorPosition.Y >= ClientSize.Height - heightThreshold)
                {
                    m.Result = (IntPtr)HTBOTTOMRIGHT;
                }
                else if (cursorPosition.X <= widthThreshold)
                {
                    m.Result = (IntPtr)HTLEFT;
                }
                else if (cursorPosition.X >= ClientSize.Width - widthThreshold)
                {
                    m.Result = (IntPtr)HTRIGHT;
                }
                else if (cursorPosition.Y <= heightThreshold)
                {
                    m.Result = (IntPtr)HTTOP;
                }
                else if (cursorPosition.Y >= ClientSize.Height - heightThreshold)
                {
                    m.Result = (IntPtr)HTBOTTOM;
                }
            }
        }

        #endregion
    }
}
