using ApiAlbumesSeg.DTOs;
using ApiAlbumesSeg.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiAlbumesSeg.Controllers
{

    [ApiController]
    [Route("albumes")]
    public class AlbumesController: ControllerBase
    {

        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;


        public AlbumesController(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        [HttpGet] // /albumes
        public async Task<ActionResult<List<GetAlbumDTO>>> Get()
        {
            var albumes = await dbContext.Albumes.ToListAsync();
            return mapper.Map<List<GetAlbumDTO>>(albumes);
        }

        [HttpGet("{id:int}", Name = "obteneralbum")] //albumes/(id)

        public async Task<ActionResult<AlbumDTOConCanciones>> Get(int id)
        {
            
            var album = await dbContext.Albumes
                .Include(albumBD => albumBD.AlbumCancion)
                .ThenInclude(albumCancionDB => albumCancionDB.Cancion)
                .FirstOrDefaultAsync(albumBD => albumBD.Id == id);

            if (album == null)
            {
                return NotFound();
            }

            return mapper.Map<AlbumDTOConCanciones>(album);
        }

        [HttpGet("{nombre}")] //albumes/(nombre)

        public async Task<ActionResult<List<GetAlbumDTO>>> Get([FromRoute] string nombre)
        {
            var albumes = await dbContext.Albumes.Where(albumBD => albumBD.Nombre.Contains(nombre)).ToListAsync();

            return mapper.Map<List<GetAlbumDTO>>(albumes);
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AlbumDTO albumDto)
        {

            var existeAlbumMismoNombre = await dbContext.Albumes.AnyAsync(x => x.Nombre == albumDto.Nombre);

            if (existeAlbumMismoNombre)
            {
                return BadRequest($"Ya existe un album con el nombre {albumDto.Nombre}");
            }

            var album = mapper.Map<Album>(albumDto);

            dbContext.Add(album);
            await dbContext.SaveChangesAsync();
            
            var albumDTO = mapper.Map<GetAlbumDTO>(album);

            return CreatedAtRoute("obteneralbum", new { id = album.Id }, albumDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Album album, int id)
        {
            var exist = await dbContext.Albumes.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound();
            }

            dbContext.Update(album);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]

        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Albumes.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("El recurso no fue encontrado");
            }

            dbContext.Remove(new Album()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
