using API_de_Contenido.Models;

public class Like
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public int PublicacionId { get; set; }

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    public Usuario Usuario { get; set; } = null!;

    public Publicacion Publicacion { get; set; } = null!;
}
