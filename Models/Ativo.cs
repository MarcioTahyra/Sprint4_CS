using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;

namespace FIAPOracleEF.Models
{
    [Table("ATIVOS")]
    public class Ativo
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("NOME")]
        public string Nome { get; set; } = "";

        [Column("TIPO")]
        public string Tipo { get; set; } = ""; 
        [Column("DESCRICAO")]
        public string Descricao { get; set; } = "";

        [Column("RENDIMENTO")]
        public decimal Rendimento { get; set; } 

        [Column("LIQUIDEZ")]
        public string Liquidez { get; set; } = "";  

        [Column("RISCO")]
        public string Risco { get; set; } = ""; 
    }

    }
