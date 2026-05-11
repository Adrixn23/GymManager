using System;

namespace Gym.Domain.Entities
{
    public class Member
    {
        public int MemberId { get; set; }
        
        // Lo único obligatorio del cliente
        public string FullName { get; set; } 
        
        // Opcional para mandarle la alerta de vencimiento que pediste
        public string? Email { get; set; } 

        // Fechas de control
        public DateTime RegistrationDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        // El semáforo (Activo, Vencido, Suspendido)
        public string MembershipStatus { get; set; } 

        // Cuánto pagó y qué tipo de plan es (Ej: "Mensual", 900)
        public string PlanType { get; set; } 
        public decimal PlanPrice { get; set; }

        // Auditoría: Quién lo registró y cuándo
        public int CreatedById { get; set; }
        public DateTime CreatedAt { get; set; }

        // Control de concurrencia para que no choquen dos recepcionistas
        public byte[] RowVersion { get; set; } 
    }
}
