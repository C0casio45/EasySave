using EasySave.Commands;
using EasySave.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EasySave.ViewModels
{
    public class WorkSaveViewModel : ViewModelBase
    {
        //This is an abstraction for the worksaves objects be used by viewmodel
        private readonly CWorkSave _workSave;
        private readonly WorkSaveListingViewModel _workSaveListingViewModel;

        public string Name => _workSave.SName;
        public string SourcePath => _workSave.SSourcePath;
        public string TargetPath => _workSave.STargetPath;
        public string Type => _workSave.BDiscriminating?"Discriminating":"Complete";
        public ICommand StartWorkSaveCommand { get; }
        public ICommand DeleteWorkSaveCommand { get; }

        public WorkSaveViewModel(CWorkSave workSave, WorkSaveListingViewModel workSaveListingViewModel)
        {
            _workSave = workSave;
            _workSaveListingViewModel = workSaveListingViewModel;
            StartWorkSaveCommand = new StartOneWorkSaveCommand(_workSave);
            DeleteWorkSaveCommand = new DeleteWorkSaveCommand(_workSave, _workSaveListingViewModel);
        }
    }
}
