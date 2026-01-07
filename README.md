# 📝 API de Contenido (Blog / Social)

API REST desarrollada con **ASP.NET Core** que implementa un sistema de contenido tipo **blog / red social básica**, aplicando buenas prácticas de backend, reglas de negocio y una arquitectura limpia orientada a proyectos reales.

Este proyecto forma parte de un roadmap de aprendizaje enfocado en **Backend .NET Junior**.

---

## 🚀 Tecnologías utilizadas

- ASP.NET Core Web API
- .NET 8
- Entity Framework Core
- SQL Server
- JWT Authentication
- Unit of Work
- Result Pattern
- Swagger / OpenAPI

---

## 🧱 Arquitectura

El proyecto sigue una arquitectura por capas:

Controllers
Services (Lógica de negocio)
Repositories (Acceso a datos)
DTOs
Models (Entidades)


Se implementa el **patrón Unit of Work** para coordinar repositorios y garantizar consistencia en las operaciones de escritura.

---

## 🔐 Autenticación y autorización

- Autenticación basada en **JWT**
- Uso de claims para identificar al usuario autenticado
- Aplicación de **ownership**:
  - Solo el autor puede editar o eliminar su contenido
  - Las acciones se validan usando el `UserId` obtenido desde el token

---

## 📦 Entidades principales

- **Usuario**
- **Publicación (Post)**
- **Comentario**
- **Like**

---

## ⚙️ Funcionalidades

### Publicaciones
- Crear publicación
- Editar publicación (solo autor)
- Eliminar publicación (solo autor)
- Obtener publicaciones con filtros y ordenamiento

### Comentarios
- Crear comentarios en publicaciones
- Validación de comentarios no vacíos
- Ownership aplicado al eliminar comentarios

### Likes
- Dar / quitar like a una publicación (toggle)
- Un usuario no puede dar like más de una vez a la misma publicación

---

## 🧠 Reglas de negocio implementadas

- Un usuario no puede dar like dos veces a la misma publicación
- Los comentarios no pueden ser vacíos
- El contenido solo puede ser modificado por su autor
- El backend es responsable de validar todas las reglas (no el cliente)

---

## 📊 Consultas avanzadas

- Obtener publicaciones con:
  - Cantidad de likes
  - Cantidad de comentarios
- Filtros:
  - Por autor
  - Por rango de fechas
- Ordenamiento:
  - Más recientes
  - Más populares (por likes)

---

## 📌 Buenas prácticas aplicadas

- Controllers delgados
- Lógica de negocio centralizada en Services
- Repositories sin lógica de dominio
- Uso de DTOs para evitar exponer entidades
- Result Pattern para manejo consistente de errores
- Separación clara de responsabilidades
- Endpoints pensados desde una perspectiva REST

---

## 🎯 Objetivo del proyecto

Este proyecto fue desarrollado con fines educativos para:

- Consolidar conceptos de backend en .NET
- Practicar diseño de APIs REST reales
- Aplicar reglas de negocio y ownership
- Preparar un proyecto demostrable para un rol **Backend Junior**

---

## 📂 Estado del proyecto

✔️ **Proyecto completado (Nivel 3)**  
El sistema cumple con todos los requisitos planteados y está listo para ser utilizado como parte de un portafolio profesional.

---

## 🧑‍💻 Autor

Desarrollado por **Santiago**  
Backend .NET Developer en formación
