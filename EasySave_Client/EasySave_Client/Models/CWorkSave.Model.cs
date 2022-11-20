using EasySave_Client.ViewModels;

namespace EasySave_Client.Models
{
    public class CWorkSave : ViewModelBase
    {
        // Work saves attributs
        public string SName { get; set; }
        public string SSourcePath { get; set; }
        public string STargetPath { get; set; }
        public bool BDiscriminating { get; set; }

        public CWorkSave(string sName, string sSourcePath, string sTargetPath, bool bDiscriminating)
        {
            SName = sName;
            SSourcePath = sSourcePath;
            STargetPath = sTargetPath;
            BDiscriminating = bDiscriminating;
        }
    }
}
