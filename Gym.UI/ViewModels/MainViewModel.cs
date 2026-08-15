using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gym.Core.Application.DTOs;
using Gym.Core.Application.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

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
        private ObservableObject? _currentViewModel;

        // Propiedades para controlar el estilo activo de los botones del menú
        [ObservableProperty]
        private bool _isDashboardActive = true;

        [ObservableProperty]
        private bool _isMembersActive = false;

        public MainViewModel()
        {
            // Inicializar con el Dashboard
            NavigateToDashboard();
        }

        public void Initialize(string fullName, string role)
        {
            UserFullName = fullName;
            UserRole = role;
            
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

        [RelayCommand]
        private void NavigateToDashboard()
        {
            IsDashboardActive = true;
            IsMembersActive = false;
            
            var vm = App.AppHost!.Services.GetRequiredService<DashboardViewModel>();
            _ = vm.LoadDataAsync();
            CurrentViewModel = vm;
        }

        [RelayCommand]
        private void NavigateToMembers()
        {
            IsDashboardActive = false;
            IsMembersActive = true;
            
            var vm = App.AppHost!.Services.GetRequiredService<MembersViewModel>();
            _ = vm.LoadDataAsync();
            CurrentViewModel = vm;
        }
    }
}
