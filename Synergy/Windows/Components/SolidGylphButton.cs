using System.Drawing;
using System.Windows.Forms;

namespace Synergy.Windows.Components
{
    public class SolidGylphButton : Panel
    {
        public string Gylph;
        public float FontSize = 7;
        public string FontStyle = "Segoe Fluent Icons";

        protected override void OnPaint(PaintEventArgs e)
        {
            var titleFont = new Font(FontStyle, FontSize);
            var titleSize = e.Graphics.MeasureString(this.Gylph, titleFont);
            var titlePosition = new Point((Width - (int)titleSize.Width) / 2, (Height - (int)titleSize.Height) / 2);
            e.Graphics.DrawString(this.Gylph, titleFont, Brushes.White, titlePosition);
        }
    }
}
