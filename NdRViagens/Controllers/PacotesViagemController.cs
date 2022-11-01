using Microsoft.AspNetCore.Mvc;
using NdRViagens.Data;
using NdRViagens.Models;
using Microsoft.EntityFrameworkCore;

namespace NdRViagens.Services
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacotesViagemController : ControllerBase
    {
        private readonly NdRDbContext _context;

        public PacotesViagemController(NdRDbContext context)
        {
            _context = context;
        }

        private static Decimal PorcentagemPromo = 0.50m;

        [HttpGet]
        public IEnumerable<PacoteViagem> GetPacotesViagem()
        {
            return _context.PacoteViagem.Include(x => x.Destino);
        }

        [HttpGet("{id}")]
        public IActionResult GetPacoteViagemById(long id)
        {
            var PacoteViagem = _context.PacoteViagem.SingleOrDefault(PacoteViagem => PacoteViagem.Id == id);

            if (PacoteViagem == null)
            {
                return NotFound();
            }
            return new ObjectResult(PacoteViagem);
        }

        [HttpGet("promocoes/{id}")]
        public IActionResult GetPromoPacoteViagens(long id)
        {
            var PacoteViagem = _context.PacoteViagem.SingleOrDefault(PacoteViagem => PacoteViagem.Id == id);

            if (PacoteViagem == null)
            {
                return NotFound();
            }

            Decimal PrecoOriginal = PacoteViagem.Preco;
            PacoteViagem.PrecoPromo = PrecoOriginal * PorcentagemPromo;

            return new ObjectResult(PacoteViagem);
        }

        [HttpPost]
        public async Task<IActionResult> PostPacoteViagem(PacoteViagem PacoteViagem)
        {
            if (PacoteViagem == null)
            {
                return BadRequest();
            }

            var destino = _context.Destino.Where(x => x.Id == PacoteViagem.Destino.Id).FirstOrDefault();

            await _context.PacoteViagem.AddAsync(PacoteViagem);
            await _context.SaveChangesAsync();
            return new ObjectResult(PacoteViagem);
        }   

        [HttpPut]
        public IActionResult AtualizarPacoteViagem(long Id, PacoteViagem PacoteViagem)
        {
            if (Id != PacoteViagem.Id)
            {
                return BadRequest();
            }

            _context.Entry(PacoteViagem).State = EntityState.Modified;
            _context.SaveChanges();

            return new ObjectResult(PacoteViagem);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePacoteViagem(long Id)
        {
            var PacoteViagem = _context.PacoteViagem.SingleOrDefault(PacoteViagem => PacoteViagem.Id == Id);

            if (PacoteViagem == null)
            {
                return BadRequest();
            }

            _context.PacoteViagem.Remove(PacoteViagem);
            _context.SaveChanges();
            return Ok(PacoteViagem);
        }
    }
}
