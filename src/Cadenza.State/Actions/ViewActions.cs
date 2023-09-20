using Cadenza.Common.Domain.Enums;
using Cadenza.Web.Common.Enums;

namespace Cadenza.State.Actions;

public record ViewTabRequest(Tab Tab);
public record ViewItemRequest(PlayerItemType Type, string Id, string Name);