using System;

namespace Medusa.Utils
{
    public class MsiAfterburner
    {
        public static MSI.Afterburner.ControlMemory verifyAfterburnerInstallation()
        {
            string installPath = null;
            try
            {
                installPath = Windows.getInstallPath("MSI Afterburner");
                MSI.Afterburner.ControlMemory cm = new MSI.Afterburner.ControlMemory();
                return cm;
            }
            catch (Exception ex)
            {
                if (ex.Message == "Could not connect to MSI Afterburner 2.1 or later.")
                {
                    if (string.IsNullOrEmpty(installPath))
                        throw new Exceptions.MsiAfterburnerNotInstalled("It looks like MSI Afterburner is not installed. The app cannot be used without it.");
                    else
                    {
                        string exeFullPath = installPath + "\\MSIAfterburner.exe";
                        if (System.IO.File.Exists(exeFullPath))
                            throw new Exceptions.MsiAfterburnerNotStarted("MSI Afterburner is not started. The app cannot be used without it.",  exeFullPath);
                        else
                            throw new Exceptions.MsiAfterburnerNotInstalled("MSI Afterburner appears to be installed but the executable could not be found. Please verify the MSI Afterburner installation and make sure it is running.");
                    }
                }
                else
                    throw ex;
            }
        }
    }
}
