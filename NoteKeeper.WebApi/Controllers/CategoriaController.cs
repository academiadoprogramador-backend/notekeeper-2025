using Microsoft.AspNetCore.Mvc;
using NoteKeeper.Aplicacao.ModuloCategoria;
using NoteKeeper.Dominio.ModuloCategoria;

namespace NoteKeeper.WebApi.Controllers;

[ApiController]
[Route("/api/categorias")]
public class CategoriaController(CategoriaAppService categoriaAppService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CadastrarCategoriaResult>> Cadastrar(CadastrarCategoriaCommand command)
    {
        var resultado = await categoriaAppService.Cadastrar(command);

        if (resultado is null)
            return BadRequest("Não foi possível cadastrar a categoria. Tente novamente.");

        return Ok(resultado);
    }

    [HttpPut("{id:guid}")]
    public IActionResult Editar(Guid id)
    {
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Excluir(Guid id)
    {
        return Ok();
    }

    [HttpGet("{id:guid}")]
    public IActionResult SelecionarPorId(Guid id)
    {
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<SelecionarCategoriasResult>> SelecionarTodas()
    {
        var result = await categoriaAppService.SelecionarTodas();

        return Ok(result);
    }
}
