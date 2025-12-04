namespace NoteKeeper.Aplicacao.ModuloCategoria;

// DTO = Data Transfer Object

// Cadastro
public record CadastrarCategoriaCommand(string Titulo); // Objetos Imutáveis
public record CadastrarCategoriaResult(Guid Id);

// Edição
public record EditarCategoriaPartialCommand(string Titulo);
public record EditarCategoriaCommand(Guid Id, string Titulo);
public record EditarCategoriaResult(string Titulo);

// Exclusão
public record ExcluirCategoriaCommand(Guid Id);
public record ExcluirCategoriaResult();

// Seleção por Id
public record SelecionarCategoriaPorIdQuery(Guid Id);
public record SelecionarCategoriaPorIdResult(Guid Id, string Titulo);

// Seleção
public record SelecionarCategoriasResult(IReadOnlyList<SelecionarCategoriasDto> Registros);
public record SelecionarCategoriasDto(Guid Id, string Titulo);