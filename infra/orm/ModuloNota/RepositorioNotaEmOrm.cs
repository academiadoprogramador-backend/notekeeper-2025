using Microsoft.EntityFrameworkCore;
using NoteKeeper.Dominio.ModuloNota;
using NoteKeeper.Infraestrutura.Orm.Compartilhado;

namespace NoteKeeper.Infraestrutura.Orm.ModuloNota;

public class RepositorioNotaEmOrm(AppDbContext contexto) : RepositorioBaseEmOrm<Nota>(contexto)
{
    public override async Task<Nota?> SelecionarPorIdAsync(Guid idRegistro)
    {
        return await registros
            .Include(n => n.Categoria)
            .FirstOrDefaultAsync(x => x.Id.Equals(idRegistro));
    }

    public override async Task<List<Nota>> SelecionarTodosAsync()
    {
        return await registros
            .Include(n => n.Categoria)
            .ToListAsync();
    }
}
