# 🎨 Artistas API v1 - Descripción del Proyecto

https://github.com/matismasters/Artistasv1

## Resumen General

**Artistas API v1** es una API REST desarrollada en **ASP.NET Core 8.0** que permite gestionar información de artistas musicales. El sistema incluye autenticación JWT, categorización de artistas, y gestión de usuarios. La aplicación utiliza **Entity Framework Core** con **SQL Server** como base de datos y proporciona una interfaz Swagger para documentación y pruebas.

### Características Principales
- ✅ **Gestión completa de artistas** (CRUD)
- ✅ **Sistema de autenticación JWT**
- ✅ **Registro y login de usuarios**
- ✅ **Categorización de artistas**
- ✅ **Validación de datos con DTOs**
- ✅ **Documentación automática con Swagger**
- ✅ **Base de datos SQL Server con EF Core**

---

## 📁 Estructura del Proyecto

### Archivo de Solución
```
Artistas.sln
```
**Descripción**: Archivo de solución de Visual Studio que contiene la configuración del proyecto ASP.NET Core.

---

## 📂 Directorio Principal: `/Artistas/`

### 🔧 Archivos de Configuración

#### `Program.cs`
**Descripción**: Punto de entrada principal de la aplicación. Configura:
- Conexión a base de datos SQL Server
- Autenticación JWT con validación de tokens
- Swagger UI con soporte para autorización Bearer
- Inyección de dependencias
- Middleware de autenticación y autorización

#### `Artistas.csproj`
**Descripción**: Archivo de proyecto que define:
- Framework objetivo: .NET 8.0
- Dependencias del proyecto:
  - `Microsoft.AspNetCore.Authentication.JwtBearer` (8.0.0)
  - `Microsoft.EntityFrameworkCore.SqlServer` (9.0.6)
  - `Microsoft.EntityFrameworkCore.Tools` (9.0.6)
  - `Swashbuckle.AspNetCore` (6.6.2)

#### `appsettings.json`
**Descripción**: Configuración principal de la aplicación que incluye:
- **JWT Configuration**: Clave secreta, emisor, audiencia y duración (2 horas)
- **Connection String**: Configuración para SQL Server Express local
- **Logging**: Niveles de registro para desarrollo

#### `appsettings.Development.json`
**Descripción**: Configuración específica para el entorno de desarrollo.

#### `Artistas.http`
**Descripción**: Archivo con ejemplos de peticiones HTTP para probar la API.

---

### 🎛️ Controladores (`/Controllers/`)

#### `ArtistasController.cs`
**Descripción**: Controlador principal que maneja todas las operaciones CRUD para artistas.
**Endpoints**:
- `GET /api/artistas` - Lista todos los artistas con información completa
- `GET /api/artistas/{id}` - Obtiene un artista específico por ID
- `POST /api/artistas` - Crea un nuevo artista (requiere autenticación)
- `PUT /api/artistas/{id}` - Actualiza un artista existente
- `DELETE /api/artistas/{id}` - Elimina un artista

**Características**:
- Requiere autenticación JWT (`[Authorize]`)
- Incluye validaciones de negocio
- Manejo de errores y excepciones
- Retorna DTOs estructurados

#### `UsuariosController.cs`
**Descripción**: Maneja el registro y autenticación de usuarios.
**Endpoints**:
- `POST /api/usuarios` - Registro de nuevos usuarios
- `POST /api/usuarios/login` - Autenticación y generación de tokens JWT

**Características**:
- Encriptación de contraseñas con MD5
- Validación de emails únicos
- Generación de tokens JWT para sesiones

#### `WeatherForecastController.cs`
**Descripción**: Controlador de ejemplo (template por defecto de ASP.NET Core). Puede ser removido en producción.

---

### 🗃️ Modelos de Datos (`/Models/`)

#### `Artista.cs`
**Descripción**: Modelo principal que representa un artista musical.
**Propiedades**:
- `Id`: Identificador único
- `Nombre`: Nombre del artista (requerido, único)
- `Genero`: Género musical
- `FechaNacimiento`: Fecha de nacimiento (`DateOnly`)
- `Nacionalidad`: País de origen
- `CategoriaId`: Referencia a categoría
- `UsuarioId`: Referencia al usuario que lo creó

#### `Usuario.cs`
**Descripción**: Modelo para usuarios del sistema.
**Propiedades**:
- `Id`: Identificador único
- `Email`: Email único del usuario
- `PasswordEncriptado`: Contraseña hasheada con MD5
- `Artistas`: Lista de artistas creados por el usuario

**Métodos**:
- `EncriptarPassword()`: Método estático para encriptar contraseñas

#### `Categoria.cs`
**Descripción**: Modelo para categorizar artistas.
**Propiedades**:
- `Id`: Identificador único
- `Nombre`: Nombre de la categoría (requerido, único)
- `Descripcion`: Descripción opcional
- `Artistas`: Lista de artistas en esta categoría

---

### 📋 DTOs - Objetos de Transferencia de Datos (`/Models/DTOs/`)

#### `ArtistaDTO.cs`
**Descripción**: DTO para crear/actualizar artistas.
**Campos**: Id, Nombre (requerido), Genero, FechaNacimiento (requerido), Nacionalidad, CategoriaId (requerido)

