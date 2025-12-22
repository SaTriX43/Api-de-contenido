using Microsoft.EntityFrameworkCore;

namespace API_de_Contenido.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Publicacion> Publicaciones { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Like> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasIndex(u => u.Email)
                      .IsUnique();

                entity.Property(u => u.Nombre)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(u => u.Email)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.Property(u => u.PasswordHash)
                      .IsRequired();
            });

            modelBuilder.Entity<Publicacion>(entity =>
            {
                entity.Property(p => p.Titulo)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(p => p.Contenido)
                      .IsRequired();

                entity.HasOne(p => p.Usuario)
                      .WithMany(u => u.Publicaciones)
                      .HasForeignKey(p => p.UsuarioId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Comentario>(entity =>
            {
                entity.Property(c => c.Contenido)
                      .IsRequired()
                      .HasMaxLength(500);

                entity.HasOne(c => c.Usuario)
                      .WithMany(u => u.Comentarios)
                      .HasForeignKey(c => c.UsuarioId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.Publicacion)
                      .WithMany(p => p.Comentarios)
                      .HasForeignKey(c => c.PublicacionId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Like>(entity =>
            {
                entity.HasIndex(l => new { l.UsuarioId, l.PublicacionId })
                      .IsUnique();

                entity.HasOne(l => l.Usuario)
                      .WithMany(u => u.Likes)
                      .HasForeignKey(l => l.UsuarioId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(l => l.Publicacion)
                      .WithMany(p => p.Likes)
                      .HasForeignKey(l => l.PublicacionId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
