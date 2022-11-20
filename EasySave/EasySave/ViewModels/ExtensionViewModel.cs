using EasySave.Commands;
using EasySave.Models;
using System.Windows.Input;

namespace EasySave.ViewModels
{
    public class ExtensionViewModel : ViewModelBase
    {
        //this class abstact the CExtension objects to be used by the WorkSaveListingViewModel
        private readonly CExtension _extension;
        private readonly SettingsViewModel _settingsViewModel;

        public string Name => _extension.SName;
        public ICommand DeleteExtensionCommand { get; }

        public ExtensionViewModel(CExtension extension, SettingsViewModel settingsViewModel)
        {
            _extension = extension;
            _settingsViewModel = settingsViewModel;
            DeleteExtensionCommand = new DeleteExtensionCommand(_extension, _settingsViewModel);
        }
    }
}
