using System;
using System.Windows;

namespace RobloxAutoLaunch.Windows
{
    public partial class LauncherInstaller : Window
    {
        public LauncherInstaller()
        {
            InitializeComponent();
            TitlebarUtil.ApplyCustomTitlebar(this);
        }
    }
}
