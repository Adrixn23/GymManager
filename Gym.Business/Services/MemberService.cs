using Gym.Business.DTOs;
using Gym.Business.Interfaces;
using Gym.Business.LogicResults;
using Gym.Data.Interfaces;
using Gym.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.Business.Services
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<IEnumerable<MemberDTO>>> GetAllMembersAsync()
        {
            var members = await _unitOfWork.Members.GetAllAsync();

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

        public async Task<OperationResult<MemberDTO>> GetMemberByIdAsync(int memberId)
        {
            var memberEntity = await _unitOfWork.Members.GetByIdAsync(memberId);

            if (memberEntity == null)
            {
                return OperationResult<MemberDTO>.Fail("Socio no encontrado");
            }

            var dto = new MemberDTO
            {
                MemberId = memberEntity.MemberId,
                FullName = memberEntity.FullName,
                Email = memberEntity.Email,
                ExpirationDate = memberEntity.ExpirationDate,
                MembershipStatus = memberEntity.MembershipStatus,
                PlanType = memberEntity.PlanType,
                PlanPrice = memberEntity.PlanPrice
            };

            return OperationResult<MemberDTO>.Ok(dto, "Socio encontrado exitosamente");
        }

        public async Task<OperationResult<MemberDTO>> RegisterMemberAsync(MemberDTO member)
        {
            var newMember = new Member
            {
                FullName = member.FullName,
                Email = member.Email,
                RegistrationDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddDays(30), // Por defecto 30 días
                MembershipStatus = "Activo",
                PlanType = member.PlanType,
                PlanPrice = member.PlanPrice,
                CreatedAt = DateTime.Now,
                CreatedById = 1 // TODO: Obtener el ID del usuario logueado después
            };

            await _unitOfWork.Members.AddAsync(newMember);
            await _unitOfWork.SaveChangesAsync();

            member.MemberId = newMember.MemberId;
            member.ExpirationDate = newMember.ExpirationDate;
            member.MembershipStatus = newMember.MembershipStatus;

            return OperationResult<MemberDTO>.Ok(member, "Socio creado correctamente");
        }

        public async Task<OperationResult<MemberDTO>> UpdateMemberAsync(MemberDTO member)
        {
            var existing = await _unitOfWork.Members.GetByIdAsync(member.MemberId);

            if (existing == null)
                return OperationResult<MemberDTO>.Fail("Socio no encontrado");

            existing.FullName = member.FullName;
            existing.Email = member.Email;
            existing.ExpirationDate = member.ExpirationDate;
            existing.MembershipStatus = member.MembershipStatus;
            existing.PlanType = member.PlanType;
            existing.PlanPrice = member.PlanPrice;

            await _unitOfWork.Members.UpdateAsync(existing);
            await _unitOfWork.SaveChangesAsync();

            return OperationResult<MemberDTO>.Ok(member, "Socio actualizado correctamente");
        }
    }
}
