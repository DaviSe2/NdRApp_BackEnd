using Microsoft.AspNetCore.Mvc;
using NdRViagens.Data;
using NdRViagens.Models;

namespace NdRViagens.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestinosController : ControllerBase
    {
        private readonly NdRDbContext _context;

        public DestinosController(NdRDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Destino> GetDestinos()
        {
            return _context.Destino;
        }

        [HttpGet("{id}")]
        public IActionResult GetDestinoById(int id)
        {
            var destino = _context.Destino.SingleOrDefault(destino => destino.Id == id);
            
            if(destino == null)
            {
                return NotFound();
            }
            return new ObjectResult(destino);
        }

        [HttpPost]
        public IActionResult PostDestino([FromBody] Destino destino)
        {
            if(destino == null)
            {
                return BadRequest();
            }
            _context.Destino.Add(destino);
            _context.SaveChanges();
            return new ObjectResult(destino);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDestino(int id)
        {
            var destino = _context.Destino.SingleOrDefault(destino => destino.Id == id);

            if(destino == null)
            {
                return BadRequest();
            }
            _context.Destino.Remove(destino);
            _context.SaveChanges();
            return Ok(destino);
        }

    }
}
