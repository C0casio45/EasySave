using EasySave.Models;
using EasySave.Services;
using EasySave.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EasySave.Commands
{
    public class CreateWorkSaveCommand : CommandBase
    {
        private readonly CreateWorkSaveViewModel _createWSViewModel;
        private readonly NavigationService _workSaveListingViewnavigationService;

        public CreateWorkSaveCommand(CreateWorkSaveViewModel createWSViewModel, NavigationService workSaveListingViewnavigationService)
        {
            _createWSViewModel = createWSViewModel;
            _workSaveListingViewnavigationService = workSaveListingViewnavigationService;
            _createWSViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }
        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(_createWSViewModel.Name) && !string.IsNullOrEmpty(_createWSViewModel.SourcePath) &&
                !string.IsNullOrEmpty(_createWSViewModel.TargetPath) && base.CanExecute(parameter);
        }
        public override void Execute(object sender)
        {
            //Create the workSave, write in json and navigate to the worksavelisting view
            CWorkSave WorkSave = new CWorkSave(_createWSViewModel.Name, _createWSViewModel.SourcePath, _createWSViewModel.TargetPath, _createWSViewModel.Type);
            WorkSave.CreateJsonWorkSave();
            _workSaveListingViewnavigationService.Navigate();
        }
        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CreateWorkSaveViewModel.Name) || e.PropertyName == nameof(CreateWorkSaveViewModel.SourcePath) ||
                e.PropertyName == nameof(CreateWorkSaveViewModel.TargetPath))
            {
                OnCanExecutedChanged();
            }
        }
    }
}