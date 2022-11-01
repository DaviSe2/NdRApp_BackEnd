using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using NPersistence;

namespace NdRViagens.Models
{
    [Entity]
    public class Destino
    {
        [Id]
        [GeneratedValue(Strategy = GenerationType.IDENTITY)]
        [ColumnAttribute("ID")]
        public long Id { get; set; } = 0;

        [NotNull]
        [ColumnAttribute("Cidade")]
        public string Cidade { get; set; }
    }
}
