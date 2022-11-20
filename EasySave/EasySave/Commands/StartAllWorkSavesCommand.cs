using EasySave.Models;
using EasySave.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace EasySave.Commands
{
    public class StartAllWorkSavesCommand : CommandBase
    {
        private readonly WorkSaveListingViewModel _workSaveListingViewModel;

        public StartAllWorkSavesCommand(WorkSaveListingViewModel workSaveListingViewModel)
        {
            _workSaveListingViewModel = workSaveListingViewModel;
        }

        public override void Execute(object parameter)
        {
            //List the worksaves and try to start each ones
            List<String> failedWorksaves = new List<string>();
            foreach (WorkSaveViewModel workSave in _workSaveListingViewModel.WorkSaves)
            {
                try
                {
                    workSave.StartWorkSaveCommand.Execute(null);
                }
                catch (Exception e)
                {
                    failedWorksaves.Add(workSave.Name+" failed : "+e.Message+"\n");
                }
            }
            //return validation message
            if (failedWorksaves == null)
            {
                MessageBox.Show("All worksaves succeded");
            }
            else
            {
                //return error message
                string message = "";
                foreach (string name in failedWorksaves)
                {
                    message += name;
                }
                MessageBox.Show(message);
            }
        }
    }
}
