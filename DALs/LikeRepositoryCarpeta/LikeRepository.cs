using API_de_Contenido.Models;
using Microsoft.EntityFrameworkCore;

namespace API_de_Contenido.DALs.LikeRepositoryCarpeta
{
    public class LikeRepository : ILikeRepository
    {
        private readonly ApplicationDbContext _context;

        public LikeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CrearLike(Like like)
        {
            _context.Likes.Add(like);
        }

        public void EliminarLike(Like like)
        {
            _context.Likes.Remove(like);
        }

        public async Task<Like?> ObtenerLikeAsync(int publicacionId, int usuarioId)
        {
            var likeEncontrado = await _context.Likes.FirstOrDefaultAsync(l => l.PublicacionId == publicacionId && l.UsuarioId ==  usuarioId);
            return likeEncontrado;
        }

        public async Task<int> NumeroDeLikesAsync(int publicacionId)
        {
            int likesPorPublicacion = await _context.Likes.CountAsync(l => l.PublicacionId == publicacionId);
            return likesPorPublicacion;
        }
    }
}
