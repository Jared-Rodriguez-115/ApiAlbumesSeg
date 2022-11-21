using Microsoft.AspNetCore.Mvc;
using ApiAlbumesSeg.Entidades;
using Microsoft.EntityFrameworkCore;

namespace ApiAlbumesSeg.Controllers
{

    [ApiController]
    [Route("api/canciones")]
    public class CancionesController: ControllerBase
    {

        private readonly ApplicationDbContext dbContext;

        public CancionesController (ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        [HttpGet]

        public async Task<ActionResult<List<Cancion>>> GetAll()
        {
            return await dbContext.Canciones.ToListAsync();
        }

        [HttpGet("{id:int}")]

        public async Task<ActionResult<Cancion>> GetById(int id)
        {
            return await dbContext.Canciones.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]

        public async Task<ActionResult> Post(Cancion cancion)
        {
            var existeAlbum = await dbContext.Albumes.AnyAsync(x => x.Id == cancion.AlbumId);

            if (!existeAlbum)
            {
                return BadRequest($"No existe el album con el id: {cancion.AlbumId}");
            }

            dbContext.Add(cancion);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]

        public async Task<ActionResult> Put(Cancion cancion, int id)
        {
            var exist = await dbContext.Canciones.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound("La cancion especificada no existe. ");
            }

            if(cancion.Id != id)
            {
                return BadRequest("El id de la cancion no coincide con el establecido en la url");
            }

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
    }
}
