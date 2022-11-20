using EasySave.Commands;
using EasySave.Models;
using EasySave.Services;
using EasySave.Stores;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EasySave.ViewModels
{
    public class WorkSaveListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<WorkSaveViewModel> _workSaves;

        public IEnumerable<WorkSaveViewModel> WorkSaves => _workSaves;

        public ICommand AddWorkSaveNavCommand { get; }
        public ICommand StartAllWorkSavesCommand { get; }

        public WorkSaveListingViewModel(NavigationService CreateWorkSaveNavigationService)
        {
            _workSaves = new ObservableCollection<WorkSaveViewModel>();
            AddWorkSaveNavCommand = new NavigateCommand(CreateWorkSaveNavigationService);
            StartAllWorkSavesCommand = new StartAllWorkSavesCommand(this);
            UpdateWorkSaves();
        }

        public void UpdateWorkSaves()
        {
            //Clear the workSave list and read the json file to update
            _workSaves.Clear();
            List<CWorkSave> workSaveList;
            try
            {
                workSaveList = JsonConvert.DeserializeObject<List<CWorkSave>>(File.ReadAllText("./UserSettings/workSaves.json"));
            }
            catch (Exception)
            {
                workSaveList = JsonConvert.DeserializeObject<List<CWorkSave>>(File.ReadAllText(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\UserSettings\workSaves.json"));
            }

            foreach (CWorkSave workSave in workSaveList)
            {
                _workSaves.Add(new WorkSaveViewModel(workSave, this));
            }
        }
    }
}
