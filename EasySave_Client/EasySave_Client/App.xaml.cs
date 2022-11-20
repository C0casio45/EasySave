using System.Windows;
using EasySave_Client.ViewModels;

namespace EasySave_Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            
            NetworkViewModel network = new NetworkViewModel();
            
            MainWindow = new MainWindow()
            {
                DataContext = new WorkSaveListViewModel(network)
            };
            MainWindow.Show();

        }
    }
}
