using Cadenza.API.Database.Model;
using Cadenza.Domain.Models.Album;
using Cadenza.Domain.Models.Artist;
using Cadenza.Domain.Models.Track;

namespace Cadenza.API.Database.Interfaces;

internal interface IModelToJsonConverter
{
    JsonArtist ConvertArtist(ArtistInfo artist);
    JsonAlbum ConvertAlbum(AlbumInfo album);
    JsonTrack ConvertTrack(TrackInfo track);
    JsonAlbumTrackLink ConvertAlbumTrackLink(AlbumTrackLink albumTrackLink);
}