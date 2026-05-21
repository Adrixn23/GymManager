using Gym.Business.DTOs;
using Gym.Business.Interfaces;
using Gym.Business.LogicResults;
using Gym.Data.Interfaces;
using Gym.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Gym.Business.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<PaymentDTO>> RegisterPayAsync(PaymentDTO payment)
        {
            var member = await _unitOfWork.Members.GetByIdAsync(payment.MemberId);

            if (member == null)
            {
                return OperationResult<PaymentDTO>.Fail("El socio especificado no existe.");
            }

            DateTime newExpiration;
            if (member.ExpirationDate > DateTime.Today)
            {
                newExpiration = member.ExpirationDate.AddDays(30);
            }
            else
            {
                newExpiration = DateTime.Today.AddDays(30);
            }

            member.ExpirationDate = newExpiration;
            member.MembershipStatus = "Activo";

            var paymentEntity = new Payment
            {
                MemberId = payment.MemberId,
                Amount = payment.Amount,
                Method = (Payment.PaymentMethod)Enum.Parse(typeof(Payment.PaymentMethod), payment.Method),
                Concept = payment.Concept,
                PaymentDate = DateTime.Now,
                NewExpiryDate = newExpiration,
                IsCanceled = false,
                ReceiptNumber = $"REC-{payment.MemberId}-{DateTime.Now.Ticks % 100000}"
            };

            await _unitOfWork.Payments.AddPay(paymentEntity);
            await _unitOfWork.Members.UpdateAsync(member);
            
            await _unitOfWork.SaveChangesAsync();

            payment.PaymentId = paymentEntity.PaymentId;
            payment.ReceiptNumber = paymentEntity.ReceiptNumber;
            payment.NewExpiryDate = paymentEntity.NewExpiryDate;
            payment.PaymentDate = paymentEntity.PaymentDate;
            payment.MemberName = member.FullName;

            return OperationResult<PaymentDTO>.Ok(payment, "Pago registrado exitosamente");
        }

        public Task<OperationResult<PaymentDTO>> CancelPayAsync(int paymentId, string reason, int userId) => throw new NotImplementedException();
        public Task<OperationResult<decimal>> GetDailyTotalAsync() => throw new NotImplementedException();
        public Task<OperationResult<IEnumerable<PaymentDTO>>> GetHistorialByMemberAsync(int memberId) => throw new NotImplementedException();
        public Task<OperationResult<bool>> ValidateCashClosingAsync(decimal declaredAmount) => throw new NotImplementedException();
    }
}