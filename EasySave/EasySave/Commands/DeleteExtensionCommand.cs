using EasySave.Models;
using EasySave.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave.Commands
{
    public class DeleteExtensionCommand : CommandBase
    {
        private readonly CExtension _extension;
        private readonly SettingsViewModel _settingsViewModel;

        public DeleteExtensionCommand(CExtension extension, SettingsViewModel settingsViewModel)
        {
            _settingsViewModel = settingsViewModel;
            _extension = extension;
        }

        public override void Execute(object parameter)
        {
            //Delete the extension from json and update the view
            _extension.DeleteJsonExtension();
            _settingsViewModel.UpdateExtensions();
        }
    }
}
