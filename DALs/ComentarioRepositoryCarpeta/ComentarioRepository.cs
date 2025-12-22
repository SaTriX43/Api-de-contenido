using API_de_Contenido.Models;

namespace API_de_Contenido.DALs.ComentarioRepositoryCarpeta
{
    public class ComentarioRepository : IComentarioRepository
    {
        private readonly ApplicationDbContext _context;

        public ComentarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Comentario> CrearComentarioAsync(Comentario comentario)
        {
            _context.Comentarios.Add(comentario);
            await _context.SaveChangesAsync();
            return comentario;
        }
    }
}
