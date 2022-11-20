using System.ComponentModel;

namespace EasySave.ViewModels
{
    //
    // Class that all our ViewModel are gonna inherit
    // Implementing the interface INotifyPropertyChanged
    // Needed to tell the UI which binding to update
    // 
    public class ViewModelBase : INotifyPropertyChanged
    {
        //this contains the event for viewmodelChanges and permit using ViewModelBase type in methods which work on differents ViewModels
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            // Observer Design Pattern
            // Anything subscribed 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
