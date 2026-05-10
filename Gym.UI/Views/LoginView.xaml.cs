using System.Windows;
using System.Windows.Controls;
using Gym.UI.ViewModels;

namespace Gym.UI.Views
{
    public partial class LoginView : Window
    {
        public LoginView(LoginViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }


        private void UsernameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(UsernameTextBox.Text))
            {
                UserPlaceholder.Visibility = Visibility.Visible;
            }
            else
            {
                UserPlaceholder.Visibility = Visibility.Hidden;
            }
        }

        private void PasswordInput_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(PasswordInput.Password))
            {
                PassPlaceholder.Visibility = Visibility.Visible;
            }
            else
            {
                PassPlaceholder.Visibility = Visibility.Hidden;
            }
        }
    }
}