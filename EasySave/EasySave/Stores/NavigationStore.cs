using EasySave.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave.Stores
{
    public class NavigationStore
    {
        //Store the current viewModel and the mainMenu
        public MainMenuViewModel MainMenuViewModel { get; set; }
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
        //event for updating the currentviewmodel when it change
        public event Action CurrentViewModelChanged;
    }
}
