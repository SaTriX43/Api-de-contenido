using API_de_Contenido.DTOs.ComentarioDtoCarpeta;
using API_de_Contenido.Models;

namespace API_de_Contenido.Services.ComentarioServiceCarpeta
{
    public interface IComentarioService
    {
        public Task<Result<ComentarioDto>> CrearComentarioAsync(ComentarioCrearDto comentarioCrearDto, int publicacionId, int usuarioId);
    }
}
