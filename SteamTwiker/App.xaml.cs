using SteamTwiker.Utils;
using SteamTwiker.ViewModels;
using System.Windows;

namespace SteamTwiker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            NavigationStore navigationStore = new NavigationStore();
            navigationStore.CurrentViewModel = new HomeViewModel(navigationStore);
            MainWindow mainWindow = new MainWindow()
            {
                DataContext = new BaseViewModel(navigationStore)
            };

            mainWindow.Show();

            base.OnStartup(e);
        }
    }
}
