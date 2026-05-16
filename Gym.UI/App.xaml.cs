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
       
        public static IHost? AppHost { get; private set; }

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, config) =>
                {
                    // Configuramos de dónde leer los datos 
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    
                    string connectionString = context.Configuration.GetConnectionString("DefaultConnection")!;
                    
                    services.AddDbContext<GymDbContext>(options =>
                        options.UseSqlServer(connectionString));

                    // Registramos nuestras Ventanas (Windows) para que el DI pueda crearlas
                    services.AddSingleton<MainWindow>();
                    services.AddTransient<Gym.UI.Views.LoginView>();
                    services.AddTransient<Gym.UI.ViewModels.LoginViewModel>();
                    services.AddTransient<Gym.UI.ViewModels.MainViewModel>();
                    services.AddTransient<Gym.UI.ViewModels.DashboardViewModel>();
                    services.AddTransient<Gym.UI.ViewModels.MembersViewModel>();
                    services.AddTransient<Gym.UI.Views.AddMemberWindow>();
                    services.AddTransient<Gym.UI.ViewModels.AddMemberViewModel>();
                    services.AddTransient<Gym.UI.Views.EditMemberWindow>();
                    services.AddTransient<Gym.UI.ViewModels.EditMemberViewModel>();

                    // Registrar el UnitOfWork (Scoped para que viva durante la petición/transacción)
                    services.AddScoped<Gym.Data.Interfaces.IUnitOfWork, Gym.Data.Repositories.UnitOfWork>();

                    //egistrar el Servicio de Negocio
                    services.AddScoped<Gym.Business.Interfaces.IAuthService, Gym.Business.Services.AuthService>();
                    services.AddScoped<Gym.Business.Interfaces.IMemberService, Gym.Business.Services.MemberService>();
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            // Arrancamos el host
            await AppHost!.StartAsync();

        
            using (var scope = AppHost.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<GymDbContext>();
                
              
                if (!await dbContext.Users.AnyAsync())
                {
                    
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

            var loginWindow = AppHost.Services.GetRequiredService<Gym.UI.Views.LoginView>();
            loginWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
           
            if (AppHost != null)
            {
                await AppHost.StopAsync();
                AppHost.Dispose();
            }

            base.OnExit(e);
        }
    }
}
