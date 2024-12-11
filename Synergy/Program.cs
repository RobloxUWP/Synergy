#region Imports

using Synergy.RobloxSDK;
using Synergy.Windows;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using System.MDI;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

#endregion

namespace Synergy
{
    internal class Program
    {
        public static LauncherArgs la;
        public static MDIInIFile config = new MDIInIFile();

        public static bool CheckAdminPerms() => new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

        public static string[] args;

        // my C# is corrupt so this isn't needed..
        static void DisplayError(Exception ex)
        {
            var stackTrace = new StackTrace(ex, true);
            var frame = stackTrace.GetFrame(0);
            var lineNumber = frame.GetFileLineNumber();
            var fileName = frame.GetFileName();

            MessageBox.Show($"Exception: {ex.Message}\n" +
                            $"Source: {ex.Source}\n" +
                            $"File: {fileName}\n" +
                            $"Line: {lineNumber}\n" +
                            $"StackTrace: {ex.StackTrace}", "Unhandled Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        [STAThread]
        static void Main(string[] cmdLineArgs)
        {
            args = cmdLineArgs;

            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                var exception = (Exception)e.ExceptionObject;
                DisplayError(exception);
            };

            if (args.Length == 0)
            {
                if (CheckAdminPerms())
                    Application.Run(new InstallerWindow());

                Application.Run(new MainWindow());
            }
            else
            {
                if (args[0] == "--reinstall")
                    ReinstallRoblox();

                if (args[0] == "--refresh")
                    RefreshSynergy();

                la = Launcher.ParseArgs(args[0]);
                Task.Factory.StartNew(() => Application.Run(new LauncherWindow()));

                string robloxPath = RobloxClient.GetRobloxVersionPath();
                if (string.IsNullOrEmpty(robloxPath))
                {
                    LauncherWindow.Window.VersionInvalid();
                    Thread.Sleep(-1);
                }
                else
                {
                    CheckReinstallRequired();
                    while (LauncherWindow.Window == null) { } // avoid race condition cuz im using concurrency alot..
                    LauncherWindow.Window.VersionValid();
                    Launchable = true;

                    string placeId = HttpUtility.UrlDecode(la.PlaceLauncherUrl).Split('&')[2].Split('=')[1];
                    RobloxClient.Process.curPlace = RobloxAPI.GetMainUniverse(placeId);
                    
                    Thread.Sleep(-1); // pause app
                }
            }
        }

        public static bool Launchable = false;

        private static void RefreshSynergy()
        {
            bool autolauncb = Program.config.KeyExists("AutoLaunch", "Options") && Program.config.Read("AutoLaunch", "Options") == "true";

            if (autolauncb)
            {
                RobloxClient.Process.ReplaceRoblox();
                Program.config.Write("RequiresReinstall", "0", "System");
            }
            else
            {
                string versionPath = RobloxClient.GetRobloxVersionPath();

                if (string.IsNullOrEmpty(versionPath))
                {
                    MessageBox.Show("Latest roblox version not detected (FATAL FAILURE)", "Synergy");
                    Program.config.Write("AutoLaunch", "false", "Options");
                    return;
                }

                RobloxClient.Process.ReplaceRoblox(versionPath + "\\RobloxPlayerLauncher.exe");
            }

            RobloxClient.ExitApp();
        }

        static void ReinstallRoblox()
        {
            RobloxClient.Process.version = RobloxAPI.GetVersion();
            RobloxClient.UpdateRoblox();

            while (Process.GetProcessesByName("RobloxPlayerLauncher").Length == 0) { Thread.Sleep(1); }

            while (true)
            {
                Thread.Sleep(1);

                if (Process.GetProcessesByName("RobloxPlayerLauncher").Length == 0)
                {
                    RobloxClient.Process.ReplaceRoblox();
                    config.Write("RequiresReinstall", "0", "System");

                    // avoid some bugs i had.. (roblox as admin avoids autoclicking and several other issues)
                    var startInfo = new ProcessStartInfo
                    {
                        FileName = Application.ExecutablePath,
                        Arguments = args[1]
                    };

                    Process.Start(startInfo);

                    RobloxClient.ExitApp();

                    break;
                }
            }
        }
        
        static void CheckReinstallRequired()
        {
            if (File.Exists(MDI.mdiBase + "config.ini"))
            {
                if (config.KeyExists("RequiresReinstall", "System")
                    && config.Read("RequiresReinstall", "System") != "0")
                {
                    if (!CheckAdminPerms())
                    {
                        MessageBox.Show("Roblox cant start due to needing a reinstall", "Synergy");
                        RobloxClient.ExitApp();
                    }
                }
            }
        }

        public static void StartRoblox()
        {
            string robloxPath = RobloxClient.GetRobloxVersionPath();

            RobloxClient.InitMutex(); // legacy
            Task.Factory.StartNew(() =>
            {
                RobloxClient.Process.roblox = Process.Start(robloxPath + "\\RobloxPlayerBeta.exe", args[0]);
            });
        }
    }
}