using EasySave.Services;

namespace EasySave.Commands
{
    public class NavigateCommand : CommandBase
    {
        private readonly NavigationService _navigationService;

        public NavigateCommand(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            //Navigate to the target page
            _navigationService.Navigate();
        }
    }
}