#### `RespuestaArtistaDTO.cs`
**Descripción**: DTO para respuestas de artistas con información completa.
**Campos**: Incluye todos los datos del artista más información de categoría y usuario asociado.

#### `LoginUsuarioDTO.cs`
**Descripción**: DTO para autenticación de usuarios.
**Campos**: Email (requerido), Password (requerido)

#### `RegistroUsuarioDTO.cs`
**Descripción**: DTO para registro de nuevos usuarios.
**Campos**: Email (requerido), Password (requerido)

#### `RespuestaLoginTokenDTO.cs`
**Descripción**: DTO para devolver el token JWT tras login exitoso.
**Campos**: Token (requerido)

---

### 🛠️ Helpers (`/Helpers/`)

#### `Autentica.cs`
**Descripción**: Clase helper para manejo de autenticación JWT.
**Funcionalidad**:
- `CrearToken(Usuario usuario)`: Genera tokens JWT válidos
- Incluye claims de usuario (ID, email)
- Configura expiración y firma del token
- Lee configuración desde `appsettings.json`

---

### 🗄️ Datos (`/Data/`)

#### `AppDbContext.cs`
**Descripción**: Contexto de Entity Framework que define:
- **DbSets**: Artistas, Categorias, Usuarios
- **Relaciones**:
  - Artista → Categoria (Many-to-One)
  - Usuario → Artistas (One-to-Many)
- **Índices únicos**: Nombres de artistas, categorías y emails
- **Configuración de borrado**: NoAction para evitar cascadas

---

### 🔄 Migraciones (`/Migrations/`)

#### `20250623224330_Inicial.cs` y `20250623224330_Inicial.Designer.cs`
**Descripción**: Migración inicial que crea:
- Tabla `Categorias` con índice único en Nombre
- Tabla `Artistas` con relación a Categorias e índice único en Nombre

#### `20250627221644_AgregarUsuarios.cs` y `20250627221644_AgregarUsuarios.Designer.cs`
**Descripción**: Migración que añade:
- Tabla `Usuarios` con email único
- Relación Usuario → Artistas
- Actualización del modelo para incluir usuarios

#### `AppDbContextModelSnapshot.cs`
**Descripción**: Snapshot del modelo actual de la base de datos para Entity Framework.

---

### ⚙️ Configuración (`/Properties/`)

#### `launchSettings.json`
**Descripción**: Configuración de lanzamiento para desarrollo, incluyendo:
- Puertos de desarrollo
- URLs de inicio
- Configuración de IIS Express
- Variables de entorno

---

### 📄 Otros Archivos

#### `WeatherForecast.cs`
**Descripción**: Modelo de ejemplo para el controlador WeatherForecast (puede ser removido).

---

## 🔐 Seguridad

### Autenticación JWT
- **Algoritmo**: HMAC SHA-256
- **Duración**: 2 horas (configurable)
- **Claims incluidos**: ID de usuario, email, timestamp
- **Validación**: Emisor, audiencia, tiempo de vida y firma

### Encriptación de Contraseñas
- **Algoritmo**: MD5 (⚠️ Recomendación: actualizar a bcrypt o Argon2)
- **Almacenamiento**: Solo hash, nunca texto plano

---

## 📋 Funcionalidades Principales

### 1. Gestión de Artistas
- Crear, leer, actualizar y eliminar artistas
- Validación de nombres únicos
- Categorización obligatoria
- Asociación con usuarios autenticados

### 2. Sistema de Usuarios
- Registro con email único
- Login con generación de JWT
- Cada usuario puede gestionar sus propios artistas

### 3. Categorización
- Sistema de categorías para clasificar artistas
- Nombres únicos de categorías
- Relación uno-a-muchos con artistas

### 4. API RESTful
- Endpoints estándar REST
- Documentación automática con Swagger
- Validación de modelos
- Manejo de errores estructurado

---

## 🚀 Tecnologías Utilizadas

- **Framework**: ASP.NET Core 8.0
- **Base de Datos**: SQL Server Express
- **ORM**: Entity Framework Core 9.0.6
- **Autenticación**: JWT Bearer Tokens
- **Documentación**: Swagger/OpenAPI
- **Validación**: Data Annotations
- **Arquitectura**: MVC con Repository Pattern implícito

---

## 📝 Notas de Desarrollo

### Mejoras Recomendadas
1. **Seguridad**: Reemplazar MD5 por bcrypt o Argon2 para contraseñas
2. **Validación**: Añadir más validaciones de negocio
3. **Logging**: Implementar logging estructurado
4. **Testing**: Añadir pruebas unitarias e integración
5. **Paginación**: Implementar paginación en listados
6. **Cache**: Considerar implementar cache para consultas frecuentes

### Arquitectura
El proyecto sigue una arquitectura en capas típica de ASP.NET Core:
- **Presentación**: Controladores y DTOs
- **Lógica de Negocio**: Modelos y validaciones
- **Acceso a Datos**: Entity Framework Core y DbContext
- **Infraestructura**: Helpers y configuración

---

*Esta documentación refleja el estado actual del proyecto Artistasv1 y debe actualizarse conforme evolucione el sistema.*