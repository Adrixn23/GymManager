using System.Windows;
using System.Windows.Controls;

namespace Gym.UI.Views
{
    public partial class MembersView : UserControl
    {
        public MembersView()
        {
            InitializeComponent();
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.ContextMenu != null)
            {
                button.ContextMenu.PlacementTarget = button;
                button.ContextMenu.IsOpen = true;
            }
        }
    }
}