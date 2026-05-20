using Gym.Data.Context;
using Gym.Data.Interfaces;


namespace Gym.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GymDbContext _context;
        public IUserRepository Users { get; }

        public IMemberRepository Members { get; }


        public IPaymentRepository Payments { get; }
        

        public UnitOfWork(GymDbContext context)
        {


            _context = context;

            Users  = new UserRepository(_context);
            Members = new MemberRepository(_context);
            Payments = new PaymentRepository(_context);
        }
         
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context. Dispose();
        }


    }
}
