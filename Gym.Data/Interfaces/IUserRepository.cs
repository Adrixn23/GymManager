using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gym.Domain.Entities;


namespace Gym.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);

        Task AddAsync(User user);

        Task<bool> ExistsAsync(string username);

       Task UpdateLastAccessAsync(int userId);
    }
}
