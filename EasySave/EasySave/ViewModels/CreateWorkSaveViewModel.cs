using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using EasySave.Commands;
using EasySave.Services;
using EasySave.Stores;

namespace EasySave.ViewModels
{
    //
    // ViewModel used create a WorkSave
    // Every private attribute is Binded with the View CreateWorkSaveView
    //
    public class CreateWorkSaveViewModel : ViewModelBase
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private string _sourcePath;
        public string SourcePath
        {
            get => _sourcePath;
            set
            {
                _sourcePath = value;
                OnPropertyChanged(nameof(SourcePath));
            }
        }
        private string _targetPath;
        public string TargetPath
        {
            get => _targetPath;
            set
            {
                _targetPath = value;
                OnPropertyChanged(nameof(TargetPath));
            }
        }
        private bool _type;
        public bool Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged(nameof(Type));
            }
        }
        public ICommand SetSourceCommand { get; }
        public ICommand SetTargetCommand { get; }
        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public CreateWorkSaveViewModel(NavigationService workSaveListingViewnavigationService)
        {
            SubmitCommand = new CreateWorkSaveCommand(this, workSaveListingViewnavigationService);
            CancelCommand = new NavigateCommand(workSaveListingViewnavigationService);
            SetSourceCommand = new SetFolderCommand("Source", this);
            SetTargetCommand = new SetFolderCommand("Target", this);
        }
    }
}
