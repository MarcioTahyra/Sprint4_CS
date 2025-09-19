namespace FIAPOracleEF.Models
{
    /// <summary>
    /// DTO que representa uma recomendação de investimentos para um cliente.
    /// </summary>
    public class RecomendacaoDTO
    {
        /// <summary>
        /// Caminho do arquivo JSON contendo os ativos recomendados.
        /// </summary>
        public string CaminhoArquivo { get; set; }

        /// <summary>
        /// Identificador do cliente para quem a recomendação é destinada.
        /// </summary>
        public int ClienteId { get; set; }

        /// <summary>
        /// Perfil do investidor (ex: conservador, moderado, agressivo).
        /// </summary>
        public string Perfil { get; set; } = "";

        /// <summary>
        /// Objetivo financeiro do cliente (ex: aposentadoria, compra de imóvel).
        /// </summary>
        public string Objetivo { get; set; } = "";

        /// <summary>
        /// Carteira de investimentos recomendada ao cliente.
        /// </summary>
        public CarteiraDeInvestimentos Carteira { get; set; }
    }
}
