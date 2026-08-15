using System;

namespace Gym.Core.Application.DTOs
{
    public class PaymentDTO
    {
        public int PaymentId { get; set; }
        public int MemberId { get; set; }
        public string MemberName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Method { get; set; } = string.Empty;
        public string Concept { get; set; } = string.Empty;
        public DateTime PaymentDate { get; set; }
        public DateTime NewExpiryDate { get; set; }
        public string ReceiptNumber { get; set; } = string.Empty;
        public bool IsCanceled { get; set; }
        public string VoidReason { get; set; } = string.Empty;
    }
}
