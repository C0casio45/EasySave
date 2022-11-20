using EasySave.Stores;
using EasySave.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave.Services
{
    public class NavigationService
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<ViewModelBase> _createViewModel;

        public NavigationService(NavigationStore navigationStore, Func<ViewModelBase> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }
        public void Navigate()
        {
            //update the currentViewModel by creating a new viewModel
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
