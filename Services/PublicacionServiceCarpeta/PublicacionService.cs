using API_de_Contenido.DALs.PublicacionRepositoryCarpeta;
using API_de_Contenido.DALs.UsuarioRepositoryCarpeta;
using API_de_Contenido.DTOs.PublicacionDtoCarpeta;
using API_de_Contenido.Models;

namespace API_de_Contenido.Services.PublicacionServiceCarpeta
{
    public class PublicacionService : IPublicacionService
    {
        private readonly IPublicacionRepository _publicacionRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public PublicacionService(IPublicacionRepository publicacionRepository, IUsuarioRepository usuarioRepository)
        {
            _publicacionRepository = publicacionRepository;
            _usuarioRepository = usuarioRepository;
        }


        public async Task<Result<PublicacionDto>> CrearPublicacionAsync(PublicacionCrearDto publicacionCrearDto, int usuarioId)
        {
            var usuarioExiste = await _usuarioRepository.ObtenerPorIdAsync(usuarioId);

            if(usuarioExiste == null)
            {
                return Result<PublicacionDto>.Failure($"Su usuario con id = {usuarioId} no existe");
            }

            var tituloNormalizado = publicacionCrearDto.Titulo.Trim().ToLower();

            var publicacionModel = new Publicacion
            {
                Titulo = tituloNormalizado,
                Contenido = publicacionCrearDto.Contenido,
                FechaCreacion = DateTime.Now,
                UsuarioId = usuarioId,
            };

            var publicacionCreada = await _publicacionRepository.CrearPublicacionAsync(publicacionModel);

            var publicacionDto = new PublicacionDto
            {
                Id = publicacionModel.Id,
                Contenido = publicacionCreada.Contenido,
                FechaCreacion = publicacionCreada.FechaCreacion,
                Eliminado = publicacionCreada.Eliminado,
                FechaEdicion = publicacionCreada.FechaEdicion,
                Titulo = publicacionCreada.Titulo,
                UsuarioId = publicacionCreada.UsuarioId
            };

            return Result<PublicacionDto>.Success(publicacionDto);
        }
    }
}
