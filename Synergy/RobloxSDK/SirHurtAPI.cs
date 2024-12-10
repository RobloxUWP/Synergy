using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Synergy.RobloxSDK
{
    public class SirHurtAPI
    {
        /// <summary>
        /// The directory where SirHurt stores its data
        /// </summary>
        public static DirectoryInfo SirHurtDirectory =
            new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\sirhurt\\sirhui");

        /// <summary>
        /// Injects SirHurt into Roblox silently
        /// </summary>
        /// <param name="wait">If the function should wait until the injector closes (Useless..)</param>
        public static void Inject(bool wait = false)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = Application.StartupPath + "\\sirhurt.exe",
                WorkingDirectory = Application.StartupPath,
                RedirectStandardOutput = true, // TODO: read if it successfully injected or not
                UseShellExecute = false,
                CreateNoWindow = true
            };

            var proc = Process.Start(startInfo);

            if (wait)
                proc.WaitForExit();
        }

        /// <summary>
        /// Enables or disables the drawing library
        /// </summary>
        public static bool DrawLibEnabled
        {
            get => File.ReadAllText(SirHurtDirectory.FullName + "\\debuglibenabled.txt") == "true" ? true : false;
            set => File.WriteAllText(SirHurtDirectory.FullName + "\\debuglibenabled.txt", value.ToString().ToLower());
        }

        /// <summary>
        /// The script that is currently being executed by SirHurt
        /// </summary>
        public static string QueuedScript
        {
            get => File.ReadAllText(SirHurtDirectory.FullName + "\\sirhurt.dat");
            set => File.WriteAllText(SirHurtDirectory.FullName + "\\sirhurt.dat", value.ToString());
        }
    }
}
