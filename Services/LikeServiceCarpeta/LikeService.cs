using API_de_Contenido.DALs.LikeRepositoryCarpeta;
using API_de_Contenido.DALs.PublicacionRepositoryCarpeta;
using API_de_Contenido.DALs.UsuarioRepositoryCarpeta;
using API_de_Contenido.DTOs.LikeDtoCarpeta;
using API_de_Contenido.Models;

namespace API_de_Contenido.Services.LikeServiceCarpeta
{
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository _likeRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPublicacionRepository _publicacionRepository;

        public LikeService(ILikeRepository likeRepository, IUsuarioRepository usuarioRepository, IPublicacionRepository publicacionRepository)
        {
            _likeRepository = likeRepository;
            _usuarioRepository = usuarioRepository;
            _publicacionRepository = publicacionRepository;
        }


        public async Task<Result<LikeRespuestaDto>> ToggleLikeAsync(int publicacionId, int usuarioId)
        {
            var usuarioExiste = await _usuarioRepository.ObtenerPorIdAsync(usuarioId);

            if(usuarioExiste == null)
            {
                return Result<LikeRespuestaDto>.Failure($"Su usuario con id = {usuarioId} no existe");
            }

            if(usuarioExiste.Baneado)
            {
                return Result<LikeRespuestaDto>.Failure($"Su usuario con id = {usuarioId} esta baneado");
            }

            var publicacionExiste = await _publicacionRepository.ObtenerPublicacionPorIdAsync(publicacionId);

            if (publicacionExiste == null)
            {
                return Result<LikeRespuestaDto>.Failure($"Su publicacion con id = {publicacionId} no existe");
            }

            if (publicacionExiste.Eliminado)
            {
                return Result<LikeRespuestaDto>.Failure($"Su publicacion con id = {publicacionId} esta eliminada");
            }

            var likeExiste = await _likeRepository.ObtenerLikeAsync(publicacionId, usuarioId);

            if(likeExiste != null)
            {
                await _likeRepository.EliminarLikeAsync(likeExiste);
                return Result<LikeRespuestaDto>.Success(new LikeRespuestaDto
                {
                    IsLiked = false,
                    Likes = await _likeRepository.NumeroDeLikesAsync(publicacionId)
                });
            }

            var likeModel = new Like
            {
                PublicacionId = publicacionId,
                UsuarioId = usuarioId,
                FechaCreacion = DateTime.UtcNow
            };

            await _likeRepository.CrearLikeAsync(likeModel);

            return Result<LikeRespuestaDto>.Success(new LikeRespuestaDto
            {
                IsLiked = true,
                Likes = await _likeRepository.NumeroDeLikesAsync(publicacionId)
            });
        }
    }
}
