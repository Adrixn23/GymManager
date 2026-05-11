using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gym.Business.Interfaces;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Gym.UI.ViewModels
{
    // Con CommunityToolkit, la clase DEBE ser partial y heredar de ObservableObject
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IAuthService _authService;

        // [ObservableProperty] genera automáticamente la propiedad pública 'Username'
        // con toda la lógica de INotifyPropertyChanged por detrás.
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

        // [RelayCommand] genera automáticamente el ICommand llamado 'LoginCommand'
        [RelayCommand]
        private async Task LoginAsync(object? parameter)
        {
            // Validamos que el parámetro sea nuestro PasswordBox visual
            if (parameter is not PasswordBox passwordBox) return;

            string password = passwordBox.Password;

            // Validaciones básicas de UI
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(password))
            {
                ErrorMessage = "Por favor ingrese usuario y contraseña.";
                return;
            }

            ErrorMessage = string.Empty;
            IsBusy = true; // Podrías usar esto para deshabilitar el botón mientras carga

            // Llamamos a tu servicio de negocio que programaste!
            var result = await _authService.LoginAsync(Username, password);

            IsBusy = false;

            if (result.Success)
            {
                // Limpiamos la clave de la memoria visual
                passwordBox.Clear();
                
                // Obtenemos el contenedor de DI
                var services = Gym.UI.App.AppHost!.Services;

                // Solicitamos la ventana principal
                var mainWindow = Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<Gym.UI.MainWindow>(services);
                
                // Configuramos los datos del usuario en el MainViewModel
                if (mainWindow.DataContext is MainViewModel mainViewModel)
                {
                    mainViewModel.Initialize(result.Data!.FullName, result.Data.Role);
                }

                // Hacemos el cambio de pantallas
                var loginWindow = Window.GetWindow(passwordBox);
                Application.Current.MainWindow = mainWindow;
                mainWindow.Show();
                loginWindow?.Close();
            }
            else
            {
                // Mostramos el error que nos devolvió el Result Pattern
                ErrorMessage = result.Message;
            }
        }
    }
}
