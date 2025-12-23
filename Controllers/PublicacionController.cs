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
        [HttpPost("crear-publicacion")]
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
        [HttpGet("obtener-publicaciones")]
        public async Task<IActionResult> ObtenerPublicacionesAsync()
        {
            var publicaciones = await _publicacionService.ObtenerPublicacionesAsync();
            return Ok(new
            {
                success = true,
                valor = publicaciones.Value
            });
        }

        [Authorize]
        [HttpPost("{publicacionId}/crear-comentario")]
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

            if(publicacionId <= 0)
            {
                return BadRequest(new
                {
                    success = false,
                    error = "Su publicacion id no puede ser menor o igual a 0"
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
        [HttpPost("{publicacionId}/like")]
        public async Task<IActionResult> ToggleLikeAsync(int publicacionId)
        {
            if (publicacionId <= 0)
            {
                return BadRequest(new
                {
                    success = false,
                    error = "Su publicacion id no puede ser menor o igual a 0"
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
