using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FIAPOracleEF.Models
{
    [Table("CARTEIRAS")]
    public class Carteira
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("ID_CLIENTE")]
        public int ClienteId { get; set; }

        [Column("ID_ATIVO")]
        public int AtivoId { get; set; }

        [Column("QUANTIDADE")]
        public int Quantidade { get; set; }
    }
}
