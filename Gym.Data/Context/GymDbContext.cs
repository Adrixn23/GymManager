using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Gym.Data.Context
{
    public class GymDbContext : DbContext
    {

        GymDbContext(DbContextOptions<GymDbContext> options) : base(options) {


        }

       



    }
}
