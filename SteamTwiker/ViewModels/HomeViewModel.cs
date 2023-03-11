using SteamTwiker.Models;
using SteamTwiker.Utils;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

namespace SteamTwiker.ViewModels
{
    public class HomeViewModel: BaseViewModel
    {
        private DirectoryHelper _directoryHelper = new DirectoryHelper();
        private NavigationStore _navigationStore;
        private ObservableCollection<Account>? _accounts;
        private Account? _selectedAccount;
        public Account? SelectedAccount
        {
            get => _selectedAccount;
            set
            {
                _selectedAccount = value;
                OnPropertyChanged(nameof(SelectedAccount));
            }
        }
        public ObservableCollection<Account>? Accounts
        {
            get => _accounts;
            set
            {
                _accounts = value; 
                OnPropertyChanged(nameof(Accounts));
            }
        }
        public HomeViewModel(NavigationStore navigationStore): base(navigationStore)
        {
            _navigationStore = navigationStore;
            Accounts = new ObservableCollection<Account>(_directoryHelper.GetAllAccountsFromPath());
        }
        public RelayCommand RunSteam
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    if (SelectedAccount is not null)
                        _directoryHelper.RunBatFile(SelectedAccount.Name!);
                });
            }
        }
        public RelayCommand DeleteAccount
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    if (SelectedAccount is not null)
                        _directoryHelper.RemoveAccount(SelectedAccount);
                    Accounts = 
                    new ObservableCollection<Account>(_directoryHelper.GetAllAccountsFromPath());
                });
            }
        }
        public RelayCommand AppendAccount
        { 
            get
            {
                return new RelayCommand(obj =>
                {
                    _navigationStore.CurrentViewModel = new AddViewModel(_navigationStore);
                });
            }
        }
    }
}
