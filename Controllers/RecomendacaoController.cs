using FIAPOracleEF.Database;
using FIAPOracleEF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Controller responsável por operações de recomendação de investimentos.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class RecomendacaoController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IGeminiExplainerService _geminiExplainerService;

    /// <summary>
    /// Inicializa o controller de recomendação.
    /// </summary>
    public RecomendacaoController(AppDbContext context, IGeminiExplainerService geminiExplainerService)
    {
        _context = context;
        _geminiExplainerService = geminiExplainerService;
    }

    /// <summary>
    /// Retorna uma carteira de investimento recomendada para um cliente, com explicação da IA, baseada no ID.
    /// </summary>
    /// <param name="clienteId">O ID do cliente para gerar a recomendação.</param>
    [HttpGet("cliente/{clienteId}")]
    public async Task<IActionResult> GerarRecomendacaoParaCliente(int clienteId)
    {
        var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Id == clienteId);

        if (cliente == null)
        {
            return NotFound($"Cliente com ID {clienteId} não encontrado.");
        }

        var ativosDisponiveis = await _context.Ativos.ToListAsync();

        var ativosRecomendados = ativosDisponiveis
            .Where(a => a.Risco.Equals(cliente.Perfil, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (!ativosRecomendados.Any())
        {
            return Ok($"Não foram encontrados ativos adequados para o perfil '{cliente.Perfil}'.");
        }

        var carteiraRecomendada = new CarteiraDeInvestimentos
        {
            Ativos = ativosRecomendados,
            Explicacao = "",
            ValorTotal = cliente.LiquidezDisponivel
        };

        var ativosJson = JsonSerializer.Serialize(carteiraRecomendada.Ativos, new JsonSerializerOptions { WriteIndented = false });

        string prompt = $@"
            Justifique esta recomendação de investimentos:
            - Cliente: Perfil '{cliente.Perfil}', Objetivo '{cliente.Objetivo}', Liquidez Disponível: R${cliente.LiquidezDisponivel}.
            - Ativos Recomendados: {ativosJson}.
            - Regra utilizada: Filtramos ativos onde o Risco ({ativosRecomendados.First()?.Risco}) correspondia ao Perfil do cliente.
            A resposta deve ser um parágrafo conciso, em português, e focado em risco/objetivo.
        ";

        string explicacaoGemini = await _geminiExplainerService.GenerateExplanation(prompt);

        carteiraRecomendada.Explicacao = explicacaoGemini;

        var recomendacao = new RecomendacaoDTO
        {
            ClienteId = cliente.Id,
            Perfil = cliente.Perfil,
            Objetivo = cliente.Objetivo,
            Carteira = carteiraRecomendada
        };

        return Ok(recomendacao);
    }

}
