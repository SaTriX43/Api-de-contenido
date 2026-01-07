namespace API_de_Contenido.DALs.LikeRepositoryCarpeta
{
    public interface ILikeRepository
    {
        public void CrearLike(Like like);
        public void EliminarLike(Like like);
        public Task<Like?> ObtenerLikeAsync(int publicacionId, int usuarioId);
        public Task<int> NumeroDeLikesAsync(int publicacionId);
    }
}
