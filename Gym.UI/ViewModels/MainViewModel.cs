using CommunityToolkit.Mvvm.ComponentModel;
using Gym.Business.DTOs;
using Gym.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.UI.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IMemberService _memberService;

        [ObservableProperty]
        private string _userFullName = string.Empty;

        [ObservableProperty]
        private string _userRole = string.Empty;

        [ObservableProperty]
        private string _initials = string.Empty;

        [ObservableProperty]
        private string _currentDate = string.Empty;

        // Lista real de socios para la UI
        public ObservableCollection<MemberDTO> Members { get; } = new();

        [ObservableProperty]
        private int _activeMembersCount;

        public MainViewModel(IMemberService memberService)
        {
            _memberService = memberService;
            
            // Fecha formateada
            CurrentDate = DateTime.Now.ToString("dddd, dd 'de' MMMM yyyy", new System.Globalization.CultureInfo("es-ES"));
            if (!string.IsNullOrEmpty(CurrentDate))
            {
                CurrentDate = char.ToUpper(CurrentDate[0]) + CurrentDate.Substring(1);
            }
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

            // Cargamos los datos reales al iniciar
            _ = LoadDashboardDataAsync();
        }

        public async Task LoadDashboardDataAsync()
        {
            var result = await _memberService.GetAllMembersAsync();
            if (result.Success)
            {
                Members.Clear();
                foreach (var member in result.Data)
                {
                    Members.Add(member);
                }
                
                ActiveMembersCount = Members.Count(m => m.MembershipStatus == "Activo");
            }
        }
    }
}