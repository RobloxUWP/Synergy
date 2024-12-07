using System;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows;
using System.Security.Cryptography;
using System.Windows.Media.Media3D;

namespace RobloxAutoLaunch.Windows
{
    public static class TitlebarUtil
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);

        public enum WindowCompositionAttribute
        {
            WCA_ACCENT = 19
        }
        
        public static void ApplyCustomTitlebar(Window window)
        {
            IntPtr hwnd = new WindowInteropHelper(window).Handle;

            window.WindowStyle = WindowStyle.None;
            window.AllowsTransparency = true;
            window.ResizeMode = ResizeMode.CanResizeWithGrip;
        
            EnableWindows11Style(hwnd);

        }

        private static void EnableWindows11Style(IntPtr hwnd)
        {

        }
    }
}
