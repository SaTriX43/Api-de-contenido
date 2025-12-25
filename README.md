📘 API de Contenido — Posts, Comentarios y Likes

API REST desarrollada en ASP.NET Core (.NET 8) que implementa un sistema tipo blog / red social simple, con autenticación JWT, reglas de negocio reales y arquitectura por capas.

Este proyecto forma parte de mi portafolio como Backend .NET Junior, enfocado en buenas prácticas, lógica de negocio y código mantenible.

🚀 Tecnologías utilizadas

ASP.NET Core Web API (.NET 8)

Entity Framework Core

SQL Server

JWT Authentication

BCrypt.Net (hash de contraseñas)

LINQ

Serilog (logging)

Swagger / OpenAPI

Result Pattern

Middleware global de errores

🧱 Arquitectura

Arquitectura en capas, separando responsabilidades:

Controllers
   ↓
Services (reglas de negocio)
   ↓
Repositories (acceso a datos)
   ↓
Entity Framework Core


Capas principales:

Controllers: manejo de HTTP, validaciones básicas y claims JWT

Services: lógica de negocio, ownership, validaciones de dominio

Repositories: acceso a datos (EF Core)

DTOs: contratos de entrada y salida

Middleware: manejo global de errores

📦 Entidades del sistema

Usuario

Publicación

Comentario

Like

Relaciones

Usuario 1:N Publicaciones

Publicación 1:N Comentarios

Usuario N:N Publicaciones (Likes)

Los likes se controlan mediante una restricción única (UsuarioId + PublicacionId) para evitar duplicados.

🔐 Autenticación y Autorización

Autenticación basada en JWT

Registro y login de usuarios

Hash de contraseñas con BCrypt

Claims utilizados:

UserId

Email

Role

Autorización aplicada

Solo usuarios autenticados pueden:

crear publicaciones

comentar

dar o quitar like

Ownership:

solo el autor puede editar o eliminar su publicación

📌 Endpoints principales
🔑 Autenticación

POST /api/autenticacion/register

POST /api/autenticacion/login

📝 Publicaciones

POST /api/publicacion/crear-publicacion

GET /api/publicacion/obtener-publicaciones

PUT /api/publicacion/{id}/actualizar

DELETE /api/publicacion/{id}/eliminar

Soporta:

filtro por autor

filtro por fecha

ordenamiento por popularidad (likes)

💬 Comentarios

POST /api/publicacion/{id}/crear-comentario

Reglas:

no se permiten comentarios vacíos

no se permite comentar publicaciones eliminadas

usuarios baneados no pueden comentar

❤️ Likes

POST /api/publicacion/{id}/like

Implementado como toggle:

si el like existe → se elimina

si no existe → se crea

Devuelve:

{
  "isLiked": true,
  "likes": 10
}

🧠 Reglas de negocio implementadas

Un usuario no puede dar like dos veces a la misma publicación

No se permiten comentarios vacíos

Usuarios baneados:

no pueden iniciar sesión

no pueden comentar

no pueden dar like

Publicaciones eliminadas:

no aceptan comentarios

no aceptan likes

Solo el dueño puede editar o eliminar su publicación

Uso de soft delete (Eliminado = true)

⚠️ Manejo de errores

Middleware global de errores

Uso del Result Pattern para evitar excepciones innecesarias

Respuestas consistentes desde los servicios

Controllers solo traducen el resultado a HTTP

🧪 Validaciones

DataAnnotations en DTOs

Validaciones de dominio en Services

Normalización de datos sensibles:

email (Trim + ToLower)

No se normalizan campos de presentación (ej: título)

🗃️ Base de datos

SQL Server

Migrations con Entity Framework Core

Índices únicos:

Email de usuario

Like (UsuarioId + PublicacionId)

▶️ Ejecución del proyecto

Configurar appsettings.json:

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=ApiContenidoDb;Trusted_Connection=True;"
  },
  "Jwt": {
    "Key": "clave-super-secreta",
    "Issuer": "ApiContenido",
    "Audience": "ApiContenido",
    "ExpireMinutes": 60
  }
}


Ejecutar migraciones:

dotnet ef database update


Ejecutar el proyecto:

dotnet run


Acceder a Swagger:

https://localhost:{puerto}/swagger

🎯 Objetivo del proyecto

Este proyecto fue desarrollado para:

consolidar conocimientos de backend real

practicar reglas de negocio

trabajar con JWT sin Identity

reforzar ownership, relaciones y validaciones

construir un portafolio sólido para roles Junior / Trainee

👤 Autor

Santiago
Backend Developer (.NET)
Ecuador 🇪🇨

Proyecto desarrollado como parte de mi formación práctica para roles Backend .NET Junior.