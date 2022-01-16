using Cadenza.Library;

namespace Cadenza.Local;

public class JsonMerger : IJsonMerger
{
    private readonly IAlbumConverter _albumConverter;
    private readonly IArtistConverter _artistConverter;
    private readonly ITrackConverter _trackConverter;

    private readonly IMerger _merger;
    private readonly IValueMerger _valueMerger;

    public JsonMerger(ITrackConverter trackConverter, IAlbumConverter albumConverter, IArtistConverter artistConverter, IMerger merger, IValueMerger valueMerger)
    {
        _trackConverter = trackConverter;
        _albumConverter = albumConverter;
        _artistConverter = artistConverter;
        _merger = merger;
        _valueMerger = valueMerger;
    }

    public JsonTrack Merge(JsonTrack track, JsonTrack update, List<JsonArtist> artists)
    {
        var modelTrack = _trackConverter.ToAppModel(track, artists);
        var modelUpdate = _trackConverter.ToAppModel(update, artists);
        return Merge(modelTrack, modelUpdate, false);
    }

    public JsonArtist Merge(JsonArtist artist, JsonArtist update)
    {
        var modelArtist = _artistConverter.ToAppModel(artist);
        var modelUpdate = _artistConverter.ToAppModel(update);
        return Merge(modelArtist, modelUpdate, false);
    }

    public JsonAlbum Merge(JsonAlbum album, JsonAlbum update, List<JsonArtist> artists)
    {
        var modelAlbum = _albumConverter.ToAppModel(album, artists);
        var modelUpdate = _albumConverter.ToAppModel(update, artists);
        return Merge(modelAlbum, modelUpdate, false);
    }

    public JsonAlbumTrackLink Merge(JsonAlbumTrackLink existing, JsonAlbumTrackLink update)
    {
        existing.AlbumId = _valueMerger.Merge(existing.AlbumId, update.AlbumId, false);
        existing.TrackNo = _valueMerger.Merge(existing.TrackNo, update.TrackNo, false);
        existing.DiscNo = _valueMerger.Merge(existing.DiscNo, update.DiscNo, false);
        return existing;
    }

    public JsonTrack Update(JsonTrack track, TrackInfo update, List<JsonArtist> artists)
    {
        var existingTrack = _trackConverter.ToAppModel(track, artists);
        return Merge(existingTrack, update, true);
    }

    public JsonArtist Update(JsonArtist artist, ArtistInfo update)
    {
        var existingArtist = _artistConverter.ToAppModel(artist);
        return Merge(existingArtist, update, true);
    }

    public JsonAlbum Update(JsonAlbum album, AlbumInfo update, List<JsonArtist> artists)
    {
        var existingAlbum = _albumConverter.ToAppModel(album, artists);
        return Merge(existingAlbum, update, true);
    }

    private JsonTrack Merge(TrackInfo existing, TrackInfo update, bool forceUpdate)
    {
        _merger.MergeTrack(update, existing, forceUpdate);
        return _trackConverter.ToJsonModel(existing);
    }

    private JsonArtist Merge(ArtistInfo existing, ArtistInfo update, bool forceUpdate)
    {
        _merger.MergeArtist(existing, update, forceUpdate);
        return _artistConverter.ToJsonModel(existing);
    }

    private JsonAlbum Merge(AlbumInfo existing, AlbumInfo update, bool forceUpdate)
    {
        _merger.MergeAlbum(existing, update, forceUpdate);
        return _albumConverter.ToJsonModel(existing);
    }
}
