using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.Core.Application.DTOs;
using Gym.Core.Domain.Common.Results;

namespace Gym.Core.Application.Contracts
{
    public interface IMemberService
    {
        Task<OperationResult<MemberDTO>> RegisterMemberAsync(MemberDTO member);
        Task<OperationResult<IEnumerable<MemberDTO>>> GetAllMembersAsync();
        Task<OperationResult<MemberDTO>> GetMemberByIdAsync(int memberId);
        Task<OperationResult<MemberDTO>> UpdateMemberAsync(MemberDTO member);
        Task<OperationResult<bool>> RenewMemberAsync(int memberId, int monthsToAdd);
        Task<OperationResult<bool>> DeactivateMemberAsync(int memberId);
    }
}
