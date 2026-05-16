using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gym.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gym.Data.Context
{
    public class GymDbContext : DbContext
    {
       public GymDbContext(DbContextOptions<GymDbContext> options) : base(options)
        {
        }

       public DbSet<User> Users { get; set; }
        public DbSet<Member> Members { get; set; }

        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("usuarios");
                entity.HasKey(e => e.UserId);
                
                entity.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(50);

                entity.HasIndex(e => e.Username)
                .IsUnique();

                entity.Property(e => e.Role).IsRequired().HasMaxLength(20);
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.Property(e => e.Salt).IsRequired();
            });
            modelBuilder.Entity<Member>(entity =>
                {
                    entity.ToTable("socios");
                    entity.HasKey(e => e.MemberId);
                    entity.Property(e => e.FullName).IsRequired().HasMaxLength(100);
                    entity.Property(e => e.RowVersion).IsRowVersion();
                    entity.Property(e => e.PlanPrice).HasColumnType("decimal(10, 2)");
                    entity.Property(e => e.PlanType).HasMaxLength(50);
                    entity.Property(e => e.MembershipStatus).IsRequired().HasMaxLength(20) ;
                });

                modelBuilder.Entity<Payment>(entity => {


                    entity.ToTable("pagos");
                    entity.HasKey(e => e.PaymentId);
                    entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
                    entity.HasIndex(e => e.ReceiptNumber).IsUnique();
                    entity.Property(e => e.Concept).HasMaxLength(100).IsRequired();
                    entity.Property(e => e.VoidReason).HasMaxLength(200);
                    entity.Property(e => e.Method)
            .HasConversion<string>()
            .HasMaxLength(20)
              .IsRequired();
                });

             

         
        }
    }
}
