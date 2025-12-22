using API_de_Contenido.DTOs.AutenticacionDtoCarpeta;
using API_de_Contenido.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_de_Contenido.Controllers
{
    [ApiController]
    [Route("api/autenticacion")]
    public class AutenticacionController : ControllerBase
    {
        private readonly IAutenticacionService _authService;

        public AutenticacionController(IAutenticacionService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    error = ModelState
                });
            }
            var result = await _authService.RegisterAsync(dto);

            if(result.IsFailure)
            {
                return BadRequest(new
                {
                    success = false,
                    error = result.Error
                });
            }

            return Ok(new
            {
                success = true,
                token = result.Value
            }); 
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    error = ModelState
                });
            }

            var result = await _authService.LoginAsync(dto);

            if (result.IsFailure)
            {
                return BadRequest(new
                {
                    success = false,
                    error = result.Error
                });
            }
            return Ok(new
            {
                success = true,
                token = result.Value
            });
        }
    }
}
