using Microsoft.EntityFrameworkCore;
using NoteKeeper.Dominio.ModuloCategoria;
using NoteKeeper.Dominio.ModuloNota;

namespace NoteKeeper.Infraestrutura.Orm.Compartilhado;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Nota> Notas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assembly = typeof(AppDbContext).Assembly;

        modelBuilder.ApplyConfigurationsFromAssembly(assembly);

        // Filtro para registros marcados como excluídos
        modelBuilder.Entity<Categoria>().HasQueryFilter(x => !x.Excluido);
        modelBuilder.Entity<Nota>().HasQueryFilter(x => !x.Excluido);

        base.OnModelCreating(modelBuilder);
    }
}
