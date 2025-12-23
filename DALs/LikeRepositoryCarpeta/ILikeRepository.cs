namespace API_de_Contenido.DALs.LikeRepositoryCarpeta
{
    public interface ILikeRepository
    {
        public Task CrearLikeAsync(Like like);
        public Task EliminarLikeAsync(Like like);
        public Task<Like?> ObtenerLikeAsync(int publicacionId, int usuarioId);
        public Task<int> NumeroDeLikesAsync(int publicacionId);
    }
}
