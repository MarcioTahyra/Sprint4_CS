using FIAPOracleEF.Database;
using FIAPOracleEF.Models;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controller responsável por operações de recomendação de investimentos.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class RecomendacaoController : ControllerBase
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Inicializa o controller de recomendação.
    /// </summary>
    public RecomendacaoController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Carrega ativos de um arquivo e retorna uma recomendação.
    /// </summary>
    [HttpPost("carregar-ativos")]
    public IActionResult CarregarAtivos([FromBody] RecomendacaoDTO recomendacaoDTO)
    {
        if (recomendacaoDTO == null || string.IsNullOrEmpty(recomendacaoDTO.CaminhoArquivo))
        {
            return BadRequest("O caminho do arquivo é obrigatório.");
        }

        try
        {
            var ativoService = new AtivoService(_context);
            ativoService.CarregarAtivosDeArquivoJson(recomendacaoDTO.CaminhoArquivo);

            var recomendacao = new RecomendacaoDTO
            {
                ClienteId = recomendacaoDTO.ClienteId,
                Perfil = recomendacaoDTO.Perfil,
                Objetivo = recomendacaoDTO.Objetivo
            };

            return Ok(recomendacao);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao processar a requisição: {ex.Message}");
        }
    }
}
