using EasySave_Client.Models;
using EasySave_Client.ViewModels;
using System;

namespace EasySave_Client.Commands
{
    public class PauseWorkSaveCommand : CommandBase
    {
        private readonly CWorkSave _workSave;
        private readonly NetworkViewModel _networkViewModel;

        public PauseWorkSaveCommand(CWorkSave workSave, NetworkViewModel networkViewModel)
        {
            _workSave = workSave;
            _networkViewModel = networkViewModel;
        }

        public override void Execute(object parameter)
        {
            //Send a message to the server to pause the worksave
            _networkViewModel.SendDatas("<PAUSE>" + _workSave.SName);
        }
    }
}
