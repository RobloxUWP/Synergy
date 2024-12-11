using Synergy.RobloxSDK;
using Synergy.Windows.Components;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Synergy.Windows
{
    public partial class MainWindow : TitlebarClose
    {
        public SolidGylphButton MinimizeButton { get; set; }
        public SolidGylphButton SettingsButton { get; set; }

        public static MainWindow Window;

        public MainWindow()
        {
            Window = this;

            MinimizeButton = new SolidGylphButton
            {
                Gylph = "\uef2d",
                Width = 26,
                Dock = DockStyle.Right
            };

            this.MinimizeButton.Click += (sender, e) => WindowState = FormWindowState.Minimized;
            this.TitlePanel.Controls.Add(MinimizeButton);
            MinimizeButton.BringToFront();

            SettingsButton = new SolidGylphButton
            {
                Gylph = "\uf8b0",
                FontSize = 10,
                Width = 26,
                Dock = DockStyle.Right
            };

            this.SettingsButton.Click += (sender, e) => {
                if (!OptionsWindow.Open)
                    new OptionsWindow().Show();
            };
            this.TitlePanel.Controls.Add(SettingsButton);
            SettingsButton.BringToFront();

            this.CloseButton.Click += (sender, e) => Application.Exit();

            InitializeComponent();

            LuaWebExecutor.Source = new System.Uri(Application.StartupPath + "\\data\\Editor.html");

            this.Text = "Synergy - Main";
        }

        private async void roundedPanelButton2_Click(object sender, System.EventArgs e)
            => await LuaWebExecutor.ExecuteScriptAsync("SetText('')");

        private void roundedPanelButton3_Click(object sender, System.EventArgs e)
        {
            string filePath = FileDialog.OpenFileDialog();
            if (filePath != null)
            {
                LoadScriptByPath(filePath);
            }
        }

        public void LoadScriptByPath(string filePath)
        {
            string script = File.ReadAllText(filePath);
            LuaWebExecutor.ExecuteScriptAsync($"SetText(`{script.Replace("\\", "\\\\").Replace("\"", "\\\"")}`)");
        }

        private void roundedPanelButton6_Click(object sender, System.EventArgs e) => SirHurtAPI.Inject(false);

        private async void roundedPanelButton5_Click(object sender, System.EventArgs e)
        {
            var rawScript = (await LuaWebExecutor.ExecuteScriptAsync("GetText()"));
            var script = new JavaScriptSerializer().Deserialize<string>(rawScript);

            if (script.Length > 0)
            {
                string filePath = FileDialog.SaveFileDialog();
                if (filePath != null)
                    File.WriteAllText(filePath, script);
            }
        }

        private async void roundedPanelButton1_Click(object sender, System.EventArgs e)
            => SirHurtAPI.QueuedScript = (await LuaWebExecutor.ExecuteScriptAsync("GetText()")).Trim('\"');
    }
}

// SettingsSolid - f8b0
// ChromeMinimizeContrast - ef2d
// ChromeCloseContrast - ef2c