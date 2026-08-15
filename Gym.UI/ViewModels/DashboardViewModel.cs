using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gym.Core.Application.DTOs;
using Gym.Core.Application.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.UI.ViewModels
{
    public partial class DashboardViewModel : ObservableObject
    {
        private readonly IMemberService _memberService;

        [ObservableProperty]
        private string _currentDate = string.Empty;

        public ObservableCollection<MemberDTO> Members { get; } = new();

        [ObservableProperty]
        private int _activeMembersCount;

        public DashboardViewModel(IMemberService memberService)
        {
            _memberService = memberService;
            
            CurrentDate = DateTime.Now.ToString("dddd, dd 'de' MMMM yyyy", new System.Globalization.CultureInfo("es-ES"));
            if (!string.IsNullOrEmpty(CurrentDate))
            {
                CurrentDate = char.ToUpper(CurrentDate[0]) + CurrentDate.Substring(1);
            }
        }

        public async Task LoadDataAsync()
        {
            var result = await _memberService.GetAllMembersAsync();
            if (result.Success)
            {
                Members.Clear();
                foreach (var member in result.Data.OrderByDescending(m => m.MemberId).Take(5)) // Show only top 5 recent
                {
                    Members.Add(member);
                }
                
                ActiveMembersCount = result.Data.Count(m => m.MembershipStatus == "Activo");
            }
        }

        [RelayCommand]
        private async Task OpenAddMemberAsync()
        {
            var addWindow = App.AppHost!.Services.GetRequiredService<Views.AddMemberWindow>();
            addWindow.Owner = App.Current.MainWindow; 
            
            if (addWindow.ShowDialog() == true)
            {
                await LoadDataAsync();
            }
        }
    }
}
