using Cadenza.API.Common.Model.Json;
using Cadenza.Domain;

namespace Cadenza.API.Common.Interfaces.Converters;

public interface IJsonToModelConverter
{
    ArtistInfo ConvertArtist(JsonArtist artist);
    AlbumInfo ConvertAlbum(JsonAlbum album, ICollection<JsonArtist> artists);
    TrackInfo ConvertTrack(JsonTrack track, ICollection<JsonArtist> artists);
    AlbumTrackLink ConvertAlbumTrackLink(JsonAlbumTrackLink albumTrackLink);
}