using API_de_Contenido.DALs.PublicacionRepositoryCarpeta;
using API_de_Contenido.DALs.UsuarioRepositoryCarpeta;
using API_de_Contenido.DTOs.ComentarioDtoCarpeta;
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

            var tituloNormalizado = publicacionCrearDto.Titulo.Trim();

            if(tituloNormalizado == "")
            {
                return Result<PublicacionDto>.Failure("Su titulo no puede estar vacio");
            }

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

        public async Task<Result<List<PublicacionDto>>> ObtenerPublicacionesAsync()
        {
            var publicaciones = await _publicacionRepository.ObtenerPublicacionesAsync();

            var publicacionesDto = publicaciones.Select(p => new PublicacionDto
            {
                Id = p.Id,
                Contenido = p.Contenido,
                Eliminado = p.Eliminado,
                FechaCreacion = p.FechaCreacion,
                FechaEdicion = p.FechaEdicion,
                Titulo = p.Titulo,
                UsuarioId = p.UsuarioId,
                ComentarioDtos = p.Comentarios.Select(c => new ComentarioDto
                {
                    Id = c.Id,
                    Contenido = c.Contenido,
                    Eliminado = c.Eliminado,
                    FechaCreacion = c.FechaCreacion,
                    PublicacionId = c.PublicacionId,    
                    UsuarioId = c.UsuarioId
                }).ToList(),
                Likes = p.Likes.Count
            }).ToList();

            return Result<List<PublicacionDto>>.Success(publicacionesDto);
        }
        public async Task<Result> EliminarPublicacionAsync(int publicacionId, int usuarioId)
        {
            var usuarioExiste = await _usuarioRepository.ObtenerPorIdAsync(usuarioId);

            if (usuarioExiste == null)
            {
                return Result.Failure($"Su usuario con id = {usuarioId} no existe");
            }

            if (usuarioExiste.Baneado)
            {
                return Result.Failure($"Su usuario con id = {usuarioId} esta baneado");
            }

            var publicacionExiste = await _publicacionRepository.ObtenerPublicacionPorIdAsync(publicacionId);

            if (publicacionExiste == null)
            {
                return Result.Failure($"Su publicacion con id = {publicacionId} no existe");
            }

            if (publicacionExiste.Eliminado)
            {
                return Result.Failure($"Su publicacion con id = {publicacionId} esta eliminada ");
            }

            if (publicacionExiste.UsuarioId != usuarioId)
            {
                return Result.Failure("Solo puede actualizar una publicacion que sea suya");
            }

            await _publicacionRepository.EliminarPublicacionAsync(publicacionId);

            return Result.Success();
        }
        public async Task<Result<PublicacionDto>> ActualizarPublicacionAsync(PublicacionCrearDto publicacionActualizarDto, int publicacionId, int usuarioId)
        {
            var usuarioExiste = await _usuarioRepository.ObtenerPorIdAsync(usuarioId);

            if (usuarioExiste == null)
            {
                return Result<PublicacionDto>.Failure($"Su usuario con id = {usuarioId} no existe");
            }

            if(usuarioExiste.Baneado )
            {
                return Result<PublicacionDto>.Failure($"Su usuario con id = {usuarioId} esta baneado");
            }

            var publicacionExiste = await _publicacionRepository.ObtenerPublicacionPorIdAsync(publicacionId);

            if (publicacionExiste == null)
            {
                return Result<PublicacionDto>.Failure($"Su publicacion con id = {publicacionId} no existe");
            }

            if (publicacionExiste.Eliminado)
            {
                return Result<PublicacionDto>.Failure($"Su publicacion con id = {publicacionId} esta eliminada ");
            }

            if(publicacionExiste.UsuarioId != usuarioId)
            {
                return Result<PublicacionDto>.Failure("Solo puede actualizar una publicacion que sea suya");
            }

            var tituloNormalizado = publicacionActualizarDto.Titulo.Trim();

            if (tituloNormalizado == "")
            {
                return Result<PublicacionDto>.Failure("Su titulo no puede estar vacio");
            }

            var publicacionModel = new Publicacion
            {
                Titulo = tituloNormalizado,
                Contenido = publicacionActualizarDto.Contenido
            };

            var publicacionActualizar = await _publicacionRepository.ActualizarPublicacionAsync(publicacionModel, publicacionId);

            var publicacionDto = new PublicacionDto
            {
                Id = publicacionActualizar.Id,
                Contenido = publicacionActualizar.Contenido,
                Eliminado = publicacionActualizar.Eliminado,
                FechaCreacion = publicacionActualizar.FechaCreacion,
                FechaEdicion = publicacionActualizar.FechaEdicion,
                Titulo = publicacionActualizar.Titulo,
                UsuarioId = publicacionActualizar.UsuarioId
            };

            return Result<PublicacionDto>.Success(publicacionDto);
        }
    }
}
