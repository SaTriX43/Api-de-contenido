using API_de_Contenido.DALs.PublicacionRepositoryCarpeta;
using API_de_Contenido.DTOs.PublicacionDtoCarpeta;
using API_de_Contenido.Models;

namespace API_de_Contenido.Services.PublicacionServiceCarpeta
{
    public interface IPublicacionService
    {
        public Task<Result<PublicacionDto>> CrearPublicacionAsync(PublicacionCrearDto publicacionCrearDto, int usuarioId);
        public Task<Result<PublicacionDto>> ActualizarPublicacionAsync(PublicacionCrearDto publicacionActualizarDto, int publicacionId, int usuarioId);
        public Task<Result> EliminarPublicacionAsync(int publicacionId, int usuarioId);
        public Task<Result<List<PublicacionDto>>> ObtenerPublicacionesAsync(int? autor,DateTime? fechaInicio, DateTime? fechaFinal);
    }
}
