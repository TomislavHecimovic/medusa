using Microsoft.Win32;
using System.Linq;

namespace Medusa.Utils
{
    public static class Windows
    {
        public static string getInstallPath(string applicationName)
        {
            string displayName;
            object installPath = null;

            string registryKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            RegistryKey key = Registry.LocalMachine.OpenSubKey(registryKey);
            if (key != null)
            {
                foreach (RegistryKey subkey in key.GetSubKeyNames().Select(keyName => key.OpenSubKey(keyName)))
                {
                    displayName = subkey.GetValue("DisplayName") as string;
                    if (displayName != null && displayName.Contains(applicationName))
                    {
                        installPath = subkey.GetValue("UninstallString");
                        key.Close();
                        break;
                    }
                }
                key.Close();
            }

            if (installPath == null)
            {
                registryKey = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall";
                key = Registry.LocalMachine.OpenSubKey(registryKey);
                if (key != null)
                {
                    foreach (RegistryKey subkey in key.GetSubKeyNames().Select(keyName => key.OpenSubKey(keyName)))
                    {
                        displayName = subkey.GetValue("DisplayName") as string;
                        if (displayName != null && displayName.Contains(applicationName))
                        {
                            installPath = subkey.GetValue("UninstallString");
                            key.Close();
                            break;
                        }
                    }
                    key.Close();
                }
            }

            if(installPath is string)
            {
                string path = System.IO.Path.GetDirectoryName(((string)installPath).Replace("\"", ""));
                if(System.IO.Directory.Exists(path))
                {
                    return path;
                }
            }

            return string.Empty;
        }
    }
}
