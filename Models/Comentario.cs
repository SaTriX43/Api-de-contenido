using API_de_Contenido.Models;

public class Comentario
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public int PublicacionId { get; set; }

    public string Contenido { get; set; } = null!;

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    public bool Eliminado { get; set; } = false;

    public Usuario Usuario { get; set; } = null!;

    public Publicacion Publicacion { get; set; } = null!;
}
