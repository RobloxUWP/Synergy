using System;
using System.Windows;

using RobloxAutoLaunch.Windows;

public class Program
{
    public static Application app;
    public static LauncherInstaller win;

    public static void PrepareInstaller()
    {
        app = new Application();
        win = new LauncherInstaller();
        app.Run(win);
    }

    [STAThread]
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            PrepareInstaller();
        }
    }
}
