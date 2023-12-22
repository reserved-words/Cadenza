﻿using Cadenza.Common.Enums;

namespace Cadenza.Web.State.Actions;

public record ViewTabRequest(Tab Tab);
public record ViewItemRequest(PlayerItemType? Type, string Id, string Name);
public record ViewEditItemRequest(LibraryItemType Type, int Id, string Name);
public record ViewResetRequest();