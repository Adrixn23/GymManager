using Gym.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Data.Interfaces
{
    public interface IMemberRepository
    {
        Task <IEnumerable<Member>> GetAllAsync();
        Task<Member?> GetByIdAsync(int id);

        Task AddAsync(Member member);

        Task UpdateAsync(Member member);




    }
}
