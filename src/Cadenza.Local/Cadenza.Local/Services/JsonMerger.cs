using Cadenza.Library;
using Cadenza.Local.Common.Interfaces;
using Cadenza.Local.Common.Interfaces.Converters;
using Cadenza.Local.Common.Model.Json;

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
        return Merge(modelTrack, modelUpdate, MergeMode.Merge);
    }

    public JsonArtist Merge(JsonArtist artist, JsonArtist update)
    {
        var modelArtist = _artistConverter.ToAppModel(artist);
        var modelUpdate = _artistConverter.ToAppModel(update);
        return Merge(modelArtist, modelUpdate, MergeMode.Merge);
    }

    public JsonAlbum Merge(JsonAlbum album, JsonAlbum update, List<JsonArtist> artists)
    {
        var modelAlbum = _albumConverter.ToAppModel(album, artists);
        var modelUpdate = _albumConverter.ToAppModel(update, artists);
        return Merge(modelAlbum, modelUpdate, MergeMode.Merge);
    }

    public JsonAlbumTrackLink Merge(JsonAlbumTrackLink existing, JsonAlbumTrackLink update)
    {
        existing.AlbumId = _valueMerger.Merge(existing.AlbumId, update.AlbumId, MergeMode.Merge);
        existing.TrackNo = _valueMerger.Merge(existing.TrackNo, update.TrackNo, MergeMode.Merge);
        existing.DiscNo = _valueMerger.Merge(existing.DiscNo, update.DiscNo, MergeMode.Merge);
        return existing;
    }

    public JsonTrack Update(JsonTrack track, TrackInfo update, List<JsonArtist> artists)
    {
        var existingTrack = _trackConverter.ToAppModel(track, artists);
        return Merge(existingTrack, update, MergeMode.Update);
    }

    public JsonArtist Update(JsonArtist artist, ArtistInfo update)
    {
        var existingArtist = _artistConverter.ToAppModel(artist);
        return Merge(existingArtist, update, MergeMode.Update);
    }

    public JsonAlbum Update(JsonAlbum album, AlbumInfo update, List<JsonArtist> artists)
    {
        var existingAlbum = _albumConverter.ToAppModel(album, artists);
        return Merge(existingAlbum, update, MergeMode.Update);
    }

    private JsonTrack Merge(TrackInfo existing, TrackInfo update, MergeMode mode)
    {
        _merger.MergeTrack(update, existing, mode);
        return _trackConverter.ToJsonModel(existing);
    }

    private JsonArtist Merge(ArtistInfo existing, ArtistInfo update, MergeMode mode)
    {
        _merger.MergeArtist(existing, update, mode);
        return _artistConverter.ToJsonModel(existing);
    }

    private JsonAlbum Merge(AlbumInfo existing, AlbumInfo update, MergeMode mode)
    {
        _merger.MergeAlbum(existing, update, mode);
        return _albumConverter.ToJsonModel(existing);
    }
}
