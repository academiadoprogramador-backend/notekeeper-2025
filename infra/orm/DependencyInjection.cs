using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NoteKeeper.Infraestrutura.Orm.Compartilhado;
using NoteKeeper.Infraestrutura.Orm.ModuloCategoria;
using NoteKeeper.Infraestrutura.Orm.ModuloNota;

namespace NoteKeeper.Infraestrutura.Orm;

public static class DependencyInjection
{
    public static IServiceCollection AddCamadaInfraestruturaOrm(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["SQL_CONNECTION_STRING"];

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new Exception("A variável de ambiente \"SQL_CONNECTION_STRING\" não foi fornecida.");

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString, opt => opt.EnableRetryOnFailure(3)));

        services.AddScoped<RepositorioCategoriaEmOrm>();
        services.AddScoped<RepositorioNotaEmOrm>();

        return services;
    }
}
