using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EasySave.Models
{
    public class CExtension : ACFileHandler, JsonSerializable
    {
        public string SName { get; set; }

        public CExtension(string sName)
        {
            SName = sName;
        }

        public void CreateJsonExtension()
        {
            //Call json file handler to create
            if (File.Exists(@".\UserSettings\extToEncrypt.json"))
            {
                this.WriteJsonFile(this, @".\UserSettings\extToEncrypt.json");
            }
            else
            {
                this.WriteJsonFile(this, Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\UserSettings\extToEncrypt.json");
            }
        }

        public void DeleteJsonExtension()
        {
            //Call JsonFileHandler method to delete
            if (File.Exists(@".\UserSettings\extToEncrypt.json"))
            {
                this.RemoveJsonFile(this, @".\UserSettings\extToEncrypt.json");
            }
            else
            {
                this.RemoveJsonFile(this, Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\UserSettings\extToEncrypt.json");
            }
        }
    }
}
