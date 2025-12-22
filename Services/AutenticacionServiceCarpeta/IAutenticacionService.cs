using API_de_Contenido.DTOs.AutenticacionDtoCarpeta;
using API_de_Contenido.Models;

namespace API_de_Contenido.Services
{
    public interface IAutenticacionService
    {
        Task<Result<string>> RegisterAsync(RegisterDto dto);
        Task<Result<string>> LoginAsync(LoginDto dto);
    }
}
