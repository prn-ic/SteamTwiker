using SteamTwiker.Utils;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SteamTwiker.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private NavigationStore _navigationStore;
        public BaseViewModel? CurrentViewModel => _navigationStore.CurrentViewModel; 
        public BaseViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            
        }
        public void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
