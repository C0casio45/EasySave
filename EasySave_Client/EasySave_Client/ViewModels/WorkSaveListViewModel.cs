using EasySave_Client.Commands;
using EasySave_Client.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Input;

namespace EasySave_Client.ViewModels
{
    public class WorkSaveListViewModel : ViewModelBase
    {
        private ObservableCollection<WorkSaveViewModel> _workSaves;
        public IEnumerable<WorkSaveViewModel> WorkSaves => _workSaves;

        public readonly NetworkViewModel _networkViewModel;

        public ICommand SetServerIpCommand { get; }

        public WorkSaveListViewModel(NetworkViewModel networkViewModel)
        {
            _workSaves = new AsyncObservableCollection<WorkSaveViewModel>();
            _networkViewModel = networkViewModel;
            // Start a thread to continously update the view
            new Thread(UpdateWorkSave).Start();
        }

        public void UpdateWorkSave(object obj)
        {
            List<CWorkSave> workSaveList;
            Dictionary<string, int> progress;
            while (true)
            {
                //Reset the list of worksaves
                _workSaves.Clear();

                if (_networkViewModel._data != null)
                {
                    //Transform to object the worksaves received from the server
                    string jsonWorksaves = _networkViewModel._data.Substring(0, _networkViewModel._data.LastIndexOf("<"));
                    workSaveList = JsonConvert.DeserializeObject<List<CWorkSave>>(jsonWorksaves);

                    //Transform to dictionnary (worksave name => progression) the data received from the server
                    string dictionnaryProgress = _networkViewModel._data.Substring(_networkViewModel._data.LastIndexOf(">")+1);
                    progress = JsonConvert.DeserializeObject<Dictionary<string, int>>(dictionnaryProgress);

                    //Creates Cworksaves object with the informations and the progression received from the server
                    foreach (CWorkSave workSave in workSaveList)
                    {
                        try
                        {
                            _workSaves.Add(new WorkSaveViewModel(workSave, this, progress[workSave.SName]));
                        }
                        catch (Exception)
                        {

                            
                        }
                    }

                    workSaveList.Clear();
                }
                Thread.Sleep(750);
            }
        }
    }
}