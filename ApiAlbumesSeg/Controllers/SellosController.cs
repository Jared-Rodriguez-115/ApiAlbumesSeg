using ApiAlbumesSeg.DTOs;
using ApiAlbumesSeg.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.HttpSys;

namespace ApiAlbumesSeg.Controllers
{

    [ApiController]
    [Route("sellos/{selloId:int}/sellos")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SellosController: ControllerBase
    {

        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;

        public SellosController(ApplicationDbContext dbContext, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.userManager = userManager;
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

        [HttpGet("{id:int}", Name = "obtenerSello")]

        public async Task<ActionResult<SelloDTO>> GetById(int id)
        {
            var sello = await dbContext.Sellos.FirstOrDefaultAsync(selloDB => selloDB.Id == id);

            if(sello == null)
            {
                return NotFound();
            }

            return mapper.Map<SelloDTO>(sello);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Post(int cancionId, SelloCreacionDTO selloCreacionDTO)
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();

            var email = emailClaim.Value;

            var usuario = await userManager.FindByEmailAsync(email);

            var usuarioId = usuario.Id;

            var existeCancion = await dbContext.Canciones.AnyAsync(cancionDB => cancionDB.Id == cancionId);

            if (!existeCancion)
            {
                return NotFound();
            }

            var sello = mapper.Map<Sellos>(selloCreacionDTO);
            sello.CancionId = cancionId;
            sello.UsuarioId = usuarioId;
            dbContext.Add(sello);
            await dbContext.SaveChangesAsync();

            var selloDTO = mapper.Map<SelloDTO>(sello);

            return CreatedAtRoute("obtenerSello", new {id = sello.Id, cancionId = cancionId}, selloDTO);
        }

        [HttpPut("{id:int}")]

        public async Task<ActionResult> Put(int cancionId, int id, SelloCreacionDTO selloCreacionDTO)
        {
            var existeCancion = await dbContext.Canciones.AnyAsync(cancionDB => cancionDB.Id == cancionId);

            if (!existeCancion)
            {
                return NotFound();
            }

            var existeSello = await dbContext.Sellos.AnyAsync(selloDB => selloDB.Id == id);

            if (!existeSello)
            {
                return NotFound();
            }

            var sello = mapper.Map<Sellos>(selloCreacionDTO);
            sello.Id = id;
            sello.CancionId = cancionId;

            dbContext.Update(sello);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
