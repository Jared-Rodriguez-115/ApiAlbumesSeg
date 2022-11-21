using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ApiAlbumesSeg.Entidades;
using Microsoft.EntityFrameworkCore;


namespace ApiAlbumesSeg
{
    public class ApplicationDbContext: DbContext
    {

        public ApplicationDbContext(DbContextOptions options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AlbumCancion>()
                .HasKey(al => new { al.AlbumId, al.CancionId });
        }

        public DbSet<Album> Albumes { get; set; }

        public DbSet<Cancion> Canciones { get; set; }

        public DbSet<Sellos> Sellos { get; set; }

        public DbSet<AlbumCancion> AlbumCancion { get; set; }
    }
}
