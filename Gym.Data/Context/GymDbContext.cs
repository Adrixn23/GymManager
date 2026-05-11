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
        //Heredamos de DbContext para obtener todo el poder de Entity Framework


       // Este constructor es vital.Recibe la configuración (como la conexión a la DB)
         // y se la pasa a la clase base (base(options))
       public GymDbContext(DbContextOptions<GymDbContext> options) : base(options)
        {

        }

       public DbSet<User> Users { get; set; }
        public DbSet<Member> Members { get; set; }

        // Este método es el "mapeador". Aquí le decimos a EF exactamente
        // cómo se llama la tabla y qué reglas tiene.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {

                //mapeo real de la tabla de mi sql segun el diagrama del sad. 
                entity.ToTable("usuarios");
                // configurando la clave primaria. 
                entity.HasKey(e => e.UserId);
                // Configuramos el nombre de usuario para que sea obligatorio y único
                entity.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(50);

                entity.HasIndex(e => e.Username)
                .IsUnique();

                entity.Property(e => e.Role).IsRequired().HasMaxLength(20);
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.Property(e => e.Salt).IsRequired();


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

            });

           
        }

    }
}
