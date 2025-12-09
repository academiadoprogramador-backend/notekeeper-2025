using Microsoft.Extensions.DependencyInjection;
using NoteKeeper.Aplicacao.ModuloAutenticacao;
using NoteKeeper.Aplicacao.ModuloCategoria;
using NoteKeeper.Aplicacao.ModuloNota;

namespace NoteKeeper.Aplicacao;

public static class DependencyInjection
{
    public static IServiceCollection AddCamadaAplicacao(this IServiceCollection services)
    {
        services.AddScoped<CategoriaAppService>();
        services.AddScoped<NotaAppService>();

        services.AddScoped<AutenticacaoAppService>();
        services.AddScoped<AccessTokenProvider>();

        return services;
    }
}
