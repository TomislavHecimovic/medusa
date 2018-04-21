using System.Windows;

namespace Medusa
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Views.Busy.BusyType IsProgressVisible { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IsProgressVisible = Views.Busy.BusyType.INDETERMINATE;
            MSI.Afterburner.ControlMemory afterburnerControl = null;
            await System.Threading.Tasks.Task.Run(() =>
            {
                afterburnerControl = verifyAfterburnerInstallation();
            });
            IsProgressVisible = Views.Busy.BusyType.NONE;

            if(null != afterburnerControl)
            {
                MessageBox.Show("Afterburner is running");
                // TODO: load settings and GPUs
            }
        }

        private MSI.Afterburner.ControlMemory verifyAfterburnerInstallation()
        {
            string installPath = null;
            try
            {
                installPath = Utils.Windows.getInstallPath("MSI Afterburner");
                MSI.Afterburner.ControlMemory cm = new MSI.Afterburner.ControlMemory();

                // List the GPUs and provide
                var gpus = cm.GpuEntries;
                return cm;
            }
            catch (System.Exception ex)
            {
                if (ex.Message == "Could not connect to MSI Afterburner 2.1 or later.")
                {
                    // TODO: Be a bit smarter and try to find the Afterburner app on the disk in obvious locations like 
                    //       C:\Program Files (x86)\MSI Afterburner, or similar. Then offer an option to start it if it is installed.
                    if (string.IsNullOrEmpty(installPath))
                    {
                        MessageBox.Show("It looks like  MSI Afterburner is not installed. The app cannot be used without it.");
                        // TODO: exit the app?
                    }
                    else
                    {
                        string exeFullPath = installPath + "\\MSIAfterburner.exe";
                        if (System.IO.File.Exists(exeFullPath))
                        {
                            MessageBox.Show("MSI Afterburner is not started. The app cannot be used without it. Start the Afterburner app?");
                            // TODO: Ask the question and offer Admin privileged afterburner start
                            return null;
                        }
                        else
                        {
                            MessageBox.Show("MSI Afterburner appears to be installed but the executable could not be found. Please verify the MSI Afterburner installation and make sure it is running.");
                            // TODO: offer download instructions
                        }
                    }

                }
                else
                    throw ex;
            }

            return null;
        }
    }
}
