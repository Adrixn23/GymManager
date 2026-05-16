using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gym.Business.DTOs;
using Gym.Business.Interfaces;
using System;
using System.Threading.Tasks;

namespace Gym.UI.ViewModels
{
    public partial class AddMemberViewModel : ObservableObject
    {
        private readonly IMemberService _memberService;

        [ObservableProperty]
        private string _fullName = string.Empty;

        [ObservableProperty]
        private string _email = string.Empty;

        [ObservableProperty]
        private string _planType = "Mensual";

        [ObservableProperty]
        private decimal _planPrice = 800m;

        [ObservableProperty]
        private string _errorMessage = string.Empty;

        [ObservableProperty]
        private bool _isBusy;

        public Action? CloseAction { get; set; }

        public AddMemberViewModel(IMemberService memberService)
        {
            _memberService = memberService;
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

            var dto = new MemberDTO
            {
                FullName = FullName,
                Email = string.IsNullOrWhiteSpace(Email) ? null : Email,
                PlanType = PlanType,
                PlanPrice = PlanPrice
            };

            var result = await _memberService.RegisterMemberAsync(dto);

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
