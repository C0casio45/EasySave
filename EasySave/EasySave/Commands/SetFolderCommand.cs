using EasySave.ViewModels;
using System;
using System.Linq;
using System.Windows.Media;

//Write the folder path in the desired textbox (source or target) to make a better UX

namespace EasySave.Commands
{
    public class SetFolderCommand : CommandBase
    {
        private readonly string _value;
        private CreateWorkSaveViewModel _createWSViewModel;

        public SetFolderCommand(string Value, CreateWorkSaveViewModel CreateWSViewModel)
        {
            _value = Value;                                                                 //AKA Source or Target
            _createWSViewModel = CreateWSViewModel;                                         //AKA Parent (to updates their attributes)
        }



        public override void Execute(object parameter)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())             //Call this namespace in controlled area to prevent conflict with wpf
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();             //Display the research folder windows tool to the user

                if (result == System.Windows.Forms.DialogResult.OK)                         //When the user close the dialog, then ...
                {
                    switch (_value)
                    {
                        case "Source":
                            _createWSViewModel.SourcePath = dialog.SelectedPath;            //Returned path is the Source Path
                            break;
                        case "Target":
                            _createWSViewModel.TargetPath = dialog.SelectedPath;            //Returned path is the Target Path
                            break;
                        default:
                            break;
                    }
                }


            }
        }

    }
}
