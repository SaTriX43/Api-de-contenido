namespace API_de_Contenido.DALs.PublicacionRepositoryCarpeta
{
    public interface IPublicacionRepository
    {
        public Task<Publicacion> CrearPublicacionAsync(Publicacion publicacion);
        public Task<Publicacion?> ObtenerPublicacionPorIdAsync(int publicacionId);
    }
}
