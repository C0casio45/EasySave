using EasySave_Client.Models;
using EasySave_Client.ViewModels;
using Newtonsoft.Json;
using System;

namespace EasySave_Client.Commands
{
    public class StartWorkSaveCommand : CommandBase
    {
        private readonly CWorkSave _workSave;
        private readonly NetworkViewModel _networkViewModel;

        public StartWorkSaveCommand(CWorkSave workSave, NetworkViewModel networkViewModel)
        {
            _workSave = workSave;
            _networkViewModel = networkViewModel;
        }

        public override void Execute(object parameter)
        {
            //Send a message to the server to start the worksave
            _networkViewModel.SendDatas("<PLAY>" + _workSave.SName);
        }
    }
}
