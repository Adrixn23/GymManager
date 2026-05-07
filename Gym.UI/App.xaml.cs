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
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
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

                    // AQUÍ registraremos los Servicios y Repositorios en los siguientes pasos
                    // Ejemplo: services.AddScoped<IAuthService, AuthService>();
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            // Arrancamos el host
            await _host.StartAsync();

            // Pedimos la MainWindow al contenedor y la mostramos
            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            // Apagamos el host de forma limpia para liberar recursos (como la conexión a la DB)
            using (_host)
            {
                await _host.StopAsync();
            }

            base.OnExit(e);
        }
    }
}
