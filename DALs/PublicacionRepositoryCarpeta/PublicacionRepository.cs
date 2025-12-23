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
        public async Task<List<Publicacion>> ObtenerPublicacionesAsync()
        {
            var publicaciones = await _context.Publicaciones
                .Include(p => p.Comentarios).Include(p => p.Likes).ToListAsync();
            return publicaciones;
        }
        public async Task EliminarPublicacionAsync(int publicacionId)
        {
            var publicacionEncontrada = await _context.Publicaciones.FirstOrDefaultAsync(p => p.Id==publicacionId);
            _context.Publicaciones.Remove(publicacionEncontrada);
            await _context.SaveChangesAsync();
        }
        public async Task<Publicacion> ActualizarPublicacionAsync(Publicacion publicacion, int publicacionId)
        {
            var publicacionEncontrada = await _context.Publicaciones.FirstOrDefaultAsync(p => p.Id == publicacionId);

            publicacionEncontrada.Titulo = publicacion.Titulo;
            publicacionEncontrada.Contenido = publicacion.Contenido;
            publicacionEncontrada.FechaEdicion = DateTime.UtcNow; 
            
            return publicacionEncontrada;
        }
    }
}
