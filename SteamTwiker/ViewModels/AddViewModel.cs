using SteamTwiker.Models;
using SteamTwiker.Utils;
using System.Windows;
using System.Xml.Linq;

namespace SteamTwiker.ViewModels
{
    class AddViewModel: BaseViewModel
    {
        private NavigationStore _navigationStore;
        private DirectoryHelper _directoryHelper = new DirectoryHelper();
        public AddViewModel(NavigationStore navigationStore): base(navigationStore)
        {
            _navigationStore = navigationStore;
        }
        private Account? _newAccount;
        private string? _name;
        public string? Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private string? _password;
        public string? Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public RelayCommand AppendAccount
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    _directoryHelper.AddAccount(new Account() { Name = Name, Password = Password});
                    _navigationStore.CurrentViewModel = new HomeViewModel(_navigationStore);
                });
            }
        }
        public RelayCommand CancelCommand
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    _navigationStore.CurrentViewModel = new HomeViewModel(_navigationStore);
                });
            }
        }
    }
}
