namespace ApiAlbumesSeg.Entidades
{
    public class Album
    {

        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Artista { get; set; }

        public List<Cancion> canciones { get; set; }
    }
}
