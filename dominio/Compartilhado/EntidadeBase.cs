namespace NoteKeeper.Dominio.Compartilhado;

public abstract class EntidadeBase<T>
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTimeOffset CriadaEmUtc { get; set; } = DateTime.UtcNow;
    public DateTimeOffset? ExcluidoEmUtc { get; set; } = null;
    public bool Excluido { get; set; } = false;

    public abstract void AtualizarRegistro(T registroEditado);

    public void Excluir()
    {
        Excluido = true;
        ExcluidoEmUtc = DateTime.UtcNow;
    }
}
