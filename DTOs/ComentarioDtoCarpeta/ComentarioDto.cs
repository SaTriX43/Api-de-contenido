namespace API_de_Contenido.DTOs.ComentarioDtoCarpeta
{
    public class ComentarioDto
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        public int PublicacionId { get; set; }

        public string Contenido { get; set; } = null!;

        public DateTime FechaCreacion { get; set; }

        public bool Eliminado { get; set; } = false;
    }
}
