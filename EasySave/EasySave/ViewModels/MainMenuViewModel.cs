using EasySave.Commands;
using EasySave.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EasySave.ViewModels
{
    public class MainMenuViewModel : ViewModelBase
    {
        //this is the mainMenuViewmodel which permit to navigate between worksaves and settings
        public ICommand SettingsNavCommand { get; }
        public ICommand WorkSaveListingNavCommand { get; }

        public MainMenuViewModel(NavigationService navigationService, NavigationService navigationService1)
        {
            SettingsNavCommand = new NavigateCommand(navigationService);
            WorkSaveListingNavCommand = new NavigateCommand(navigationService1);
        }
    }
}
