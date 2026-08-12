# ============================================================
# Script de Configuración - Clean Architecture para GymManager
# Ejecutar desde la raíz del proyecto (donde está GymManager.sln)
# ============================================================

Write-Host "🏗️  Creando proyectos de Clean Architecture..." -ForegroundColor Cyan

# PASO 1: Crear los 6 nuevos proyectos
dotnet new classlib -n "Gym.Core.Domain" -f net9.0 -o "Gym.Core.Domain"
dotnet new classlib -n "Gym.Core.Application" -f net9.0 -o "Gym.Core.Application"
dotnet new classlib -n "Gym.Infrastructure.Persistence" -f net9.0 -o "Gym.Infrastructure.Persistence"
dotnet new classlib -n "Gym.Infrastructure.Identity" -f net9.0 -o "Gym.Infrastructure.Identity"
dotnet new classlib -n "Gym.Infrastructure.Shared" -f net9.0 -o "Gym.Infrastructure.Shared"
dotnet new classlib -n "Gym.IoC" -f net9.0 -o "Gym.IoC"

Write-Host "✅ Proyectos creados" -ForegroundColor Green

# PASO 2: Agregar proyectos a la solución
Write-Host "📦 Agregando proyectos a la solución..." -ForegroundColor Cyan
dotnet sln GymManager.sln add "Gym.Core.Domain/Gym.Core.Domain.csproj"
dotnet sln GymManager.sln add "Gym.Core.Application/Gym.Core.Application.csproj"
dotnet sln GymManager.sln add "Gym.Infrastructure.Persistence/Gym.Infrastructure.Persistence.csproj"
dotnet sln GymManager.sln add "Gym.Infrastructure.Identity/Gym.Infrastructure.Identity.csproj"
dotnet sln GymManager.sln add "Gym.Infrastructure.Shared/Gym.Infrastructure.Shared.csproj"
dotnet sln GymManager.sln add "Gym.IoC/Gym.IoC.csproj"

Write-Host "✅ Proyectos agregados a la solución" -ForegroundColor Green

# PASO 3: Configurar referencias entre proyectos
Write-Host "🔗 Configurando referencias entre capas..." -ForegroundColor Cyan

# Application referencia Domain
dotnet add "Gym.Core.Application/Gym.Core.Application.csproj" reference "Gym.Core.Domain/Gym.Core.Domain.csproj"

# Infrastructure.Persistence referencia Application y Domain
dotnet add "Gym.Infrastructure.Persistence/Gym.Infrastructure.Persistence.csproj" reference "Gym.Core.Application/Gym.Core.Application.csproj"
dotnet add "Gym.Infrastructure.Persistence/Gym.Infrastructure.Persistence.csproj" reference "Gym.Core.Domain/Gym.Core.Domain.csproj"

# Infrastructure.Identity referencia Application y Domain
dotnet add "Gym.Infrastructure.Identity/Gym.Infrastructure.Identity.csproj" reference "Gym.Core.Application/Gym.Core.Application.csproj"
dotnet add "Gym.Infrastructure.Identity/Gym.Infrastructure.Identity.csproj" reference "Gym.Core.Domain/Gym.Core.Domain.csproj"

# Infrastructure.Shared referencia Application y Domain
dotnet add "Gym.Infrastructure.Shared/Gym.Infrastructure.Shared.csproj" reference "Gym.Core.Application/Gym.Core.Application.csproj"
dotnet add "Gym.Infrastructure.Shared/Gym.Infrastructure.Shared.csproj" reference "Gym.Core.Domain/Gym.Core.Domain.csproj"

# IoC referencia Application y todas las Infraestructuras
dotnet add "Gym.IoC/Gym.IoC.csproj" reference "Gym.Core.Application/Gym.Core.Application.csproj"
dotnet add "Gym.IoC/Gym.IoC.csproj" reference "Gym.Infrastructure.Persistence/Gym.Infrastructure.Persistence.csproj"
dotnet add "Gym.IoC/Gym.IoC.csproj" reference "Gym.Infrastructure.Identity/Gym.Infrastructure.Identity.csproj"
dotnet add "Gym.IoC/Gym.IoC.csproj" reference "Gym.Infrastructure.Shared/Gym.Infrastructure.Shared.csproj"

