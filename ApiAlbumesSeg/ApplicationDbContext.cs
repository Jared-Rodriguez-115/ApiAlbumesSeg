using ApiAlbumesSeg.Entidades;
using Microsoft.EntityFrameworkCore;

namespace ApiAlbumesSeg
{
    public class ApplicationDbContext: DbContext
    {

        public ApplicationDbContext(DbContextOptions options): base(options)
        {

        }

        public DbSet<Album> Albumes { get; set; }

        public DbSet<Cancion> Canciones { get; set; }
    }
}
