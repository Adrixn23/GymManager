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
    public class MemberRepository : IMemberRepository

    {
        private readonly GymDbContext _context;

        public MemberRepository(GymDbContext context) {


            _context = context;


        }


        public async Task AddAsync(Member member)
        {
            await _context.Members.AddAsync(member);
        }

        public async Task<IEnumerable<Member>> GetAllAsync()
        {
            return await _context.Members.ToListAsync();
        }

        public async Task<Member?> GetByIdAsync(int id)
        {
            return await _context.Members.FindAsync(id);
        }

        public Task UpdateAsync(Member member)
        {
            _context.Members.Update(member);
            return Task.CompletedTask;
        }
    }
}
