using SteamTwiker.ViewModels;
using System;

namespace SteamTwiker.Utils
{
    public class NavigationStore
    {
        public Action? CurrentViewModelChanged;
        private BaseViewModel? _currentViewModel;
        public BaseViewModel? CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }
        public void OnCurrentViewModelChanged()
        { 
            CurrentViewModelChanged?.Invoke();
        }
    }
}
