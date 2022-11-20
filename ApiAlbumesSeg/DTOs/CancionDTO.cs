namespace ApiAlbumesSeg.DTOs
{
    public class CancionDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Compositor { get; set; }

        public DateTime? FechaPublicacion { get; set; }

        public List<SelloDTO> Sellos { get; set; }
    }
}
