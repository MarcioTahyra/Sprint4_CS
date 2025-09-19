namespace FIAPOracleEF.Models
{
    /// <summary>
    /// Representa uma carteira de investimentos composta por ativos.
    /// </summary>
    public class CarteiraDeInvestimentos
    {
        /// <summary>
        /// Lista de ativos que compõem a carteira.
        /// </summary>
        public List<Ativo> Ativos { get; set; } = new List<Ativo>();

        /// <summary>
        /// Explicação ou justificativa da composição da carteira.
        /// </summary>
        public string Explicacao { get; set; } = "";

        /// <summary>
        /// Valor total investido na carteira.
        /// </summary>
        public decimal ValorTotal { get; set; }
    }
}
