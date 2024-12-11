using Synergy.RobloxSDK;
using Synergy.SDK.Jobs;
using Synergy.Windows;

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace Synergy
{
    public partial class LauncherWindow : Titlebar
    {
        public static LauncherWindow Window;

        private string loadingSuffix = "Checking Version";
        private int dots = 1;
        bool? versionValid = null;

        private string placeId = "0"; // TODO: remove this and use new sdk

        private JobManager jobManager;

        bool isWhitelisted = false;
        bool extra1 = false;

        public LauncherWindow()
        {
            Window = this;

            TopMost = true;
            InitializeComponent();

            Resizable = false;
            this.Text = "Synergy - AutoLaunch";

            jobManager = new JobManager((value) =>
            {
                roundedProgressBar1.Progress = value;
                UpdateLabel();
            });

            jobManager.AddJob(new Job(() => { return versionValid == true; }, () => { loadingSuffix = "Checking Version"; Program.StartRoblox(); }));
            jobManager.AddJob(new Job(() => { return IsRobloxRunning(); }, () => { loadingSuffix = "Waiting"; }));
            //jobManager.AddJob(new Job(() => { return isWhitelisted; }, () => { loadingSuffix = "Checking whitelist"; })); // to test how it looks
            jobManager.AddJob(new Job(() => { return true; }, () => {
                loadingSuffix = "Injecting";

                SirHurtAPI.Inject(false);
            }));
            jobManager.AddJob(new Job(() => { return false; }, () => {
                loadingSuffix = "Have fun!";

                Focus();
                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(1000);
                    RobloxClient.ExitApp();
                });
            }));
            jobManager.End();
        }

        public bool IsRobloxRunning() => Process.GetProcesses().Any(proc => proc.MainWindowTitle == "Roblox");

        public void CancelShutdown()
        {
            robloxTimer.Enabled = false;
            SuspendTimer.Enabled = false;
            Close();
        }

        public void InitRobloxDetectTask()
        {

        }

        private void RobloxTimer_Tick(object sender, EventArgs e)
        {
            if (RobloxClient.Process.curPlace != null && placeId == null)
            {
                placeId = HttpUtility.UrlDecode(Program.la.PlaceLauncherUrl)
                .Split('&')[2]
                .Split('=')[1];

                switch (placeId)
                {
                    case "4483381587":
                        break;
                }
            }
        }

        public string GetGameTitle()
        {
            if (RobloxClient.Process.curPlace != null)
                return "\n" + CleanText(RobloxClient.Process.curPlace.data.First().sourceName);

            return "";
        }

        public void UpdateLabel()
        {
            dots = (dots == 3) ? 1 : dots + 1;
            label1.Text = !loadingSuffix.EndsWith("!") ? $"{loadingSuffix}{new string('.', dots)}" : loadingSuffix;//{GetGameTitle()}
        }

        private void SuspendTimer_Tick(object sender, EventArgs e)
        {
            UpdateLabel();
            jobManager.TickJobs();
        }

        public static string CleanText(string input)
        {
            string pattern = @"[^a-zA-Z0-9!@#$%^&*()\[\]{} ]";
            return Regex.Replace(input, pattern, "");
        }

        private void LauncherWindow_Load(object sender, EventArgs e) { }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        public void VersionValid() => versionValid = true;

        public void VersionInvalid()
        {
            versionValid = false;

            robloxTimer.Enabled = false;
            SuspendTimer.Enabled = false;

            TopMost = false;

            var startInfo = new ProcessStartInfo
            {
                FileName = Application.ExecutablePath,
                Verb = "runas",
                Arguments = $"--reinstall {Program.args[0]}"
            };

            try
            {
                Process.Start(startInfo);
            }
            catch (System.ComponentModel.Win32Exception)
            {
                MessageBox.Show("Admin privileges are required, restart installer with admin.");
            }

            RobloxClient.ExitApp();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            SoundPlayer.PlayClick(); // funny easter egg
        }
    }
}
