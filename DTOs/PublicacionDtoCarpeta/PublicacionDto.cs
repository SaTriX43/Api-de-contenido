using API_de_Contenido.DTOs.ComentarioDtoCarpeta;

namespace API_de_Contenido.DTOs.PublicacionDtoCarpeta
{
    public class PublicacionDto
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        public string Titulo { get; set; }

        public string Contenido { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaEdicion { get; set; }

        public bool Eliminado { get; set; } = false;

        public List<ComentarioDto>? ComentarioDtos { get; set; }
        public int? Likes {  get; set; }
    }
}
