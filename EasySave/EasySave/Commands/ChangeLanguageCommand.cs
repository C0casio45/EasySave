using EasySave.Models;
using EasySave.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace EasySave.Commands
{
    public class ChangeLanguageCommand : CommandBase
    {
        private readonly string _selectedLang;
        private readonly SettingsViewModel _settingsViewModel;

        public ChangeLanguageCommand(SettingsViewModel settingsViewModel, string selectedLang)
        {
            _settingsViewModel = settingsViewModel;
            _selectedLang = selectedLang;
        }

        public override void Execute(object parameter)
        {
            List<ResourceDictionary> dictionaryList = Application.Current.Resources.MergedDictionaries.ToList(); //get all ressources dictionnary

            string defaultLang = "English (en-US)";
            string requestedLang = $"Lang/{_selectedLang}.xaml"; //Get the lang file
            ResourceDictionary resourceDictionary = dictionaryList.
                FirstOrDefault(d => d.Source.OriginalString == requestedLang); //find if the ressource dictionnary is in app.xaml

            if (resourceDictionary == null)//not exist -> english lang
            {
                requestedLang = defaultLang;
                resourceDictionary = dictionaryList.
                    FirstOrDefault(d => d.Source.OriginalString == requestedLang);
            }

            if (resourceDictionary != null)//exist -> change the dictionnary
            {
                Application.Current.Resources.MergedDictionaries.Remove(resourceDictionary);
                Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
            }


        }
    }
}
