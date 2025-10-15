using FIAPApi.Models;
using FIAPOracleEF.Models;
using Microsoft.EntityFrameworkCore;

namespace FIAPOracleEF.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Ativo> Ativos { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Carteira> Carteiras { get; set; }
}
