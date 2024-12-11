using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.MDI;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Synergy.Windows
{
    public class OptionsWindow : TitlebarClose
    {
        private Components.ToggleSetting toggleAutoLaunch;
        private Components.ToggleSetting toggleTopMost;
        private Components.ToggleSetting toggleAutoAttach;
        private Components.ToggleSetting toggleClearEditorPrompt;
        private Components.RoundedPanelButton roundedPanelButton1;
        public static bool Open = false;

        private void InitializeComponent()
        {
            this.toggleAutoLaunch = new Synergy.Windows.Components.ToggleSetting();
            this.toggleTopMost = new Synergy.Windows.Components.ToggleSetting();
            this.toggleAutoAttach = new Synergy.Windows.Components.ToggleSetting();
            this.toggleClearEditorPrompt = new Synergy.Windows.Components.ToggleSetting();
            this.roundedPanelButton1 = new Synergy.Windows.Components.RoundedPanelButton();
            this.SuspendLayout();
            // 
            // TitlePanel
            // 
            this.TitlePanel.Size = new System.Drawing.Size(255, 32);
            // 
            // toggleAutoLaunch
            // 
            this.toggleAutoLaunch.Content = "AutoLaunch";
            this.toggleAutoLaunch.ForeColor = System.Drawing.SystemColors.Control;
            this.toggleAutoLaunch.IsChecked = false;
            this.toggleAutoLaunch.Location = new System.Drawing.Point(85, 38);
            this.toggleAutoLaunch.Name = "toggleAutoLaunch";
            this.toggleAutoLaunch.Size = new System.Drawing.Size(164, 25);
            this.toggleAutoLaunch.TabIndex = 1;
            // 
            // toggleTopMost
            // 
            this.toggleTopMost.Content = "TopMost";
            this.toggleTopMost.ForeColor = System.Drawing.SystemColors.Control;
            this.toggleTopMost.IsChecked = false;
            this.toggleTopMost.Location = new System.Drawing.Point(85, 131);
            this.toggleTopMost.Name = "toggleTopMost";
            this.toggleTopMost.Size = new System.Drawing.Size(164, 25);
            this.toggleTopMost.TabIndex = 2;
            // 
            // toggleAutoAttach
            // 
            this.toggleAutoAttach.Content = "AutoAttach";
            this.toggleAutoAttach.ForeColor = System.Drawing.SystemColors.Control;
            this.toggleAutoAttach.IsChecked = false;
            this.toggleAutoAttach.Location = new System.Drawing.Point(85, 69);
            this.toggleAutoAttach.Name = "toggleAutoAttach";
            this.toggleAutoAttach.Size = new System.Drawing.Size(164, 25);
            this.toggleAutoAttach.TabIndex = 3;
            // 
            // toggleClearEditorPrompt
            // 
            this.toggleClearEditorPrompt.Content = "Clear Editor Prompt";
            this.toggleClearEditorPrompt.ForeColor = System.Drawing.SystemColors.Control;
            this.toggleClearEditorPrompt.IsChecked = false;
            this.toggleClearEditorPrompt.Location = new System.Drawing.Point(85, 100);
            this.toggleClearEditorPrompt.Name = "toggleClearEditorPrompt";
            this.toggleClearEditorPrompt.Size = new System.Drawing.Size(164, 25);
            this.toggleClearEditorPrompt.TabIndex = 4;
            // 
            // roundedPanelButton1
            // 
            this.roundedPanelButton1.ButtonText = "Options";
            this.roundedPanelButton1.Location = new System.Drawing.Point(5, 38);
            this.roundedPanelButton1.Name = "roundedPanelButton1";
            this.roundedPanelButton1.Size = new System.Drawing.Size(75, 25);
            this.roundedPanelButton1.TabIndex = 5;
            // 
            // OptionsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(255, 214);
            this.Controls.Add(this.roundedPanelButton1);
            this.Controls.Add(this.toggleClearEditorPrompt);
            this.Controls.Add(this.toggleAutoAttach);
            this.Controls.Add(this.toggleTopMost);
            this.Controls.Add(this.toggleAutoLaunch);
            this.Name = "OptionsWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Synergy - Options";
            this.TopMost = true;
            this.Controls.SetChildIndex(this.TitlePanel, 0);
            this.Controls.SetChildIndex(this.toggleAutoLaunch, 0);
            this.Controls.SetChildIndex(this.toggleTopMost, 0);
            this.Controls.SetChildIndex(this.toggleAutoAttach, 0);
            this.Controls.SetChildIndex(this.toggleClearEditorPrompt, 0);
            this.Controls.SetChildIndex(this.roundedPanelButton1, 0);
            this.ResumeLayout(false);

        }

        protected override void OnFormClosing(FormClosingEventArgs e) => Open = false;

        public OptionsWindow()
        {
            DoubleBuffered = true;
            Open = true;
            InitializeComponent();
            this.roundedPanelButton1.RealBackcolour = Color.FromArgb(16, 16, 16);
            this.roundedPanelButton1.DefaultColour = Color.FromArgb(16, 16, 16);

            // TODO: store as a global class and just reference that instead..
            {
                if (!Program.config.KeyExists("AutoLaunch", "Options"))
                    Program.config.Write("AutoLaunch", "false", "Options");
                this.toggleAutoLaunch.IsChecked = Program.config.Read("AutoLaunch", "Options") == "true";

                if (!Program.config.KeyExists("TopMost", "Options"))
                    Program.config.Write("TopMost", "false", "Options");
                this.toggleTopMost.IsChecked = Program.config.Read("TopMost", "Options") == "true";

                if (!Program.config.KeyExists("AutoAttach", "Options"))
                    Program.config.Write("AutoAttach", "false", "Options");
                this.toggleAutoAttach.IsChecked = Program.config.Read("AutoAttach", "Options") == "true";

                if (!Program.config.KeyExists("ClearEditorPrompt", "Options"))
                    Program.config.Write("ClearEditorPrompt", "false", "Options");
                this.toggleClearEditorPrompt.IsChecked = Program.config.Read("ClearEditorPrompt", "Options") == "true";
            }

            this.CloseButton.Click += (sender, e) => this.Close();
            this.Resizable = false;
            this.toggleAutoLaunch.OnCheck += (sender, e) =>
            {
                Program.config.Write("AutoLaunch", e.CurrentValue.ToString().ToLower(), "Options");

                var startInfo = new ProcessStartInfo
                {
                    FileName = Application.ExecutablePath,
                    Verb = "runas",
                    Arguments = "--refresh"
                };

                Process.Start(startInfo);
            };

            this.toggleTopMost.OnCheck += (sender, e) =>
            {
                MainWindow.Window.TopMost = e.CurrentValue;
                Program.config.Write("TopMost", e.CurrentValue.ToString().ToLower(), "Options"); // TODO: make this less disk reliant when running
            };

            this.toggleAutoAttach.OnCheck += (sender, e) =>
            {
                Program.config.Write("AutoAttach", e.CurrentValue.ToString().ToLower(), "Options");
            };

            this.toggleClearEditorPrompt.OnCheck += (sender, e) =>
            {
                Program.config.Write("ClearEditorPrompt", e.CurrentValue.ToString().ToLower(), "Options");
            };
        }
    }
}
