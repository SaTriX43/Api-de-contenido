using System.ComponentModel.DataAnnotations;

namespace API_de_Contenido.DTOs.ComentarioDtoCarpeta
{
    public class ComentarioCrearDto
    {
        [Required]
        [StringLength(500, MinimumLength = 1)]
        public string Contenido { get; set; } = null!;
    }
}
