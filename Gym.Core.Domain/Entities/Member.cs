using System;

namespace Gym.Domain.Entities
{
    public class Member
    {
        public int MemberId { get; set; }
        public string FullName { get; set; } 
        public string? Email { get; set; } 
        public DateTime RegistrationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string MembershipStatus { get; set; } 
        public string PlanType { get; set; } 
        public decimal PlanPrice { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedAt { get; set; }
        public byte[] RowVersion { get; set; } 
    }
}
