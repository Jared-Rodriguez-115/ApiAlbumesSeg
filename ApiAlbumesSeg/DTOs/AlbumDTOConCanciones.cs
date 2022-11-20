namespace ApiAlbumesSeg.DTOs
{
    public class AlbumDTOConCanciones: GetAlbumDTO 
    {
        public List<CancionDTO> Canciones { get; set; }
    }
}
