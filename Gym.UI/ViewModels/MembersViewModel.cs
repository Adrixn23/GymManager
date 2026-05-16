using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gym.Business.DTOs;
using Gym.Business.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.UI.ViewModels
{
    public partial class MembersViewModel : ObservableObject
    {
        private readonly IMemberService _memberService;

        public ObservableCollection<MemberDTO> Members { get; } = new();

        [ObservableProperty]
        private string _searchText = string.Empty;

        public MembersViewModel(IMemberService memberService)
        {
            _memberService = memberService;
        }

        public async Task LoadDataAsync()
        {
            var result = await _memberService.GetAllMembersAsync();
            if (result.Success)
            {
                Members.Clear();
                var filtered = string.IsNullOrWhiteSpace(SearchText) 
                    ? result.Data 
                    : result.Data.Where(m => m.FullName.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase) || 
                                             m.MembershipStatus.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase));
                
                foreach (var member in filtered)
                {
                    Members.Add(member);
                }
            }
        }

        [RelayCommand]
        private async Task SearchAsync()
        {
            await LoadDataAsync();
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

        [RelayCommand]
        private async Task EditMemberAsync(MemberDTO member)
        {
            if (member == null) return;

            var editWindow = App.AppHost!.Services.GetRequiredService<Views.EditMemberWindow>();
            editWindow.Owner = App.Current.MainWindow;
            
            var vm = (EditMemberViewModel)editWindow.DataContext;
            vm.LoadMember(member);

            if (editWindow.ShowDialog() == true)
            {
                await LoadDataAsync();
            }
        }

        [RelayCommand]
        private async Task RenewMemberAsync(MemberDTO member)
        {
            if (member == null) return;

            var result = await _memberService.RenewMemberAsync(member.MemberId, 1);
            if (result.Success)
            {
                await LoadDataAsync();
            }
            // Podrías agregar un MessageBox para mostrar el mensaje de error si falla
        }

        [RelayCommand]
        private async Task DeactivateMemberAsync(MemberDTO member)
        {
            if (member == null) return;

            var result = await _memberService.DeactivateMemberAsync(member.MemberId);
            if (result.Success)
            {
                await LoadDataAsync();
            }
        }
    }
}