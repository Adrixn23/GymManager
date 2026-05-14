using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Gym.UI.ViewModels;

namespace Gym.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            // Esto conecta tu lógica (ViewModel) con la interfaz
            DataContext = viewModel;
        }

        private void OpenAddMember_Click(object sender, RoutedEventArgs e)
        {
            // Pedimos la ventana al contenedor de inyección de dependencias
            var addWindow = App.AppHost!.Services.GetRequiredService<Views.AddMemberWindow>();
            addWindow.Owner = this; // Para que aparezca centrada sobre el Dashboard
            
            // ShowDialog detiene la ejecución aquí hasta que se cierre la ventana
            if (addWindow.ShowDialog() == true)
            {
                // Si guardó exitosamente, le decimos al ViewModel que recargue la lista
                if (DataContext is MainViewModel vm)
                {
                    _ = vm.LoadDashboardDataAsync();
                }
            }
        }
    }
}