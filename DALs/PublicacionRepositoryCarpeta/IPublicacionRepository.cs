namespace API_de_Contenido.DALs.PublicacionRepositoryCarpeta
{
    public interface IPublicacionRepository
    {
        public Publicacion CrearPublicacion(Publicacion publicacion);
        public Task<Publicacion?> ObtenerPublicacionPorIdAsync(int publicacionId);
        public Task<List<Publicacion>> ObtenerPublicacionesAsync(int? autor, DateTime? fechaInicio, DateTime? fechaFinal);
    }
}
