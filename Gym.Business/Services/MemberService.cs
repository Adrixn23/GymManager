using Gym.Business.DTOs;
using Gym.Business.Interfaces;
using Gym.Business.LogicResults;
using Gym.Data.Interfaces;
using Gym.Data.Repositories;
using Gym.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Gym.Business.Services
{
    public class MemberService : IMemberService
    {

        private readonly IUnitOfWork _unitOfWork;

        public MemberService(IUnitOfWork unitOfWork) {

            _unitOfWork = unitOfWork;
        }



        public async Task<OperationResult<IEnumerable<MemberDTO>>> GetAllMembersAsync()
        {

            var members = await _unitOfWork.Members.GetAllAsync(); // busca los socios en la base de datos. 


            var membersDTO = members.Select(m => new MemberDTO
            {


                MemberId = m.MemberId,
                FullName = m.FullName,
                Email = m.Email,
                ExpirationDate = m.ExpirationDate,
                MembershipStatus = m.MembershipStatus,
                PlanType = m.PlanType,
                PlanPrice = m.PlanPrice

            });

            return OperationResult<IEnumerable<MemberDTO>>.Ok(membersDTO, "Socios obtenidos");

        }

        public Task<OperationResult<MemberDTO>> GetMemberByIdAsync(int memberId)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<MemberDTO>> RegisterMemberAsync(MemberDTO member)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<MemberDTO>> UpdateMemberAsync(MemberDTO member)
        {
            throw new NotImplementedException();
        }
    }
}
