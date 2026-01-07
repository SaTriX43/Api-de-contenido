using API_de_Contenido.DALs;
using API_de_Contenido.DALs.PublicacionRepositoryCarpeta;
using API_de_Contenido.DALs.UsuarioRepositoryCarpeta;
using API_de_Contenido.DTOs.ComentarioDtoCarpeta;
using API_de_Contenido.DTOs.PublicacionDtoCarpeta;
using API_de_Contenido.Models;

namespace API_de_Contenido.Services.PublicacionServiceCarpeta
{
    public class PublicacionService : IPublicacionService
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IPublicacionRepository _publicacionRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public PublicacionService(IPublicacionRepository publicacionRepository, IUsuarioRepository usuarioRepository,IUnidadDeTrabajo unidadDeTrabajo)
        {
            _publicacionRepository = publicacionRepository;
            _usuarioRepository = usuarioRepository;
            _unidadDeTrabajo = unidadDeTrabajo;
        }


        public async Task<Result<PublicacionDto>> CrearPublicacionAsync(PublicacionCrearDto publicacionCrearDto, int usuarioId)
        {
            var usuarioExiste = await _usuarioRepository.ObtenerUsuarioPorIdAsync(usuarioId);

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

            var publicacionCreada = _publicacionRepository.CrearPublicacion(publicacionModel);

            await _unidadDeTrabajo.GuardarCambiosAsync();

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

        public async Task<Result<List<PublicacionDto>>> ObtenerPublicacionesAsync(int? autor, DateTime? fechaInicio, DateTime? fechaFinal)
        {
            var publicaciones = await _publicacionRepository.ObtenerPublicacionesAsync(autor,fechaInicio,fechaFinal);

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
            if(publicacionId <= 0)
            {
                return Result.Failure("Su publicacionId no puede ser menor o igual a 0");
            }

            var usuarioExiste = await _usuarioRepository.ObtenerUsuarioPorIdAsync(usuarioId);

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

            publicacionExiste.Eliminado = true;
            await _unidadDeTrabajo.GuardarCambiosAsync();

            return Result.Success();
        }
        public async Task<Result<PublicacionDto>> ActualizarPublicacionAsync(PublicacionCrearDto publicacionActualizarDto, int publicacionId, int usuarioId)
        {
            if (publicacionId <= 0)
            {
                return Result<PublicacionDto>.Failure("Su publicacionId no puede ser menor o igual a 0");
            }

            var usuarioExiste = await _usuarioRepository.ObtenerUsuarioPorIdAsync(usuarioId);

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


            publicacionExiste.Titulo = tituloNormalizado;
            publicacionExiste.Contenido = publicacionActualizarDto.Contenido;

            await _unidadDeTrabajo.GuardarCambiosAsync();


            var publicacionDto = new PublicacionDto
            {
                Id = publicacionExiste.Id,
                Contenido = publicacionExiste.Contenido,
                Eliminado = publicacionExiste.Eliminado,
                FechaCreacion = publicacionExiste.FechaCreacion,
                FechaEdicion = publicacionExiste.FechaEdicion,
                Titulo = publicacionExiste.Titulo,
                UsuarioId = publicacionExiste.UsuarioId
            };

            return Result<PublicacionDto>.Success(publicacionDto);
        }
    }
}
