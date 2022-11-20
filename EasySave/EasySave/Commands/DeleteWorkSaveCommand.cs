using EasySave.Models;
using EasySave.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EasySave.Commands
{
    public class DeleteWorkSaveCommand : CommandBase
    {
        private readonly CWorkSave _workSave;
        private readonly WorkSaveListingViewModel _workSaveListingViewModel;

        public DeleteWorkSaveCommand(CWorkSave workSave, WorkSaveListingViewModel workSaveListingViewModel)
        {
            _workSave = workSave;
            _workSaveListingViewModel = workSaveListingViewModel;
        }
        public override void Execute(object parameter)
        {
            //Delete json worksave and update the view
            DialogResult dialogResult = MessageBox.Show("Are you sure to delete ?", "Warning", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                _workSave.DeleteJsonWorkSave();
                _workSaveListingViewModel.UpdateWorkSaves();
            }
        }
    }
}
