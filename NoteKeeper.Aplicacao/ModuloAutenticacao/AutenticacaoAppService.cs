using Microsoft.AspNetCore.Identity;
using NoteKeeper.Dominio.ModuloAutenticacao;
using NoteKeeper.Infraestrutura.Orm.Compartilhado;

namespace NoteKeeper.Aplicacao.ModuloAutenticacao;

public class AutenticacaoAppService(
    AppDbContext dbContext,
    AccessTokenProvider accessTokenProvider,
    UserManager<Usuario> userManager
)
{
    public async Task<AccessToken?> Registrar(RegistrarUsuarioCommand command)
    {
        if (!command.Senha.Equals(command.ConfirmarSenha))
            return null;

        Usuario usuario = new Usuario
        {
            FullName = command.NomeCompleto,
            UserName = command.Email,
            Email = command.Email
        };

        IdentityResult resultado = await userManager.CreateAsync(usuario, command.Senha);

        if (!resultado.Succeeded)
            return null;

        return accessTokenProvider.GetAccessToken(usuario);
    }

    public async Task<AccessToken?> Autenticar(AutenticarUsuarioCommand command)
    {
        Usuario? usuarioEncontrado = await userManager.FindByEmailAsync(command.Email);

        if (usuarioEncontrado is null)
            return null;

        bool autenticacaoValida = await userManager.CheckPasswordAsync(usuarioEncontrado, command.Senha);

        if (!autenticacaoValida)
            return null;
     
        return accessTokenProvider.GetAccessToken(usuarioEncontrado);
    }
}
