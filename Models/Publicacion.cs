using API_de_Contenido.Models;

public class Publicacion
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public string Titulo { get; set; } = null!;

    public string Contenido { get; set; } = null!;

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    public DateTime? FechaEdicion { get; set; }

    public bool Eliminado { get; set; } = false;

    public Usuario Usuario { get; set; } = null!;

    public ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

    public ICollection<Like> Likes { get; set; } = new List<Like>();
}
