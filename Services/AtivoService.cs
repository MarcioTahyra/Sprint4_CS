using FIAPOracleEF.Database;
using FIAPOracleEF.Models;
using System.Text.Json;


public class AtivoService
{
    private readonly AppDbContext _context;

    public AtivoService(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Carrega ativos a partir de um arquivo JSON e salva no banco de dados.
    /// </summary>
    /// <param name="caminhoArquivo">Caminho do arquivo JSON.</param>
    /// <exception cref="FileNotFoundException">Se o arquivo não for encontrado.</exception>
    /// <exception cref="Exception">Se ocorrer erro na deserialização.</exception>
    public void CarregarAtivosDeArquivoJson(string caminhoArquivo)
    {
        if (!File.Exists(caminhoArquivo))
        {
            throw new FileNotFoundException("Arquivo JSON não encontrado.", caminhoArquivo);
        }

        var json = File.ReadAllText(caminhoArquivo);
        var ativos = JsonSerializer.Deserialize<List<Ativo>>(json);

        if (ativos == null)
        {
            throw new Exception("Erro ao deserializar o arquivo JSON.");
        }

        foreach (var ativo in ativos)
        {
            _context.Ativos.Add(ativo);
        }

        _context.SaveChanges();
    }
}
