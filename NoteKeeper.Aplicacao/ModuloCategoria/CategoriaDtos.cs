namespace NoteKeeper.Aplicacao.ModuloCategoria;

// DTO = Data Transfer Object

// Cadastro
public record CadastrarCategoriaCommand(string Titulo); // Objetos Imutáveis
public record CadastrarCategoriaResult(Guid Id);

// Seleção
public record SelecionarCategoriasResult(IReadOnlyList<SelecionarCategoriasDto> Registros);
public record SelecionarCategoriasDto(Guid Id, string Titulo);
