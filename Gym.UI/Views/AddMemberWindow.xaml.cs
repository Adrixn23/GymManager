using System.Windows;
using Gym.UI.ViewModels;

namespace Gym.UI.Views
{
    public partial class AddMemberWindow : Window
    {
        public AddMemberWindow(AddMemberViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            
            // Le decimos al ViewModel cómo cerrar esta ventana cuando termine
            viewModel.CloseAction = () => {
                this.DialogResult = true; // Indica que se guardó correctamente
                this.Close();
            };
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}