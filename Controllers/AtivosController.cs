using FIAPOracleEF.Database;
using FIAPOracleEF.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AtivosController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly AtivoService _ativoService;

    /// <summary>
    /// Inicializa o controller de ativos.
    /// </summary>
    public AtivosController(AppDbContext context, AtivoService ativoService)
    {
        _context = context;
        _ativoService = ativoService;
    }

    /// <summary>
    /// Retorna todos os ativos cadastrados.
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<Ativo>> GetAtivos()
    {
        var ativos = _context.Set<Ativo>().ToList();
        return Ok(ativos);
    }

    /// <summary>
    /// Retorna um ativo pelo seu ID.
    /// </summary>
    [HttpGet("{id}")]
    public IActionResult GetAtivoById(int id)
    {
        var ativo = _context.Ativos.FirstOrDefault(c => c.Id == id);

        if (ativo == null)
        {
            return NotFound($"Ativo com ID {id} não encontrado.");
        }

        return Ok(ativo);
    }

    /// <summary>
    /// Adiciona um novo ativo.
    /// </summary>
    [HttpPost]
    public ActionResult<Ativo> AddAtivo([FromBody] Ativo ativo)
    {
        _context.Set<Ativo>().Add(ativo);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetAtivos), new { id = ativo.Id }, ativo);
    }

    /// <summary>
    /// Atualiza um ativo existente.
    /// </summary>
    [HttpPut("{id}")]
    public IActionResult UpdateAtivo(int id, [FromBody] Ativo ativo)
    {
        if (ativo == null || ativo.Id != id)
        {
            return BadRequest("Dados do ativo inválidos.");
        }

        var existingAtivo = _context.Ativos.FirstOrDefault(c => c.Id == id);

        if (existingAtivo == null)
        {
            return NotFound($"Ativo com ID {id} não encontrado.");
        }

        existingAtivo.Nome = ativo.Nome;
        existingAtivo.Tipo = ativo.Tipo;
        existingAtivo.Rendimento = ativo.Rendimento;
        existingAtivo.Descricao = ativo.Descricao;
        existingAtivo.Liquidez = ativo.Liquidez;
        existingAtivo.Risco = ativo.Risco;

        _context.SaveChanges();

        return NoContent();
    }

    /// <summary>
    /// Remove um ativo pelo ID.
    /// </summary>
    [HttpDelete("{id}")]
    public IActionResult DeleteAtivo(int id)
    {
        var ativo = _context.Ativos.FirstOrDefault(c => c.Id == id);

        if (ativo == null)
        {
            return NotFound($"Ativo com ID {id} não encontrado.");
        }

        _context.Ativos.Remove(ativo);
        _context.SaveChanges();

        return NoContent();
    }

    /// <summary>
    /// Carrega ativos a partir de um arquivo JSON.
    /// </summary>
    [HttpPost("carregar-json")]
    public IActionResult CarregarAtivosDeJson([FromBody] string caminhoArquivo)
    {
        try
        {
            _ativoService.CarregarAtivosDeArquivoJson(caminhoArquivo);
            return Ok("Ativos carregados com sucesso!");
        }
        catch (FileNotFoundException e)
        {
            return BadRequest($"Erro: {e.Message}");
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Erro ao carregar os ativos: {e.Message}");
        }
    }
}
