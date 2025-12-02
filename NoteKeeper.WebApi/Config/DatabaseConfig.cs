using Microsoft.EntityFrameworkCore;
using NoteKeeper.Infraestrutura.Orm.Compartilhado;

namespace NoteKeeper.WebApi.Config;

public static class DatabaseConfig
{
    public static void AplicarMigracoesComOrm(this IHost app)
    {
        var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        dbContext.Database.Migrate();
    }
}
