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
        }
    }
}
