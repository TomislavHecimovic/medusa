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
                try
                {
                    afterburnerControl = Utils.MsiAfterburner.verifyAfterburnerInstallation();
                    MessageBox.Show("Afterburner is running");
                    // TODO: load settings and GPUs
                    //afterburnerControl.GpuEntries;
                }
                catch (Exceptions.MsiAfterburnerNotInstalled ex)
                {
                    MessageBox.Show(ex.Message);
                    // TODO: offer download instructions
                }
                catch (Exceptions.MsiAfterburnerNotStarted ex)
                {
                    MessageBox.Show(ex.Message + "\nPath:" + ex.ExeFullPath);
                    // TODO: Ask the question and offer Admin privileged afterburner start
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
            IsProgressVisible = Views.Busy.BusyType.NONE;
        }
    }
}