Write-Host "✅ Referencias configuradas" -ForegroundColor Green

# PASO 4: Crear carpetas internas
Write-Host "📁 Creando estructura de carpetas..." -ForegroundColor Cyan

# Gym.Core.Domain
New-Item -ItemType Directory -Force -Path "Gym.Core.Domain/Entities"
New-Item -ItemType Directory -Force -Path "Gym.Core.Domain/Common/Enums"
New-Item -ItemType Directory -Force -Path "Gym.Core.Domain/Common/Results"
New-Item -ItemType Directory -Force -Path "Gym.Core.Domain/Common/Constants"

# Gym.Core.Application
New-Item -ItemType Directory -Force -Path "Gym.Core.Application/Contracts"
New-Item -ItemType Directory -Force -Path "Gym.Core.Application/DTOs"
New-Item -ItemType Directory -Force -Path "Gym.Core.Application/Services"
New-Item -ItemType Directory -Force -Path "Gym.Core.Application/Mapping"

# Gym.Infrastructure.Persistence
New-Item -ItemType Directory -Force -Path "Gym.Infrastructure.Persistence/Context"
New-Item -ItemType Directory -Force -Path "Gym.Infrastructure.Persistence/Configurations"
New-Item -ItemType Directory -Force -Path "Gym.Infrastructure.Persistence/Repositories"
New-Item -ItemType Directory -Force -Path "Gym.Infrastructure.Persistence/Migrations"
New-Item -ItemType Directory -Force -Path "Gym.Infrastructure.Persistence/Seeds"

# Gym.Infrastructure.Identity
New-Item -ItemType Directory -Force -Path "Gym.Infrastructure.Identity/DbContext"
New-Item -ItemType Directory -Force -Path "Gym.Infrastructure.Identity/Entities"
New-Item -ItemType Directory -Force -Path "Gym.Infrastructure.Identity/Seeds"
New-Item -ItemType Directory -Force -Path "Gym.Infrastructure.Identity/Services"
New-Item -ItemType Directory -Force -Path "Gym.Infrastructure.Identity/RegAndConfig"

# Gym.Infrastructure.Shared
New-Item -ItemType Directory -Force -Path "Gym.Infrastructure.Shared/Services"

# Gym.IoC
New-Item -ItemType Directory -Force -Path "Gym.IoC/Dependencies"

Write-Host "✅ Carpetas creadas" -ForegroundColor Green

# PASO 5: Eliminar Class1.cs autogenerados
Write-Host "🧹 Limpiando archivos autogenerados..." -ForegroundColor Cyan
Remove-Item -Force -ErrorAction SilentlyContinue "Gym.Core.Domain/Class1.cs"
Remove-Item -Force -ErrorAction SilentlyContinue "Gym.Core.Application/Class1.cs"
Remove-Item -Force -ErrorAction SilentlyContinue "Gym.Infrastructure.Persistence/Class1.cs"
Remove-Item -Force -ErrorAction SilentlyContinue "Gym.Infrastructure.Identity/Class1.cs"
Remove-Item -Force -ErrorAction SilentlyContinue "Gym.Infrastructure.Shared/Class1.cs"
Remove-Item -Force -ErrorAction SilentlyContinue "Gym.IoC/Class1.cs"

Write-Host "✅ Archivos Class1.cs eliminados" -ForegroundColor Green

# PASO 6: Verificar que compila
Write-Host "🔨 Compilando solución..." -ForegroundColor Cyan
dotnet build GymManager.sln

Write-Host ""
Write-Host "🎉 ¡Arquitectura Clean creada exitosamente!" -ForegroundColor Green
Write-Host "📚 Revisa el Manual de Arquitectura para entender cada carpeta." -ForegroundColor Yellow
