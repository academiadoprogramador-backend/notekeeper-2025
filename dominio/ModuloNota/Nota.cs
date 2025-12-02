using NoteKeeper.Dominio.Compartilhado;
using NoteKeeper.Dominio.ModuloCategoria;

namespace NoteKeeper.Dominio.ModuloNota;

public class Nota : EntidadeBase<Nota>
{
    public string Titulo { get; set; }
    public string Conteudo { get; set; }

    public Guid CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }

    public Nota(string titulo, string conteudo, Guid categoriaId)
    {
        Titulo = titulo;
        Conteudo = conteudo;
        CategoriaId = categoriaId;
    }

    public override void AtualizarRegistro(Nota registroEditado)
    {
        Titulo = registroEditado.Titulo;
        Conteudo = registroEditado.Conteudo;
        CategoriaId = registroEditado.CategoriaId;
    }
}
