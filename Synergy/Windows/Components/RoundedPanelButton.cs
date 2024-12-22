using System;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;

namespace Synergy.Windows.Components
{
    public class RoundedPanelButton : Panel
    {
        public Color DefaultColour;
        public Color HoverColour;
        public Color ForeColour;
        public Color RealBackcolour = Color.FromArgb(50, 50, 50);
        public string Content = "Button";
        public int Radius = 6;

        public RoundedPanelButton()
        {
            DefaultColour = Color.FromArgb(50, 50, 50);
            HoverColour = BrightenColor(RealBackcolour, 20);
            ForeColour = Color.White;

            this.DoubleBuffered = true;

            this.MouseEnter += (s, e) => {
                RealBackcolour = HoverColour;
                Invalidate();
            };
            this.MouseLeave += (s, e) => {
                RealBackcolour = DefaultColour;
                Invalidate();
            };
        }

        public string ButtonText
        {
            get => Content;
            set
            {
                Content = value; this.Invalidate();
            }
        }

        private static Color BrightenColor(Color color, int amount)
        {
            int R = Math.Min(255, color.R + amount);
            int G = Math.Min(255, color.G + amount);
            int B = Math.Min(255, color.B + amount);
            return Color.FromArgb(R, G, B);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (var brush = new SolidBrush(RealBackcolour))
            {
                using (var path = new GraphicsPath())
                {
                    path.AddArc(0, 0, Radius, Radius, 180, 90);
                    path.AddArc(this.Width - Radius, 0, Radius, Radius, 270, 90);
                    path.AddArc(this.Width - Radius, this.Height - Radius, Radius, Radius, 0, 90);
                    path.AddArc(0, this.Height - Radius, Radius, Radius, 90, 90);
                    path.CloseFigure();

                    graphics.FillPath(brush, path);
                }
            }

            using (var textBrush = new SolidBrush(ForeColour))
            using (var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            {
                graphics.DrawString(Content, this.Font, textBrush, this.ClientRectangle, format);
            }

            using (var borderPen = new Pen(Color.FromArgb(60, 60, 60), 1))
            {
                using (var borderPath = new GraphicsPath())
                {
                    borderPath.AddArc(0, 0, Radius, Radius, 180, 90);
                    borderPath.AddArc(this.Width - Radius - 1, 0, Radius, Radius, 270, 90);
                    borderPath.AddArc(this.Width - Radius - 1, this.Height - Radius - 1, Radius, Radius, 0, 90);
                    borderPath.AddArc(0, this.Height - Radius - 1, Radius, Radius, 90, 90);
                    borderPath.CloseFigure();

                    graphics.DrawPath(borderPen, borderPath);
                }
            }
        }
    }
}
