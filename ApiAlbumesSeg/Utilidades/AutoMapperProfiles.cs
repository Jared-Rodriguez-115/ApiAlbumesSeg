using ApiAlbumesSeg.DTOs;
using ApiAlbumesSeg.Entidades;
using AutoMapper;

namespace ApiAlbumesSeg.Utilidades
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AlbumDTO, Album>();
            CreateMap<Album, GetAlbumDTO>();
            CreateMap<Album, AlbumDTOConCanciones>()
                .ForMember(albumDTO => albumDTO.Canciones, opciones => opciones.MapFrom(MapAlbumDTOCanciones));
            CreateMap<CancionCreacionDTO, Cancion>()
                .ForMember(cancion => cancion.AlbumCancion, opciones => opciones.MapFrom(MapAlbumCancion));
            CreateMap<Cancion, CancionDTO>();
            CreateMap<Cancion, CancionDTOConAlbumes>()
                .ForMember(cancionDTO => cancionDTO.Albumes, opciones => opciones.MapFrom(MapCancionDTOAlbumes));
            CreateMap<SelloCreacionDTO, Sellos>();
            CreateMap<Sellos, SelloDTO>();

        }

        private List<CancionDTO> MapAlbumDTOCanciones(Album album, GetAlbumDTO getAlbumDTO)
        {
            var result = new List<CancionDTO>();

            if(album.AlbumCancion == null) { return result; }

            foreach(var albumCancion in album.AlbumCancion)
            {
                result.Add(new CancionDTO()
                {
                    Id = albumCancion.CancionId,
                    Nombre = albumCancion.Cancion.Nombre
                });
            }

            return result;
        }

        private List<GetAlbumDTO> MapCancionDTOAlbumes(Cancion cancion, CancionDTO cancionDTO)
        {
            var result = new List<GetAlbumDTO>();

            if(cancion.AlbumCancion == null)
            {
                return result;
            }

            foreach (var albumCancion in cancion.AlbumCancion)
            {
                result.Add(new GetAlbumDTO()
                {
                    Id = albumCancion.AlbumId,
                    Nombre = albumCancion.Album.Nombre
                });
            }

            return result;
        }

        private List<AlbumCancion> MapAlbumCancion(CancionCreacionDTO cancionCreacionDTO, Cancion cancion)
        {
            var resultado = new List<AlbumCancion>();

            if(cancionCreacionDTO.AlbumesIds == null) { return resultado; }

            foreach (var albumId in cancionCreacionDTO.AlbumesIds)
            {
                resultado.Add(new AlbumCancion() { AlbumId = albumId });
                
            }

            return resultado;
        }
    }
}
