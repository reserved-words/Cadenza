using Cadenza.Common.Enums;

namespace Cadenza.Web.State.Actions;

public record CancelEditItemRequest(LibraryItemType Type, int Id);
public record RemoveEditItemRequest(LibraryItemType Type, int Id);
public record SaveEditItemRequest(LibraryItemType Type, int Id);