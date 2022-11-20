using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EasySave.Models
{
    public class CBusinessSoftware : ACFileHandler, JsonSerializable
    {
        public string SName { get; set; }

        public CBusinessSoftware(string sName)
        {
            SName = sName;
        }

        public void CreateJsonBusinessSoftware()
        {
            //Path for msi installer
            if (File.Exists(@".\UserSettings\businessSoftware.json"))
            {
                File.WriteAllText(@".\UserSettings\businessSoftware.json", "{SName: \"" + SName + "\"}");
            }
            //path for debuging
            else
            {
                File.WriteAllText(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\UserSettings\businessSoftware.json", "{SName: \"" + SName + "\"}");
            }
        }
    }
}
