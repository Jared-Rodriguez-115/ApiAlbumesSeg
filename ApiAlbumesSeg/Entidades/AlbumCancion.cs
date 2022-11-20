namespace ApiAlbumesSeg.Entidades
{
    public class AlbumCancion
    {

        public int AlbumId { get; set; }

        public int CancionId { get; set; }

        public int Orden { get; set; }

        public Album Album { get; set; }

        public Cancion Cancion { get; set; }
    }
}
