
using Gym.Business.DTOs;
using Gym.Business.LogicResults;
using Gym.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Business.Interfaces
{
    public interface IMemberService
    {
        Task<OperationResult<MemberDTO>> RegisterMemberAsync(MemberDTO member);

        Task<OperationResult<IEnumerable<MemberDTO>>> GetAllMembersAsync();

        Task<OperationResult<MemberDTO>> GetMemberByIdAsync(int memberId);

        Task<OperationResult<MemberDTO>> UpdateMemberAsync(MemberDTO member);
    }
}

