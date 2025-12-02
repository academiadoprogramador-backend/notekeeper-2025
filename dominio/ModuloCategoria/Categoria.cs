using NoteKeeper.Dominio.Compartilhado;
using NoteKeeper.Dominio.ModuloNota;

namespace NoteKeeper.Dominio.ModuloCategoria;

public class Categoria : EntidadeBase<Categoria>
{
    public string Titulo { get; set; }
    public List<Nota> Notas { get; set; } = [];

    public Categoria(string titulo)
    {
        Titulo = titulo;
    }

    public override void AtualizarRegistro(Categoria registroEditado)
    {
        Titulo = registroEditado.Titulo;
    }
}