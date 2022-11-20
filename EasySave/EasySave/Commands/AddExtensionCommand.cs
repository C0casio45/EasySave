using EasySave.Models;
using EasySave.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EasySave.Commands
{
    class AddExtensionCommand : CommandBase
    {
        private readonly SettingsViewModel _settingsViewModel;

        public AddExtensionCommand(SettingsViewModel settingsViewModel)
        {
            _settingsViewModel = settingsViewModel;
            _settingsViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }
        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(_settingsViewModel.ExtensionName) && base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            //Create the new extension write it in json and update the list of extensions to display at the user
            CExtension extension = new CExtension(_settingsViewModel.ExtensionName);
            extension.CreateJsonExtension();
            _settingsViewModel.UpdateExtensions();
        }
        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SettingsViewModel.ExtensionName))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
