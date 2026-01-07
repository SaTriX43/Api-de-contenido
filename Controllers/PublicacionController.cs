using API_de_Contenido.DTOs.ComentarioDtoCarpeta;
using API_de_Contenido.DTOs.PublicacionDtoCarpeta;
using API_de_Contenido.Services.ComentarioServiceCarpeta;
using API_de_Contenido.Services.LikeServiceCarpeta;
using API_de_Contenido.Services.PublicacionServiceCarpeta;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API_de_Contenido.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicacionController : ControllerBase
    {
        private readonly IPublicacionService _publicacionService;
        private readonly IComentarioService _comentarioService;
        private readonly ILikeService _likeService;

        public PublicacionController(IPublicacionService publicacionService, IComentarioService comentarioService, ILikeService likeService)
        {
            _publicacionService = publicacionService;
            _comentarioService = comentarioService;
            _likeService = likeService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CrearPublicacionAsync([FromBody] PublicacionCrearDto publicacionCrearDto)
        {
            if (!ModelState.IsValid) { 
                return BadRequest(new
                {
                    success = false,
                    error = ModelState
                });
            }

            var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(!int.TryParse(usuarioIdClaim, out int id)) {
                return BadRequest(new
                {
                    success = false,
                    error = "El usuario id debe de ser un numero"
                });
            }

            var publicacionCreada = await _publicacionService.CrearPublicacionAsync(publicacionCrearDto, id);

            if(publicacionCreada.IsFailure)
            {
                return BadRequest(new
                {
                    success = false,
                    error = publicacionCreada.Error
                });
            }


            return Ok(new
            {
                success = true,
                valor = publicacionCreada.Value
            });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ObtenerPublicacionesAsync([FromQuery] int? autor,[FromQuery] DateTime? fechaInicio,[FromQuery] DateTime? fechaFinal)
        {
            var publicaciones = await _publicacionService.ObtenerPublicacionesAsync(autor,fechaInicio,fechaFinal);
            return Ok(new
            {
                success = true,
                valor = publicaciones.Value
            });
        }

        [Authorize]
        [HttpPost("{publicacionId}/comentario")]
        public async Task<IActionResult> CrearComentarioAsync([FromBody] ComentarioCrearDto comentarioCrearDto, int publicacionId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    error = ModelState
                });
            }

            var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(usuarioIdClaim, out int usuarioId))
            {
                return BadRequest(new
                {
                    success = false,
                    error = "El usuario id debe de ser un numero"
                });
            }

            var comentarioCreado = await _comentarioService.CrearComentarioAsync(comentarioCrearDto, publicacionId ,usuarioId);

            if (comentarioCreado.IsFailure)
            {
                return BadRequest(new
                {
                    success = false,
                    error = comentarioCreado.Error
                });
            }


            return Ok(new
            {
                success = true,
                valor = comentarioCreado.Value
            });
        }

        [Authorize]
        [HttpPut("{publicacionId}")]
        public async Task<IActionResult> ActualizarPublicacionAsync([FromBody] PublicacionCrearDto publicacionActualizarDto, int publicacionId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    error = ModelState
                });
            }

            var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(usuarioIdClaim, out int usuarioId))
            {
                return BadRequest(new
                {
                    success = false,
                    error = "El usuario id debe de ser un numero"
                });
            }

            var publicacionActualizada = await _publicacionService.ActualizarPublicacionAsync(publicacionActualizarDto, publicacionId, usuarioId);

            if (publicacionActualizada.IsFailure)
            {
                return BadRequest(new
                {
                    success = false,
                    error = publicacionActualizada.Error
                });
            }


            return Ok(new
            {
                success = true,
                valor = publicacionActualizada.Value
            });
        }

        [Authorize]
        [HttpDelete("{publicacionId}")]
        public async Task<IActionResult> EliminarPublicacionAsync(int publicacionId)
        {
            var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(usuarioIdClaim, out int usuarioId))
            {
                return BadRequest(new
                {
                    success = false,
                    error = "El usuario id debe de ser un numero"
                });
            }

            var publicacionEliminada = await _publicacionService.EliminarPublicacionAsync(publicacionId, usuarioId);

            if (publicacionEliminada.IsFailure)
            {
                return BadRequest(new
                {
                    success = false,
                    error = publicacionEliminada.Error
                });
            }


            return NoContent();
        }

        [Authorize]
        [HttpPost("{publicacionId}/like")]
        public async Task<IActionResult> ToggleLikeAsync(int publicacionId)
        {
            var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(usuarioIdClaim, out int usuarioId))
            {
                return BadRequest(new
                {
                    success = false,
                    error = "El usuario id debe de ser un numero"
                });
            }

            var toggleLike = await _likeService.ToggleLikeAsync(publicacionId, usuarioId); 

            if (toggleLike.IsFailure)
            {
                return BadRequest(new
                {
                    success = false,
                    error = toggleLike.Error
                });
            }


            return Ok(new
            {
                success = true,
                valor = toggleLike.Value
            });
        }

    }
}
