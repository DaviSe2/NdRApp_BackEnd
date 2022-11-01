using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using NPersistence;

namespace NdRViagens.Models
{
    [Entity]
    public class PacoteViagem
    {
        [Id]
        [GeneratedValue(Strategy = GenerationType.IDENTITY)]
        [ColumnAttribute("ID")]
        public long Id { get; set; }

        [ColumnAttribute("Hospedagem")]
        public bool Hospedagem { get; set; }

        [ColumnAttribute("Alimentacao")]
        public bool Alimentacao { get; set; }

        [ColumnAttribute("Ingressos")]
        public bool Ingressos { get; set; }

        [NotNull]
        [ColumnAttribute("Nome")]
        public string Nome { get; set; }

        [NotNull]
        [ColumnAttribute("Preco", TypeName = "decimal(19,2)")]
        public decimal Preco { get; set; }

        [ColumnAttribute("Preco_Promo", TypeName = "decimal(19,2)")]
        [MaybeNull]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public decimal? PrecoPromo { get; set; }

        [NotNull]
        [OneToOne]
        [ForeignKey("Destino_ID")]
        public Destino Destino { get; set; }

        [NotNull]
        [ColumnAttribute("Descricao")]
        public string Descricao { get; set; }

    }
}
