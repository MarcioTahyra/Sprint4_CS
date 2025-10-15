using FIAPOracleEF.Database;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.IO;
// Adicione 'using System.Linq;' e 'using System.Threading.Tasks;' no topo de seus controllers

var builder = WebApplication.CreateBuilder(args);

// --- 1. Configura��o do DbContext ---
var connectionString = builder.Configuration.GetConnectionString("OracleConnection");

if (string.IsNullOrEmpty(connectionString))
{
    // Esta exce��o ser� vista no Application Insights do Azure se a Connection String n�o for configurada.
    throw new InvalidOperationException("A string de conex�o 'OracleConnection' n�o foi configurada. Verifique as configura��es de ambiente/App Service.");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(connectionString)
);

// --- 2. Configura��o da API e Swagger ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configura��o do Swagger para incluir a documenta��o XML
builder.Services.AddSwaggerGen(c =>
{
    // Gera o nome e o caminho completo do arquivo XML de documenta��o
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    // Adiciona a documenta��o XML se o arquivo existir
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});


// --- 3. Configura��o da Inje��o de Depend�ncia ---
// HttpClient e Servi�o Gemini (lendo a chave de API de IConfiguration)
builder.Services.AddHttpClient<IGeminiExplainerService, GeminiExplainerService>();
builder.Services.AddTransient<IGeminiExplainerService, GeminiExplainerService>();
// Registrando o AtivoService (Assumindo que existe a classe AtivoService)
builder.Services.AddTransient<AtivoService>();


var app = builder.Build();

// --- 4. Configura��o do Pipeline ---

// HABILITA SWAGGER FORA DA CONDI��O DE DESENVOLVIMENTO
app.UseSwagger();
app.UseSwaggerUI();

// O bloco de desenvolvimento original foi removido ou est� vazio, pois o Swagger foi movido para fora do if.
// if (app.Environment.IsDevelopment()) { }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
