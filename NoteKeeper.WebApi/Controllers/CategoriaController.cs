using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteKeeper.Aplicacao.ModuloCategoria;

namespace NoteKeeper.WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/categorias")]
public class CategoriaController(CategoriaAppService categoriaAppService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CadastrarCategoriaResult>> Cadastrar([FromBody] CadastrarCategoriaCommand command)
    {
        var resultado = await categoriaAppService.Cadastrar(command);

        if (resultado is null)
            return BadRequest("Não foi possível cadastrar a categoria. Tente novamente.");

        return CreatedAtAction(nameof(SelecionarPorId), new { id = resultado.Id }, resultado);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<EditarCategoriaResult>> Editar(Guid id, [FromBody] EditarCategoriaPartialCommand partialCommand)
    {
        var command = new EditarCategoriaCommand(id, partialCommand.Titulo);

        var result = await categoriaAppService.Editar(command);

        if (result is null)
            return BadRequest("Falha ao editar. Tente novamente mais tarde.");

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<ExcluirCategoriaResult>> Excluir(Guid id)
    {
        var command = new ExcluirCategoriaCommand(id);

        var result = await categoriaAppService.Excluir(command);

        if (result is null)
            return NotFound("Categoria não encontrada para exclusão.");

        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SelecionarCategoriaPorIdResult>> SelecionarPorId(Guid id)
    {
        var query = new SelecionarCategoriaPorIdQuery(id);

        var result = await categoriaAppService.SelecionarPorId(query);

        if (result is null)
            return NotFound("Categoria não encontrada.");

        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<SelecionarCategoriasResult>> SelecionarTodas()
    {
        var query = new SelecionarCategoriasQuery();

        var result = await categoriaAppService.SelecionarTodas(query);

        return Ok(result);
    }
}
