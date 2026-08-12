using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gym.Core.Application.DTOs;
using Gym.Core.Application.Contracts;
using System;
using System.Threading.Tasks;

namespace Gym.UI.ViewModels
{
    public partial class EditMemberViewModel : ObservableObject
    {
        private readonly IMemberService _memberService;

        public int MemberId { get; set; }

        [ObservableProperty]
        private string _fullName = string.Empty;

        [ObservableProperty]
        private string _email = string.Empty;

        [ObservableProperty]
        private string _planType = string.Empty;

        [ObservableProperty]
        private decimal _planPrice;
        
        [ObservableProperty]
        private string _membershipStatus = string.Empty;

        [ObservableProperty]
        private string _errorMessage = string.Empty;

        [ObservableProperty]
        private bool _isBusy;

        public Action? CloseAction { get; set; }

        public EditMemberViewModel(IMemberService memberService)
        {
            _memberService = memberService;
        }

        public void LoadMember(MemberDTO member)
        {
            MemberId = member.MemberId;
            FullName = member.FullName;
            Email = member.Email ?? string.Empty;
            PlanType = member.PlanType;
            PlanPrice = member.PlanPrice;
            MembershipStatus = member.MembershipStatus;
        }

        [RelayCommand]
        private async Task SaveAsync()
        {
            if (string.IsNullOrWhiteSpace(FullName))
            {
                ErrorMessage = "El nombre completo es obligatorio.";
                return;
            }

            IsBusy = true;
            ErrorMessage = string.Empty;

            var existingResult = await _memberService.GetMemberByIdAsync(MemberId);
            if (!existingResult.Success)
            {
                ErrorMessage = "No se pudo cargar el socio para actualizar.";
                IsBusy = false;
                return;
            }

            var dto = existingResult.Data!;
            dto.FullName = FullName;
            dto.Email = string.IsNullOrWhiteSpace(Email) ? null : Email;
            dto.PlanType = PlanType;
            dto.PlanPrice = PlanPrice;
            dto.MembershipStatus = MembershipStatus;

            var result = await _memberService.UpdateMemberAsync(dto);

            IsBusy = false;

            if (result.Success)
            {
                CloseAction?.Invoke();
            }
            else
            {
                ErrorMessage = result.Message;
            }
        }
    }
}
