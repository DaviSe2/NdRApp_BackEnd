using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NdRViagens.Models
{
    [Table("pacote_viagem")]
    public class PacoteViagem
    {
        [Key]
        public long Id { get; set; }

        public bool Hospedagem { get; set; }

        public bool Alimentacao { get; set; }

        public bool Ingressos { get; set; }

        [Required]
        public string nome { get; set; }

        [Required]
        public decimal Preco { get; set; }

        public decimal PrecoPromo { get; set; }

        [ForeignKey("IdDestino")]
        public Destino Destino { get; set; }

        [Required]
        public string Descricao { get; set; }

    }
}
