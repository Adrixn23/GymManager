using Gym.Business.DTOs;
using Gym.Business.LogicResults;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gym.Business.Interfaces
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
