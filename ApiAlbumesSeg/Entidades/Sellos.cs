using Microsoft.AspNetCore.Identity;

namespace ApiAlbumesSeg.Entidades
{
    public class Sellos
    {

        public int Id { get; set; }

        public string Empresa { get; set; }

        public int CancionId { get; set; }

        public Cancion Cancion { get; set; }

        public string UsuarioId { get; set; }

        public IdentityUser Usuario { get; set; }
    }
}
