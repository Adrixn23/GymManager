using Gym.Data.Context;
using Gym.Data.Interfaces;
using Gym.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Data.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly GymDbContext _context; 

        public UserRepository(GymDbContext context) {

            _context = context;
        }




        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

      

      public async Task  <User?> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);
        }

       public async Task<bool> ExistsAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }

        public async Task UpdateLastAccessAsync(int userId)
        {

            var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user != null)
            {
                user.LastAccess = DateTime.Now;
            }
        }



}
}
