using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration; // Necessário para IConfiguration

public class GeminiExplainerService : IGeminiExplainerService
{
    private readonly HttpClient _httpClient;
    private const string GeminiModel = "gemini-2.5-flash";
    private readonly string _apiKey;

    /// <summary>
    /// Inicializa o serviço, injetando HttpClient e lendo a chave de API da configuração.
    /// </summary>
    public GeminiExplainerService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri($"https://generativelanguage.googleapis.com/");

        // Lendo a chave da configuração (Chave deve ser configurada no Azure/appsettings como GeminiApi:ApiKey)
        _apiKey = configuration["GeminiApi:ApiKey"]
                  ?? throw new ArgumentNullException("A Chave da Gemini API (GeminiApi:ApiKey) não foi configurada.");
    }

    /// <summary>
    /// Envia o prompt de recomendação para a API do Gemini e retorna a justificativa.
    /// </summary>
    public async Task<string> GenerateExplanation(string prompt)
    {
        // 1. Monta o corpo da requisição (payload)
        var requestBody = new
        {
            contents = new[]
            {
                new
                {
                    role = "user",
                    parts = new[]
                    { 
                        // Adicionando instrução de sistema no prompt
                        new { text = $"Você é um consultor financeiro que justifica recomendações de investimento de forma profissional e acessível. {prompt}" }
                    }
                }
            },
            // Usando generationConfig (corrigido)
            generationConfig = new
            {
                temperature = 0.7
            }
        };

        var jsonContent = new StringContent(
            JsonSerializer.Serialize(requestBody),
            Encoding.UTF8,
            "application/json"
        );

        // 2. Define a URL com a chave injetada
        string url = $"v1/models/{GeminiModel}:generateContent?key={_apiKey}";

        try
        {
            // 3. Envia a requisição HTTP
            var response = await _httpClient.PostAsync(url, jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();

                // 4. Desserialização para extrair o texto do Gemini
                using (JsonDocument document = JsonDocument.Parse(jsonResponse))
                {
                    var root = document.RootElement;
                    if (root.TryGetProperty("candidates", out var candidates) && candidates.GetArrayLength() > 0)
                    {
                        if (candidates[0].TryGetProperty("content", out var content) && content.TryGetProperty("parts", out var parts) && parts.GetArrayLength() > 0)
                        {
                            if (parts[0].TryGetProperty("text", out var text))
                            {
                                return text.GetString()?.Trim() ?? "Erro ao extrair explicação do Gemini.";
                            }
                        }
                    }
                }
                return "Resposta do Gemini recebida, mas falha ao analisar o conteúdo.";
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return $"Erro na API do Gemini: Status {response.StatusCode}. Conteúdo: {errorContent}";
            }
        }
        catch (Exception ex)
        {
            return $"Erro inesperado ao comunicar com o Gemini: {ex.Message}";
        }
    }
}
