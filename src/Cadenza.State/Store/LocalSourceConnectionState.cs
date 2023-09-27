﻿using Cadenza.Web.Common.Tasks;

namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record LocalSourceConnectionState(string Title, ConnectionState State, string Message)
{
    public static LocalSourceConnectionState Init() => new LocalSourceConnectionState(null, ConnectionState.None, null);
}
