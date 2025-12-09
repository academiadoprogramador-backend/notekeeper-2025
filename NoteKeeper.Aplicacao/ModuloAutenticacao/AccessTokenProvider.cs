using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NoteKeeper.Dominio.ModuloAutenticacao;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NoteKeeper.Aplicacao.ModuloAutenticacao;

public class AccessTokenProvider
{
    private readonly string chaveAssinaturaJwt;
    private readonly string audienciaValida;

    public AccessTokenProvider(IConfiguration configuration)
    {
        chaveAssinaturaJwt = configuration["JWT_GENERATION_KEY"]!;
        audienciaValida = configuration["JWT_AUDIENCE_DOMAIN"]!;
    }

    public AccessToken GetAccessToken(Usuario usuario)
    {
        var expiracaoEmUtc = DateTime.UtcNow.AddMinutes(5);

        var chaveEmBytes = Encoding.ASCII.GetBytes(chaveAssinaturaJwt);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = "NoteKeeper",
            Audience = audienciaValida,

            Subject = new ClaimsIdentity(
            [
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, usuario.FullName),
                new Claim(JwtRegisteredClaimNames.UniqueName, usuario.UserName!),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, usuario.AccessTokenVersionId.ToString())
            ]),

            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(chaveEmBytes),
                SecurityAlgorithms.HmacSha256Signature
            ),

            Expires = expiracaoEmUtc,
            NotBefore = DateTime.UtcNow
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        var tokenString = tokenHandler.WriteToken(token);

        return new AccessToken(
            tokenString,
            expiracaoEmUtc,
            new UsuarioAutenticado(
                usuario.Id,
                usuario.FullName,
                usuario.Email ?? string.Empty
            )
        );
    }
}
