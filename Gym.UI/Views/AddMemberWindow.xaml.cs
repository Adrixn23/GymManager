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
            
            viewModel.CloseAction = () => {
                this.DialogResult = true;
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