using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Linq;

namespace Gym.UI.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _userFullName = string.Empty;

        [ObservableProperty]
        private string _userRole = string.Empty;

        [ObservableProperty]
        private string _initials = string.Empty;

        [ObservableProperty]
        private string _currentDate = string.Empty;

        public MainViewModel()
        {
            // Fecha formateada, ej: Domingo, 10 de Mayo 2026
            CurrentDate = DateTime.Now.ToString("dddd, dd 'de' MMMM yyyy", new System.Globalization.CultureInfo("es-ES"));
            // Capitalizar primera letra
            if (!string.IsNullOrEmpty(CurrentDate))
            {
                CurrentDate = char.ToUpper(CurrentDate[0]) + CurrentDate.Substring(1);
            }
        }

        public void Initialize(string fullName, string role)
        {
            UserFullName = fullName;
            UserRole = role;
            
            // Calcular iniciales (ej: "Adrian Brito" -> "AB")
            var names = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (names.Length >= 2)
            {
                Initials = $"{names[0][0]}{names[1][0]}".ToUpper();
            }
            else if (names.Length == 1)
            {
                Initials = $"{names[0][0]}".ToUpper();
            }
            else
            {
                Initials = "AD";
            }
        }
    }
}