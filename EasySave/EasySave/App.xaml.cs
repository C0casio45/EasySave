using EasySave.Services;
using EasySave.Stores;
using EasySave.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace EasySave
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;
        public App()
        {
            _navigationStore = new NavigationStore();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationStore.CurrentViewModel = CreateWorkSaveListingViewModel();
            _navigationStore.MainMenuViewModel = CreateMainMenuViewModel();
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
        private WorkSaveListingViewModel CreateWorkSaveListingViewModel()
        {
            return new WorkSaveListingViewModel(new NavigationService(_navigationStore, CreateCreateWorkSaveViewModel));
        }
        private CreateWorkSaveViewModel CreateCreateWorkSaveViewModel()
        {
            return new CreateWorkSaveViewModel(new NavigationService(_navigationStore, CreateWorkSaveListingViewModel));
        }
        private SettingsViewModel CreateSettingsViewModel()
        {
            return new SettingsViewModel(new NavigationService(_navigationStore, CreateWorkSaveListingViewModel));
        }
        private MainMenuViewModel CreateMainMenuViewModel()
        {
            return new MainMenuViewModel(new NavigationService(_navigationStore, CreateSettingsViewModel), new NavigationService(_navigationStore,CreateWorkSaveListingViewModel));
        }
    }
}
