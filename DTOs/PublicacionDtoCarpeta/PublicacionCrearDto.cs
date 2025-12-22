using System.ComponentModel.DataAnnotations;

namespace API_de_Contenido.DTOs.PublicacionDtoCarpeta
{
    public class PublicacionCrearDto
    {
        [Required]
        [StringLength(200,ErrorMessage ="El titulo no puede tener mas de 200 caracteres")]

        public string Titulo { get; set; }
        [Required]
        [StringLength(500, ErrorMessage = "El titulo no puede tener mas de 500 caracteres")]

        public string Contenido { get; set; } 
    }
}
