using Cadenza.Common.Domain.Enums;

namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record PlaylistState(bool IsLoading, string Id, PlaylistType Type, string Name) 
{
    public static PlaylistState Init() => new PlaylistState(false, null, PlaylistType.All, "Nothing");
}
