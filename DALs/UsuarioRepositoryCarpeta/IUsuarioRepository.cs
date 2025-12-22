using API_de_Contenido.Models;

namespace API_de_Contenido.DALs.UsuarioRepositoryCarpeta

{
    public interface IUsuarioRepository
    {
        Task<Usuario?> ObtenerPorEmailAsync(string email);
        Task<Usuario?> ObtenerPorIdAsync(int id);
        Task<Usuario> CrearAsync(Usuario usuario);
    }
}
