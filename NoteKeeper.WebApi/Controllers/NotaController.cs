using Microsoft.AspNetCore.Mvc;
using NoteKeeper.Aplicacao.ModuloNota;

namespace NoteKeeper.WebApi.Controllers;

[ApiController]
[Route("api/notas")]
public class NotaController(NotaAppService notaAppService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CadastrarNotaResult>> Cadastrar([FromBody] CadastrarNotaCommand command)
    {
        var resultado = await notaAppService.Cadastrar(command);

        if (resultado is null)
            return BadRequest("Não foi possível cadastrar. Verifique se o título já existe ou se a categoria é válida.");

        return CreatedAtAction(nameof(SelecionarPorId), new { id = resultado.Id }, resultado);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<EditarNotaResult>> Editar(Guid id, [FromBody] EditarNotaPartialCommand partialCommand)
    {
        var command = new EditarNotaCommand(
            id,
            partialCommand.Titulo,
            partialCommand.Conteudo,
            partialCommand.CategoriaId
        );

        var resultado = await notaAppService.Editar(command);

        if (resultado is null)
            return BadRequest("Falha ao editar. Nota não encontrada ou título já em uso.");

        return Ok(resultado);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<ExcluirNotaResult>> Excluir(Guid id)
    {
        var command = new ExcluirNotaCommand(id);

        var resultado = await notaAppService.Excluir(command);

        if (resultado is null)
            return NotFound("Nota não encontrada para exclusão.");

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<SelecionarNotasResult>> SelecionarTodos()
    {
        var query = new SelecionarNotasQuery();

        var resultado = await notaAppService.SelecionarTodos(query);

        return Ok(resultado);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SelecionarNotaPorIdResult>> SelecionarPorId(Guid id)
    {
        var query = new SelecionarNotaPorIdQuery(id);

        var resultado = await notaAppService.SelecionarPorId(query);

        if (resultado is null)
            return NotFound("Nota não encontrada.");

        return Ok(resultado);
    }
}
