namespace API_de_Contenido.DALs.PublicacionRepositoryCarpeta
{
    public interface IPublicacionRepository
    {
        public Task<Publicacion> CrearPublicacionAsync(Publicacion publicacion);
        public Task<Publicacion?> ObtenerPublicacionPorIdAsync(int publicacionId);
        public Task<Publicacion> ActualizarPublicacionAsync(Publicacion publicacion, int publicacionId);
        public Task EliminarPublicacionAsync(int publicacionId);
        public Task<List<Publicacion>> ObtenerPublicacionesAsync();
    }
}
