using Microsoft.Extensions.Logging;
using NoteKeeper.Dominio.ModuloCategoria;
using NoteKeeper.Infraestrutura.Orm.Compartilhado;
using NoteKeeper.Infraestrutura.Orm.ModuloCategoria;
using System.Collections.Immutable;

namespace NoteKeeper.Aplicacao.ModuloCategoria;

public class CategoriaAppService(
    AppDbContext dbContext,
    RepositorioCategoriaEmOrm repositorioCategoria,
    ILogger<CategoriaAppService> logger
)
{
    public async Task<CadastrarCategoriaResult?> Cadastrar(CadastrarCategoriaCommand command)
    {
		try
		{
            var categoria = new Categoria(command.Titulo);

            await repositorioCategoria.CadastrarAsync(categoria);

            await dbContext.SaveChangesAsync();

            return new CadastrarCategoriaResult(categoria.Id);
        }
        catch (Exception ex)
		{
            logger.LogError(ex, "Ocorreu um erro durante o cadastro de {@Command}", command);

			throw;
		}
    }

    public async Task<SelecionarCategoriasResult> SelecionarTodas()
    {
        var registros = await repositorioCategoria.SelecionarTodosAsync();

        var dtos = registros
            .Select(c => new SelecionarCategoriasDto(c.Id, c.Titulo))
            .ToImmutableList();

        return new SelecionarCategoriasResult(dtos);
    }
}
