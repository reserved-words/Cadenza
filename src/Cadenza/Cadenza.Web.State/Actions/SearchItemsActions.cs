namespace Cadenza.Web.State.Actions;

public record SearchItemsUpdateRequest();

public record SearchItemsUpdatedAction(IReadOnlyCollection<PlayerItemVM> Result);
