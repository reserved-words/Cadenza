using Cadenza.API.Database.Model;
using Cadenza.Domain.Models.Album;
using Cadenza.Domain.Models.Artist;
using Cadenza.Domain.Models.Track;

namespace Cadenza.API.Database.Interfaces;

internal interface IJsonToModelConverter
{
    ArtistInfo ConvertArtist(JsonArtist artist);
    AlbumInfo ConvertAlbum(JsonAlbum album, ICollection<JsonArtist> artists);
    TrackInfo ConvertTrack(JsonTrack track, ICollection<JsonArtist> artists);
    AlbumTrackLink ConvertAlbumTrackLink(JsonAlbumTrackLink albumTrackLink);
}