using Microsoft.AspNetCore.Mvc;
using ApiAlbumesSeg.Entidades;
using Microsoft.EntityFrameworkCore;
using ApiAlbumesSeg.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;


namespace ApiAlbumesSeg.Controllers
{

    [ApiController]
    [Route("canciones")]
    public class CancionesController: ControllerBase
    {

        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public CancionesController (ApplicationDbContext context, IMapper mapper)
        {
            this.dbContext = context;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpGet("/listadoCancion")]

        public async Task<ActionResult<List<Cancion>>> GetAll()
        {
            return await dbContext.Canciones.ToListAsync();
        }

        [HttpGet("{id:int}", Name = "obtenerCancion")]

        public async Task<ActionResult<CancionDTOConAlbumes>> GetById(int id)
        {
            var cancion = await dbContext.Canciones
                .Include(cancionDB => cancionDB.AlbumCancion)
                .ThenInclude(albumCancionDB => albumCancionDB.Album)
                .Include(selloDB => selloDB.Sellos)
                .FirstOrDefaultAsync(x => x.Id == id);

            cancion.AlbumCancion = cancion.AlbumCancion.OrderBy(x => x.Orden).ToList();

            return mapper.Map<CancionDTOConAlbumes>(cancion);
        }

        [HttpPost]

        public async Task<ActionResult> Post(CancionCreacionDTO cancionCreacionDTO)
        {
            if(cancionCreacionDTO.AlbumesIds == null)
            {
                return BadRequest("No se puede crear una cancion sin Albumes");
            }

            var albumesIds = await dbContext.Albumes
                .Where(albumDB => cancionCreacionDTO.AlbumesIds.Contains(albumDB.Id)).Select(x => x.Id).ToListAsync();


            if(cancionCreacionDTO.AlbumesIds.Count != albumesIds.Count)
            {
                return BadRequest("No existe uno de los albumes enviados");
            }

            var cancion = mapper.Map<Cancion>(cancionCreacionDTO);

            OrdenarPorAlbumes(cancion);

            dbContext.Add(cancion);
            await dbContext.SaveChangesAsync();

            //var cancionDTO = mapper.Map<CancionDTO>(cancion);

            //return CreatedAtRoute("obtenerCancion", new {id = cancion.Id}, cancionDTO);
        }

        [HttpPut("{id:int}")]

        public async Task<ActionResult> Put(Cancion cancion,int id)
        {

            var exist = await dbContext.Canciones.AnyAsync(x => x.Id == id);
            
            //var cancionDB = await dbContext.Canciones
             //   .Include(x => x.AlbumCancion)
              //  .FirstOrDefaultAsync(x => x.Id == id);

            if(!exist)
            {
                return NotFound("La cancion especificada no existe");
            }

            if(cancion.Id != id)
            {
                return BadRequest("El id de la cancion no coincide con el establecido en la url");
            }

            //cancionDB = mapper.Map(cancionCreacionDTO, cancionDB);

            //OrdenarPorAlbumes(cancionDB);

            dbContext.Update(cancion);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]

        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Canciones.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound("El recurso no fue encontrado.");
            }

            dbContext.Remove(new Cancion {  Id = id });
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        private void OrdenarPorAlbumes(Cancion cancion)
        {
            if(cancion.AlbumCancion != null)
            {
                for (int i = 0; i < cancion.AlbumCancion.Count; i++)
                {
                    cancion.AlbumCancion[i].Orden = i;
                }
            }
        }


    }
}
