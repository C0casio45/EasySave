using EasySave.Commands;
using EasySave.Models;
using EasySave.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;

namespace EasySave.ViewModels
{
    //
    // ViewModel used for settings config
    // Every private attribute is Binded with the View SettingsView
    //
    public class SettingsViewModel : ViewModelBase
    {
        private readonly ObservableCollection<ExtensionViewModel> _extensions;
        public IEnumerable<ExtensionViewModel> Extensions => _extensions;

        private string _extensionName;
        public string ExtensionName
        {
            get => _extensionName;
            set
            {
                _extensionName = value;
                OnPropertyChanged(nameof(ExtensionName));
            }
        }
        private string _businessSoftware;
        public string BusinessSoftware
        {
            get => _businessSoftware;
            set
            {
                _businessSoftware = value;
                OnPropertyChanged(nameof(BusinessSoftware));
            }
        }
        private string _selectedItem;
        public static ObservableCollection<string> Languages { get; } = new ObservableCollection<string> { "English (en-US)", "Francais (fr-FR)" };
        public string SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;

                ChangeLanguageCommand = new ChangeLanguageCommand(this, _selectedItem);
                ChangeLanguageCommand.Execute(this);
            }
        }

        private bool _logTypeIsJson;
        public bool LogTypeIsJson
        {
            get => _logTypeIsJson;
            set
            {
                _logTypeIsJson = value;
            }
        }
        private bool _logTypeIsXml;
        public bool LogTypeIsXml
        {
            get => _logTypeIsXml;
            set
            {
                _logTypeIsXml = value;
            }
        }

        public ICommand PrimaryColorCommand { get; }
        public ICommand SecondaryColorCommand { get; }
        public ICommand FontColorCommand { get; }

        public ICommand AddExtensionCommand { get; }
        public ICommand ChangeLanguageCommand;
        public ICommand AddBusinessSoftwareCommand { get; }
        public ICommand SetLoggingTypeCommand { get; }


        public SettingsViewModel(NavigationService navigationService)
        {
            _extensions = new ObservableCollection<ExtensionViewModel>();
            PrimaryColorCommand = new SelectColorCommand("Primary");
            SecondaryColorCommand = new SelectColorCommand("Secondary");
            FontColorCommand = new SelectColorCommand("Font");
            AddExtensionCommand = new AddExtensionCommand(this);
            ChangeLanguageCommand = new ChangeLanguageCommand(this, _selectedItem);
            AddBusinessSoftwareCommand = new AddBusinessSoftwareCommand(this);
            SetLoggingTypeCommand = new SetLoggingTypeCommand();
            UpdateExtensions();
            try
            {
                // Using the installer this path will be used 
                _businessSoftware = JsonConvert.DeserializeObject<CBusinessSoftware>(File.ReadAllText("./UserSettings/businessSoftware.json")).SName;
                _logTypeIsJson = JObject.Parse(File.ReadAllText("./UserSettings/logSettings.json"))["logSettings"].ToString()=="JSON"?true:false;
            }
            catch (Exception)
            {
                // Else during developpement this path will be used
                _businessSoftware = JsonConvert.DeserializeObject<CBusinessSoftware>(File.ReadAllText(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\UserSettings\businessSoftware.json")).SName;
                _logTypeIsJson = JObject.Parse(File.ReadAllText(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\UserSettings\logSettings.json"))["logSettings"].ToString() == "JSON" ? true : false;
            }
            _logTypeIsXml = !_logTypeIsJson;

        }

        public void UpdateExtensions()
        {
            //Clear the list of extensions and read the json file to update
            _extensions.Clear();
            List<CExtension> extensionList;
            try
            {
                extensionList = JsonConvert.DeserializeObject<List<CExtension>>(File.ReadAllText("./UserSettings/extToEncrypt.json"));
            }
            catch (Exception)
            {
                extensionList = JsonConvert.DeserializeObject<List<CExtension>>(File.ReadAllText(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\UserSettings\extToEncrypt.json"));
            }

            foreach (CExtension extension in extensionList)
            {
                _extensions.Add(new ExtensionViewModel(extension, this));
            }
        }

    }
}
