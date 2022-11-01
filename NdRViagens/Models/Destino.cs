using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NdRViagens.Models
{
    [Table("destino")]
    public class Destino
    {
        [Key]
        public long Id { get; set; } = 0;

        [Required(ErrorMessage = "Informe a cidade")]
        public string Cidade { get; set; }
    }
}
