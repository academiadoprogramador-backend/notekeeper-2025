using Microsoft.Extensions.DependencyInjection;
using NoteKeeper.Aplicacao.ModuloCategoria;

namespace NoteKeeper.Aplicacao;

public static class DependencyInjection
{
    public static IServiceCollection AddCamadaAplicacao(this IServiceCollection services)
    {
        services.AddScoped<CategoriaAppService>();

        return services;
    }
}
