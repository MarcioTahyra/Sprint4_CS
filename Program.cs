using FIAPOracleEF.Database;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.IO;
// Adicione 'using System.Linq;' e 'using System.Threading.Tasks;' no topo de seus controllers

var builder = WebApplication.CreateBuilder(args);

// --- 1. Configuração do DbContext ---
var connectionString = builder.Configuration.GetConnectionString("OracleConnection");

if (string.IsNullOrEmpty(connectionString))
{
    // Esta exceção será vista no Application Insights do Azure se a Connection String não for configurada.
    throw new InvalidOperationException("A string de conexão 'OracleConnection' não foi configurada. Verifique as configurações de ambiente/App Service.");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(connectionString)
);

// --- 2. Configuração da API e Swagger ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configuração do Swagger para incluir a documentação XML
builder.Services.AddSwaggerGen(c =>
{
    // Gera o nome e o caminho completo do arquivo XML de documentação
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    // Adiciona a documentação XML se o arquivo existir
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});


// --- 3. Configuração da Injeção de Dependência ---
// HttpClient e Serviço Gemini (lendo a chave de API de IConfiguration)
builder.Services.AddHttpClient<IGeminiExplainerService, GeminiExplainerService>();
builder.Services.AddTransient<IGeminiExplainerService, GeminiExplainerService>();
// Registrando o AtivoService (Assumindo que existe a classe AtivoService)
builder.Services.AddTransient<AtivoService>();


var app = builder.Build();

// --- 4. Configuração do Pipeline ---

// HABILITA SWAGGER FORA DA CONDIÇÃO DE DESENVOLVIMENTO
app.UseSwagger();
app.UseSwaggerUI();

// O bloco de desenvolvimento original foi removido ou está vazio, pois o Swagger foi movido para fora do if.
// if (app.Environment.IsDevelopment()) { }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
