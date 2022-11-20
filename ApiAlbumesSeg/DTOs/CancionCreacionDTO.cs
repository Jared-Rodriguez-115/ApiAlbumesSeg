using ApiAlbumesSeg.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace ApiAlbumesSeg.DTOs
{
    public class CancionCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido obligatoriamente")]
        [StringLength(maximumLength: 20, ErrorMessage = "El campo {0} solo puede tener 20 caracteres")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido obligatoriamente")]
        [StringLength(maximumLength: 20, ErrorMessage = "El campo {0} solo puede tener 20 caracteres")]
        [PrimeraLetraMayuscula]
        public string Compositor { get; set; }

        public DateTime FechaPublicacion { get; set; }

        public List<int> AlbumesIds { get; set; }
    }
}
