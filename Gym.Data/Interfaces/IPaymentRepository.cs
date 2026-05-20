using Gym.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Gym.Domain.Entities.Payment;

namespace Gym.Data.Interfaces
{
    public interface IPaymentRepository
    {
        Task<Payment> AddPay(Payment payment);

        Task<IEnumerable<Payment>> GetByMemberIdAsync(int memberId);

        Task<Payment?> GetByPayIdAsync(int paymentId);

        Task UpdateAsync(Payment payment);



    }
}
