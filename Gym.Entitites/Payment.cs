using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Domain.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int MemberId { get; set; }

        public decimal Amount { get; set; }

        public enum PaymentMethod { 
        
        Cash,
        Card,
        Transfer
        };
        public PaymentMethod Method { get; set; }
        public string Concept { get; set; } = string.Empty;

       public DateTime PaymentDate { get; set; }
        public DateTime NewExpiryDate { get; set; }

        public int CreatedBy { get; set; }

        public string ReceiptNumber { get; set; } = string.Empty;

        public bool IsCanceled { get; set; }

        public int? VoidedBy { get; set; }

        public DateTime? VoidedDate { get; set; }

        public string VoidReason { get; set; } = string.Empty;







    }
}
