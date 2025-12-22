using API_de_Contenido.Models;
using Microsoft.EntityFrameworkCore;

namespace API_de_Contenido.DALs.PublicacionRepositoryCarpeta
{
    public class PublicacionRepository : IPublicacionRepository
    {
        private readonly ApplicationDbContext _context;

        public PublicacionRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<Publicacion> CrearPublicacionAsync(Publicacion publicacion)
        {
            _context.Publicaciones.Add(publicacion);
            await _context.SaveChangesAsync();
            return publicacion;
        }
        public async Task<Publicacion?> ObtenerPublicacionPorIdAsync(int publicacionId)
        {
            var publicacionEncontrada = await _context.Publicaciones.FirstOrDefaultAsync(p => p.Id == publicacionId);
            return publicacionEncontrada;
        }
    }
}
