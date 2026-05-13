using Gym.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Business.DTOs
{
    public class MemberDTO
    {
       public int MemberId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime ExpirationDate { get; set; }

        public string MembershipStatus { get; set; } = string.Empty;
        public string PlanType { get; set; } = string.Empty;
        public decimal PlanPrice { get; set; } 

    }
}
