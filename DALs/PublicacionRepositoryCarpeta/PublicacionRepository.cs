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
        public async Task<List<Publicacion>> ObtenerPublicacionesAsync(int? autor, DateTime? fechaInicio, DateTime? fechaFinal)
        {
            var query = _context.Publicaciones.AsQueryable();

            if(autor.HasValue)
            {
                query = query.Where(q => q.UsuarioId == autor);
            }

            if(fechaInicio.HasValue)
            {
                query = query.Where(q => q.FechaCreacion >= fechaInicio);
            }

            if (fechaFinal.HasValue)
            {
                query = query.Where(q => q.FechaCreacion <= fechaFinal);
            }



            query = query.Include(p => p.Comentarios).Include(p => p.Likes).OrderByDescending(p => p.Likes.Count);

            
            return await query.ToListAsync();
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
