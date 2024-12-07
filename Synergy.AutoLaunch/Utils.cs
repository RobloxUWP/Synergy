using System.Security.Principal;

namespace RobloxAutoLaunch
{
    public class Utils
    {
        public static bool CheckAdminPerms() => new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
    }
}
