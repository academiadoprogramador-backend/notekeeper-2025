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

    public async Task<EditarCategoriaResult?> Editar(EditarCategoriaCommand command)
    {
        try
        {
            var categoriaEditada = new Categoria(command.Titulo);

            var sucesso = await repositorioCategoria.EditarAsync(command.Id, categoriaEditada);

            if (!sucesso)
                return null;

            await dbContext.SaveChangesAsync();

            return new EditarCategoriaResult(categoriaEditada.Titulo);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ocorreu um erro durante a edição de {@Command}", command);

            throw;
        }
    }

    public async Task<ExcluirCategoriaResult?> Excluir(ExcluirCategoriaCommand command)
    {
        try
        {
            var sucesso = await repositorioCategoria.ExcluirAsync(command.Id);

            if (!sucesso)
                return null;

            await dbContext.SaveChangesAsync();

            return new ExcluirCategoriaResult();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ocorreu um erro durante a exclusão de {@Command}", command);

            throw;
        }
    }

    public async Task<SelecionarCategoriaPorIdResult?> SelecionarPorId(SelecionarCategoriaPorIdQuery query)
    {
        var categoria = await repositorioCategoria.SelecionarPorIdAsync(query.Id);

        if (categoria is null)
            return null;

        return new SelecionarCategoriaPorIdResult(categoria.Id, categoria.Titulo);
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
