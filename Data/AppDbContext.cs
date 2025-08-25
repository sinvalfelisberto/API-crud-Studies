using Microsoft.EntityFrameworkCore;
using crudGus.Models;
namespace crudGus.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions _options) : base(_options) { }
    public DbSet<Personagem> Personagens { get; set; }
}