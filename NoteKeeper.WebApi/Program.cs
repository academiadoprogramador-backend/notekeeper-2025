
using NoteKeeper.Aplicacao;
using NoteKeeper.Infraestrutura.Orm;
using NoteKeeper.WebApi.Config;

namespace NoteKeeper.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddCamadaInfraestruturaOrm(builder.Configuration);
        builder.Services.AddCamadaAplicacao();

        builder.Services.AddSwaggerConfig();

        builder.Services.AddControllers();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.AplicarMigracoesComOrm();

            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
