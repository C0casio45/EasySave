using EasySave_Client.Models;
using EasySave_Client.Commands;
using System.Windows.Input;

namespace EasySave_Client.ViewModels
{
    public class WorkSaveViewModel : ViewModelBase
    {
        //This is an abstraction for the worksaves objects be used by viewmodel
        private readonly CWorkSave _workSave;
        private readonly WorkSaveListViewModel _workSaveListViewModel;

        public string Name => _workSave.SName;
        public string SourcePath => _workSave.SSourcePath;
        public string TargetPath => _workSave.STargetPath;
        public string Type => _workSave.BDiscriminating ? "Discriminating" : "Complete";

        //Link the view and the viewmodel with an event
        private int _progressionPercentage;
        public int ProgressionPercentage
        {
            get => _progressionPercentage;
            set
            {
                _progressionPercentage = value;
                OnPropertyChanged(nameof(ProgressionPercentage));
            }
        }

        public ICommand StartWorkSaveCommand { get; }
        public ICommand PauseWorkSaveCommand { get; }
        public ICommand StopWorkSaveCommand { get; }

        public WorkSaveViewModel(CWorkSave workSave, WorkSaveListViewModel workSaveListingViewModel, int progressionPercentage)
        {
            _workSave = workSave;
            _workSaveListViewModel = workSaveListingViewModel;
            _progressionPercentage = progressionPercentage;

            StartWorkSaveCommand = new StartWorkSaveCommand(_workSave, _workSaveListViewModel._networkViewModel);
            PauseWorkSaveCommand = new PauseWorkSaveCommand(_workSave, _workSaveListViewModel._networkViewModel);
            StopWorkSaveCommand = new StopWorkSaveCommand(_workSave, _workSaveListViewModel._networkViewModel);
        }
    }
}
