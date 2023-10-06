using System.Collections.ObjectModel;

namespace Cadenza.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record ArtistImagesState(IReadOnlyDictionary<int, string> Images)
{
    private static ArtistImagesState Init() => new ArtistImagesState(new ReadOnlyDictionary<int, string>(new Dictionary<int, string>()));
}
