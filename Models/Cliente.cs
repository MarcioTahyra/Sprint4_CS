using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FIAPOracleEF.Models
{
    [Table("CLIENTES")]
    public class Cliente
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("NOME")]
        public string Nome { get; set; } = "";

        [Column("IDADE")]
        public int Idade { get; set; }

        [Column("PERFIL")]
        public string Perfil { get; set; } = "";

        [Column("LIQUIDEZ_DISPONIVEL")]
        public decimal LiquidezDisponivel { get; set; }

        [Column("OBJETIVO")]
        public string Objetivo { get; set; } = ""; 
    }
}
