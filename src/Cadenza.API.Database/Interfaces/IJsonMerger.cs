using Cadenza.API.Common.Model.Json;
using Cadenza.Domain;

namespace Cadenza.API.Common.Interfaces;

internal interface IJsonMerger
{
    JsonTrack Merge(JsonTrack existing, JsonTrack update, List<JsonArtist> artists);
    JsonArtist Merge(JsonArtist existing, JsonArtist update);
    JsonAlbum Merge(JsonAlbum existing, JsonAlbum update, List<JsonArtist> artists);
    JsonAlbumTrackLink Merge(JsonAlbumTrackLink existing, JsonAlbumTrackLink update);

    JsonTrack Update(JsonTrack existing, TrackInfo update, List<JsonArtist> artists);
    JsonArtist Update(JsonArtist existing, ArtistInfo update);
    JsonAlbum Update(JsonAlbum existing, AlbumInfo update, List<JsonArtist> artists);
}
