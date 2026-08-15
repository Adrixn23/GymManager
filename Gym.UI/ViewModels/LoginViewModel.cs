using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gym.Core.Application.Contracts;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Gym.UI.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IAuthService _authService;

        [ObservableProperty]
        private string _username = string.Empty;

        [ObservableProperty]
        private string _errorMessage = string.Empty;

        [ObservableProperty]
        private bool _isBusy;

        public LoginViewModel(IAuthService authService)
        {
            _authService = authService;
        }

        [RelayCommand]
        private async Task LoginAsync(object? parameter)
        {
            if (parameter is not PasswordBox passwordBox) return;

            string password = passwordBox.Password;

            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(password))
            {
                ErrorMessage = "Por favor ingrese usuario y contraseña.";
                return;
            }

            ErrorMessage = string.Empty;
            IsBusy = true;

            var result = await _authService.LoginAsync(Username, password);

            IsBusy = false;

            if (result.Success)
            {
                passwordBox.Clear();
                
                var services = Gym.UI.App.AppHost!.Services;

                var mainWindow = Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<Gym.UI.MainWindow>(services);
                
                if (mainWindow.DataContext is MainViewModel mainViewModel)
                {
                    mainViewModel.Initialize(result.Data!.FullName, result.Data.Role);
                }

                var loginWindow = Window.GetWindow(passwordBox);
                Application.Current.MainWindow = mainWindow;
                mainWindow.Show();
                loginWindow?.Close();
            }
            else
            {
                ErrorMessage = result.Message;
            }
        }
    }
}
