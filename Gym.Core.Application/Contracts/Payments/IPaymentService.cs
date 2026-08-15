using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.Core.Application.DTOs;
using Gym.Core.Domain.Common.Results;

namespace Gym.Core.Application.Contracts
{
    public interface IPaymentService
    {
        Task<OperationResult<PaymentDTO>> RegisterPayAsync(PaymentDTO payment);
        Task<OperationResult<PaymentDTO>> CancelPayAsync(int paymentId, string reason, int userId);
        Task<OperationResult<decimal>> GetDailyTotalAsync();
        Task<OperationResult<IEnumerable<PaymentDTO>>> GetHistorialByMemberAsync(int memberId);
        Task<OperationResult<bool>> ValidateCashClosingAsync(decimal declaredAmount);
    }
}
