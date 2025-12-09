using Microsoft.AspNetCore.Mvc;
using NoteKeeper.Aplicacao.ModuloAutenticacao;
using NoteKeeper.Aplicacao.ModuloCategoria;
using NoteKeeper.Dominio.ModuloAutenticacao;

namespace NoteKeeper.WebApi.Controllers;

[ApiController]
[Route("api/auth")]
public class AutenticacaoController(AutenticacaoAppService autenticacaoAppService) : ControllerBase
{
    [HttpPost("registrar")]
    public async Task<ActionResult<AccessToken?>> Registrar(RegistrarUsuarioCommand command)
    {
        var accessToken = await autenticacaoAppService.Registrar(command);

        if (accessToken is null)
            return BadRequest("Não foi possível registrar o usuário. Tente novamente.");

        return Ok(accessToken);
    }

    [HttpPost("autenticar")]
    public async Task<ActionResult<AccessToken?>> Autenticar(AutenticarUsuarioCommand command)
    {
        var accessToken = await autenticacaoAppService.Autenticar(command);

        if (accessToken is null)
            return BadRequest("Não foi possível autenticar o usuário. Tente novamente.");

        return Ok(accessToken);
    }
}
