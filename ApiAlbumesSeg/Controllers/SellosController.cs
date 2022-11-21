using ApiAlbumesSeg.DTOs;
using ApiAlbumesSeg.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiAlbumesSeg.Controllers
{

    [ApiController]
    [Route("sellos/{selloId:int}/sellos")]
    public class SellosController: ControllerBase
    {

        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public SellosController(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<SelloDTO>>> Get(int cancionId)
        {
            var existeCancion = await dbContext.Canciones.AnyAsync(cancionDB => cancionDB.Id == cancionId);

            if (!existeCancion)
            {
                return NotFound();
            }

            var sellos = await dbContext.Sellos.Where(selloDB => selloDB.CancionId == cancionId).ToListAsync();

            return mapper.Map<List<SelloDTO>>(sellos);
        }

        [HttpPost]
        public async Task<ActionResult> Post(int cancionId, SelloCreacionDTO selloCreacionDTO)
        {
            var existeCancion = await dbContext.Canciones.AnyAsync(cancionDB => cancionDB.Id == cancionId);

            if (!existeCancion)
            {
                return NotFound();
            }

            var sello = mapper.Map<Sellos>(selloCreacionDTO);
            sello.CancionId = cancionId;
            dbContext.Add(sello);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
