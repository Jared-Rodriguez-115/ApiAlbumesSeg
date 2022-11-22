using System.ComponentModel.DataAnnotations;

namespace ApiAlbumesSeg.DTOs
{
    public class EditarAdminDTO
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
