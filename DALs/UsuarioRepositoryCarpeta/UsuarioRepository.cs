using API_de_Contenido.DALs.UsuarioRepositoryCarpeta;
using API_de_Contenido.Models;
using Microsoft.EntityFrameworkCore;

namespace API_de_Contenido.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> ObtenerUsuarioPorEmailAsync(string email)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Usuario?> ObtenerUsuarioPorIdAsync(int id)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public Usuario CrearUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            return usuario;
        }
    }
}
