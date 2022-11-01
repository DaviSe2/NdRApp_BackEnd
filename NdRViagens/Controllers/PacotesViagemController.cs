using Microsoft.AspNetCore.Mvc;
using NdRViagens.Data;
using NdRViagens.Models;
using Microsoft.EntityFrameworkCore;
using NPersistence;

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

        private static decimal PorcentagemPromo = 0.50m;

        [HttpGet]
        public IEnumerable<PacoteViagem> GetPacotesViagem()
        {
            return _context.Pacotes.Include(x => x.Destino);
        }

        [HttpGet("{id}")]
        public IActionResult GetPacoteViagemById(long id)
        {
            var PacoteViagem = _context.Pacotes.SingleOrDefault(PacoteViagem => PacoteViagem.Id == id);

            if (PacoteViagem == null)
            {
                return NotFound();
            }
            return new ObjectResult(PacoteViagem);
        }

        [HttpGet("promocoes/{id}")]
        public IActionResult GetPromoPacoteViagens(long id)
        {
            var PacoteViagem = _context.Pacotes.SingleOrDefault(PacoteViagem => PacoteViagem.Id == id);

            if (PacoteViagem == null)
            {
                return NotFound();
            }

            Decimal PrecoOriginal = PacoteViagem.Preco;
            PacoteViagem.PrecoPromo = PrecoOriginal * PorcentagemPromo;

            return new ObjectResult(PacoteViagem);
        }

        [HttpPost]
        public IActionResult PostPacoteViagem(PacoteViagem PacoteViagem)
        {
            if (PacoteViagem == null)
            {
                return BadRequest();
            }

            var destino = _context.Destinos.Where(x => x.Id == PacoteViagem.Destino.Id).FirstOrDefault();

            if (destino != null)
            {
                PacoteViagem.Destino = destino;
            }

            _context.Pacotes.Add(PacoteViagem);
            _context.SaveChanges();

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
            var PacoteViagem = _context.Pacotes.SingleOrDefault(PacoteViagem => PacoteViagem.Id == Id);

            if (PacoteViagem == null)
            {
                return BadRequest();
            }

            _context.Pacotes.Remove(PacoteViagem);
            _context.SaveChanges();
            return Ok(PacoteViagem);
        }
    }
}
