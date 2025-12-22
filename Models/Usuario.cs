using API_de_Contenido.Models.Enums;

namespace API_de_Contenido.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool Baneado { get; set; } = false;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public UsuarioRol Rol { get; set; }
        public ICollection<Publicacion> Publicaciones { get; set; } = new List<Publicacion>();

        public ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

        public ICollection<Like> Likes { get; set; } = new List<Like>();
    }
}
