namespace Cadenza.State.Actions;

public record SearchItemsUpdateRequest();

public record SearchItemsUpdatedAction(List<PlayerItemVM> Result);
