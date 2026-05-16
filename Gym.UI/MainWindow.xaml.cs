using System.Windows;
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
    }
}