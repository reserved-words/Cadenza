namespace Cadenza.State.Model;

public record ItemUpdateRequestVM(LibraryItemType Type, int Id, IReadOnlyCollection<PropertyUpdateVM> Updates);