using EasySave.ViewModels;
using System;
using System.IO;

namespace EasySave.Commands
{
    public class SetLoggingTypeCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            // Error handling 
            if (File.Exists(@".\UserSettings\logSettings.json"))
            {
                // Write the type of logs type we want to use (JSON or XML)
                File.WriteAllText(@".\UserSettings\logSettings.json", "{\"logSettings\":\"" + parameter.ToString() + "\"}");
            }
            else
            {
                // Write the type of logs type we want to use (JSON or XML)
                File.WriteAllText(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\UserSettings\logSettings.json", "{\"logSettings\":\"" + parameter.ToString() + "\"}");
            }
        }
    }
}
