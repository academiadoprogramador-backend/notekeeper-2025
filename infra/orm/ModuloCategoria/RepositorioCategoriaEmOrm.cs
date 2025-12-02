using Microsoft.EntityFrameworkCore;
using NoteKeeper.Dominio.ModuloCategoria;
using NoteKeeper.Infraestrutura.Orm.Compartilhado;

namespace NoteKeeper.Infraestrutura.Orm.ModuloCategoria;

public class RepositorioCategoriaEmOrm(AppDbContext contexto) : RepositorioBaseEmOrm<Categoria>(contexto)
{
    public override async Task<Categoria?> SelecionarPorIdAsync(Guid idRegistro)
    {
        return await registros
            .Include(c => c.Notas)
            .FirstOrDefaultAsync(x => x.Id.Equals(idRegistro));
    }
}
