namespace Cadenza.Web.State.Store;

[FeatureState(CreateInitialStateMethodName = nameof(Init))]
public record AlbumArtworkState(IReadOnlyDictionary<int, string> Images)
{
    private static AlbumArtworkState Init() => new AlbumArtworkState(new ReadOnlyDictionary<int, string>(new Dictionary<int, string>()));
}
