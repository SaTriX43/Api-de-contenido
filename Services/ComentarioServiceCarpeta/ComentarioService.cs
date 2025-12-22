using API_de_Contenido.DALs.ComentarioRepositoryCarpeta;
using API_de_Contenido.DALs.PublicacionRepositoryCarpeta;
using API_de_Contenido.DALs.UsuarioRepositoryCarpeta;
using API_de_Contenido.DTOs.ComentarioDtoCarpeta;
using API_de_Contenido.Models;

namespace API_de_Contenido.Services.ComentarioServiceCarpeta
{
    public class ComentarioService : IComentarioService
    {
        private readonly IComentarioRepository _comentarioRepository;
        private readonly IPublicacionRepository _publicacionRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public ComentarioService(IComentarioRepository comentarioRepository, IPublicacionRepository publicacionRepository, IUsuarioRepository usuarioRepository)
        {
            _comentarioRepository = comentarioRepository;
            _publicacionRepository = publicacionRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Result<ComentarioDto>> CrearComentarioAsync(ComentarioCrearDto comentarioCrearDto, int publicacionId, int usuarioId)
        {
            var publicacionExiste = await _publicacionRepository.ObtenerPublicacionPorIdAsync(publicacionId);

            if (publicacionExiste == null) {
                return Result<ComentarioDto>.Failure($"Su publicacion con id = {publicacionId} no existe");
            }

            if(publicacionExiste.Eliminado)
            {
                return Result<ComentarioDto>.Failure($"Su publicacion con id = {publicacionId} esta eliminada");
            }

            var usuarioExiste = await _usuarioRepository.ObtenerPorIdAsync(usuarioId);

            if (usuarioExiste == null) {
                return Result<ComentarioDto>.Failure($"Su usuario con id = {usuarioId} no existe");
            }

            if(usuarioExiste.Baneado)
            {
                return Result<ComentarioDto>.Failure($"Su usuario con id = {usuarioId} esta baneado");
            }

            var comentarioNormalizado = comentarioCrearDto.Contenido.Trim();

            if (comentarioNormalizado == "" || comentarioNormalizado == null)
            {
                return Result<ComentarioDto>.Failure($"Su comentario no puede estar vacio o ser null");
            }

            var comentarioModel = new Comentario
            {
                Contenido = comentarioNormalizado,
                FechaCreacion = DateTime.UtcNow,
                PublicacionId = publicacionId,
                UsuarioId = usuarioId,
                Eliminado = false,
            };

            var comentarioCreado = await _comentarioRepository.CrearComentarioAsync(comentarioModel);

            var comentarioDto = new ComentarioDto
            {
                Id = comentarioModel.Id,
                PublicacionId = publicacionId,
                UsuarioId = usuarioId,
                Contenido = comentarioCreado.Contenido,
                Eliminado = comentarioCreado.Eliminado,
                FechaCreacion = comentarioCreado.FechaCreacion
            };

            return Result<ComentarioDto>.Success(comentarioDto);
        }
    }
}
