namespace ApiAlbumesSeg.Entidades
{
    public class Cancion
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Compositor { get; set; }

        public int AlbumId { get; set; }

        public Album Album { get; set; }
    }
}
