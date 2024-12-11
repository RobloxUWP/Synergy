using System.Web.Hosting;
using System.Windows.Forms;
using Synergy.Windows.Components;

namespace Synergy.Windows
{
    public class TitlebarClose : Titlebar
    {
        public SolidGylphButton CloseButton { get; set; }

        public TitlebarClose()
        {
            CloseButton = new SolidGylphButton
            {
                Gylph = "\uef2c",
                Width = 26,
                Dock = DockStyle.Right
            };

            //this.CloseButton.Click += (sender, e) => Application.Exit();
            this.TitlePanel.Controls.Add(CloseButton);
        }
    }
}