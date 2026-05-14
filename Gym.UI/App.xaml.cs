using System.IO;
using System.Windows;
using Gym.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Gym.UI
{
    public partial class App : Application
    {
        // El Host es el contenedor de todos nuestros servicios y configuración
        public static IHost? AppHost { get; private set; }

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, config) =>
                {
                    // Configuramos de dónde leer los datos (appsettings.json)
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    // 1. Registramos el DbContext usando la Connection String del archivo JSON
                    string connectionString = context.Configuration.GetConnectionString("DefaultConnection")!;
                    
                    services.AddDbContext<GymDbContext>(options =>
                        options.UseSqlServer(connectionString));

                    // 2. Registramos nuestras Ventanas (Windows) para que el DI pueda crearlas
                    services.AddSingleton<MainWindow>();
                    services.AddTransient<Gym.UI.Views.LoginView>();
                    services.AddTransient<Gym.UI.ViewModels.LoginViewModel>();
                    services.AddTransient<Gym.UI.ViewModels.MainViewModel>();
                    services.AddTransient<Gym.UI.Views.AddMemberWindow>();
                    services.AddTransient<Gym.UI.ViewModels.AddMemberViewModel>();

                    // 3. Registrar el UnitOfWork (Scoped para que viva durante la petición/transacción)
                    services.AddScoped<Gym.Data.Interfaces.IUnitOfWork, Gym.Data.Repositories.UnitOfWork>();

                    // 4. Registrar el Servicio de Negocio
                    services.AddScoped<Gym.Business.Interfaces.IAuthService, Gym.Business.Services.AuthService>();
                    services.AddScoped<Gym.Business.Interfaces.IMemberService, Gym.Business.Services.MemberService>();
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            // Arrancamos el host
            await AppHost!.StartAsync();

            // SCRIPT TEMPORAL DE INYECCIÓN (SEEDER)
            using (var scope = AppHost.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<GymDbContext>();
                
                // Verificamos si no hay usuarios en la base de datos
                if (!await dbContext.Users.AnyAsync())
                {
                    // Generamos un usuario administrador por defecto
                    string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
                    string hash = BCrypt.Net.BCrypt.HashPassword("admin123", salt);

                    var adminUser = new Gym.Domain.Entities.User
                    {
                        Username = "admin",
                        PasswordHash = hash,
                        Salt = salt,
                        Role = "Administrador",
                        FullName = "Administrador del Sistema",
                        Email = "admin@cilgym.com",
                        Phone = "000-000-0000",
                        IsActive = true,
                        CreatedAt = System.DateTime.Now,
                        LastAccess = System.DateTime.Now
                    };

                    dbContext.Users.Add(adminUser);
                    await dbContext.SaveChangesAsync();
                }
            }

            // Pedimos la LoginView al contenedor y la mostramos
            var loginWindow = AppHost.Services.GetRequiredService<Gym.UI.Views.LoginView>();
            loginWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            // Apagamos el host de forma limpia para liberar recursos (como la conexión a la DB)
            if (AppHost != null)
            {
                await AppHost.StopAsync();
                AppHost.Dispose();
            }

            base.OnExit(e);
        }
    }
}
