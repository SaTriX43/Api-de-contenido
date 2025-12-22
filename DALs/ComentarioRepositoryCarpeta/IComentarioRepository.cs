namespace API_de_Contenido.DALs.ComentarioRepositoryCarpeta
{
    public interface IComentarioRepository
    {
        public Task<Comentario> CrearComentarioAsync(Comentario comentario);
    }
}
