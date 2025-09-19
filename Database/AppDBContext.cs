using FIAPApi.Models;
using FIAPOracleEF.Models;
using Microsoft.EntityFrameworkCore;

namespace FIAPOracleEF.Database;

public class AppDbContext : DbContext
{
    private readonly string _connString;

    const string DataSource = "oracle.fiap.com.br:1521/ORCL";



    public AppDbContext()
    {
        string connString = "User Id=" + "xxx" + ";Password=" + "xxx" + ";Data Source=" + DataSource;
        _connString = connString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseOracle(_connString);
    }

    public DbSet<Ativo> Ativos { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Carteira> Carteiras { get; set; }
}