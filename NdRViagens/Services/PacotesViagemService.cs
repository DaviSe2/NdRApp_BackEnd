using Microsoft.AspNetCore.Mvc;
using NdRViagens.Data;
using NdRViagens.Models;
using Microsoft.EntityFrameworkCore;

namespace NdRViagens.Services
{
    public class PacotesViagemService : ControllerBase
    {
        private readonly NdRDbContext _context;

        public PacotesViagemService(NdRDbContext context)
        {
            _context = context;
        }

        private static Decimal PorcentagemPromo = 0.50m;

        public IEnumerable<PacoteViagem> GetPacotesViagem()
        {
            return _context.PacoteViagem;
        }

        public IActionResult GetPacoteViagemById(long Id)
        {
            var PacoteViagem = _context.PacoteViagem.SingleOrDefault(PacoteViagem => PacoteViagem.Id == Id);

            if(PacoteViagem == null)
            {
                return NotFound();
            }
            return new ObjectResult(PacoteViagem);
        }

        public IActionResult GetPromoPacoteViagens(long id)
        {
            var PacoteViagem = _context.PacoteViagem.SingleOrDefault(PacoteViagem => PacoteViagem.Id == id);
            
            if(PacoteViagem == null)
            {
                return NotFound();
            }

            Decimal PrecoOriginal = PacoteViagem.Preco;
            PacoteViagem.PrecoPromo = PrecoOriginal * PorcentagemPromo;

            return new ObjectResult(PacoteViagem);
        }

        public IActionResult PostPacoteViagem(PacoteViagem PacoteViagem)
        {
            if(PacoteViagem == null)
            {
                return BadRequest();
            }

            _context.PacoteViagem.Add(PacoteViagem);
            _context.SaveChanges();
            return new ObjectResult(PacoteViagem);
        }

        public IActionResult AtualizarPacoteViagem(long Id, PacoteViagem PacoteViagem)
        {
            if(Id != PacoteViagem.Id)
            {
                return BadRequest();
            }

            _context.Entry(PacoteViagem).State = EntityState.Modified;
            _context.SaveChanges();

            return new ObjectResult(PacoteViagem);
        }

        public IActionResult DeletePacoteViagem(long Id)
        {
            var PacoteViagem = _context.PacoteViagem.SingleOrDefault(PacoteViagem => PacoteViagem.Id == Id);

            if(PacoteViagem == null)
            {
                return BadRequest();
            }

            _context.PacoteViagem.Remove(PacoteViagem);
            _context.SaveChanges();
            return Ok(PacoteViagem);
        }
    }
}
