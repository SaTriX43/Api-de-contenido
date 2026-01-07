using API_de_Contenido.DALs;
using API_de_Contenido.DALs.UsuarioRepositoryCarpeta;
using API_de_Contenido.DTOs.AutenticacionDtoCarpeta;
using API_de_Contenido.Models;
using API_de_Contenido.Models.Enums;
using API_de_Contenido.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_de_Contenido.Services
{
    public class AutenticacionService : IAutenticacionService
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        public AutenticacionService(
            IUsuarioRepository usuarioRepository,
            IConfiguration configuration,
            IUnidadDeTrabajo unidadDeTrabajo)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task<Result<string>> RegisterAsync(RegisterDto dto)
        {
            var emailNormalizado = dto.Email.Trim().ToLower();

            var existe = await _usuarioRepository.ObtenerUsuarioPorEmailAsync(emailNormalizado);
            if (existe != null)
                return Result<string>.Failure("El email ya está registrado");

            var usuario = new Usuario
            {
                Nombre = dto.Nombre.Trim(),
                Email = emailNormalizado,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Rol = UsuarioRol.User
            };

            var usuarioCreado = _usuarioRepository.CrearUsuario(usuario);
            await _unidadDeTrabajo.GuardarCambiosAsync();

            var token = GenerarJwt(usuarioCreado);
            return Result<string>.Success(token);
        }

        public async Task<Result<string>> LoginAsync(LoginDto dto)
        {
            var emailNormalizado = dto.Email.Trim().ToLower();

            var usuario = await _usuarioRepository.ObtenerUsuarioPorEmailAsync(emailNormalizado);
            if (usuario == null)
                return Result<string>.Failure("Credenciales inválidas");

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, usuario.PasswordHash))
                return Result<string>.Failure("Credenciales inválidas");

            if (usuario.Baneado)
                return Result<string>.Failure("Usuario baneado");

            var token = GenerarJwt(usuario);
            return Result<string>.Success(token);
        }

        private string GenerarJwt(Usuario usuario)
        {
            var jwt = _configuration.GetSection("Jwt");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Rol.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwt["Key"]!)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwt["Issuer"],
                audience: jwt["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    int.Parse(jwt["ExpireMinutes"]!)
                ),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
