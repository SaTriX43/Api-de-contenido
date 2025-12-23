using API_de_Contenido.DTOs.LikeDtoCarpeta;
using API_de_Contenido.Models;

namespace API_de_Contenido.Services.LikeServiceCarpeta
{
    public interface ILikeService
    {
        public Task<Result<LikeRespuestaDto>> ToggleLikeAsync(int publicacionId, int usuarioId);
    }
}
