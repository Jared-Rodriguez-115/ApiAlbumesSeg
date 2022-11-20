using System.ComponentModel.DataAnnotations;
using ApiAlbumesSeg.Validaciones;

namespace ApiAlbumesSeg.Entidades
{
    public class Album
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido obligatoriamente")]
        [StringLength(maximumLength: 20, ErrorMessage = "El campo {0} solo puede tener 20 caracteres")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido obligatoriamente")]
        [StringLength(maximumLength: 20, ErrorMessage = "El campo {0} solo puede tener 20 caracteres")]
        [PrimeraLetraMayuscula]
        public string Artista { get; set; }

        public List<AlbumCancion> AlbumCancion { get; set; }
    }
}
