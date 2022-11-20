using EasySave.Models;
using EasySave.ViewModels;
using System.ComponentModel;

namespace EasySave.Commands
{
    //
    // Command used in the view model to add and modify the business software 
    //
    public class AddBusinessSoftwareCommand : CommandBase
    {
        private readonly SettingsViewModel _settingsViewModel;

        public AddBusinessSoftwareCommand(SettingsViewModel settingsViewModel)
        {
            _settingsViewModel = settingsViewModel;
            _settingsViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }
        // Check if the command can be executed
        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(_settingsViewModel.BusinessSoftware) && base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            // Create a new businessSoftware object and write it in the businessSoftware.json setting file
            CBusinessSoftware businessSoftware = new CBusinessSoftware(_settingsViewModel.BusinessSoftware);
            businessSoftware.CreateJsonBusinessSoftware();
        }
        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SettingsViewModel.BusinessSoftware))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
