using NoteKeeper.Aplicacao.ModuloCategoria;

namespace NoteKeeper.Aplicacao.ModuloNota;

// --- Cadastrar ---
public record CadastrarNotaCommand(string Titulo, string Conteudo, Guid CategoriaId);
public record CadastrarNotaResult(Guid Id);

// --- Editar ---
public record EditarNotaPartialCommand(string Titulo, string Conteudo, Guid CategoriaId);
public record EditarNotaCommand(Guid Id, string Titulo, string Conteudo, Guid CategoriaId);
public record EditarNotaResult(string Titulo, string Conteudo, Guid CategoriaId);

// --- Excluir ---
public record ExcluirNotaCommand(Guid Id);
public record ExcluirNotaResult();

// --- Consultar Por Id ---
public record SelecionarNotaPorIdQuery(Guid Id);
public record SelecionarNotaPorIdResult(
    Guid Id,
    string Titulo,
    string Conteudo,
    SelecionarCategoriasDto Categoria
);

// --- Consultar ---
public record SelecionarNotasQuery();
public record SelecionarNotasResult(IReadOnlyList<SelecionarNotasDto> Registros);
public record SelecionarNotasDto(
    Guid Id,
    string Titulo,
    string Conteudo,
    Guid CategoriaId
);
