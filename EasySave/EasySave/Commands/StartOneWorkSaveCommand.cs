using EasySave.Models;
using EasySave.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace EasySave.Commands
{
    public class StartOneWorkSaveCommand : CommandBase
    {
        private readonly CWorkSave _workSave;

        public StartOneWorkSaveCommand(CWorkSave workSave)
        {
            _workSave = workSave;
        }

        public override void Execute(object parameter)
        {
            //try to execute the worksave
            try
            {
                _workSave.SaveFolder();
                if (parameter != null && parameter.ToString() == "single")
                {
                    MessageBox.Show(_workSave.SName + " Succeded");
                }
            }
            catch (Exception e)
            {
                //return errors
                if (parameter != null && parameter.ToString() == "single")
                {
                    MessageBox.Show(_workSave.SName + " Failed" + " : "+e.Message);
                }
                else
                {
                    throw e;
                }
            }
        }
    }
}