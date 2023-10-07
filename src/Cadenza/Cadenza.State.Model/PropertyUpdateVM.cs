namespace Cadenza.State.Model;

public record PropertyUpdateVM(int Id, ItemProperty Property, string OriginalValue, string UpdatedValue);