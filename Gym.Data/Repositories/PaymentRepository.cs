using Gym.Data.Context;
using Gym.Data.Interfaces;
using Gym.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Data.Repositories
{
   
    
    public class PaymentRepository : IPaymentRepository
    {
        private readonly GymDbContext _context;

        public PaymentRepository(GymDbContext context) {

            _context = context;
        
        }

        public  Task<Payment> AddPay(Payment payment)
        {
            var paymentNew =  _context.Payments.Add(payment);
            return Task.FromResult(payment);
        }

        public async Task<IEnumerable<Payment>> GetByMemberIdAsync(int memberId)
        {
            return await _context.Payments.AsNoTracking().Where(e => e.MemberId == memberId).ToListAsync();
           

        }

        public async Task<Payment?> GetByPayIdAsync(int paymentId)
        {
            return await _context.Payments.FirstOrDefaultAsync(e => e.PaymentId == paymentId);
        }

        public Task UpdateAsync(Payment payment)
        {
            _context.Payments.Update(payment);
            return Task.CompletedTask;
           
        }
    }
}
