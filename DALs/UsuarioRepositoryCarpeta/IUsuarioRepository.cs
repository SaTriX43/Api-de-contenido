using API_de_Contenido.Models;

namespace API_de_Contenido.DALs.UsuarioRepositoryCarpeta

{
    public interface IUsuarioRepository
    {
        Task<Usuario?> ObtenerUsuarioPorEmailAsync(string email);
        Task<Usuario?> ObtenerUsuarioPorIdAsync(int id);
        Usuario CrearUsuario(Usuario usuario);
    }
}
