using Microsoft.EntityFrameworkCore;
using MInimalApiPelis.Entidades;

namespace MInimalApiPelis;

public class ApplicactionDbContext: DbContext
{
    public ApplicactionDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Genero> Generos { get; set; }
}