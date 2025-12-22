using System.ComponentModel.DataAnnotations;

namespace API_de_Contenido.DTOs.AutenticacionDtoCarpeta
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
