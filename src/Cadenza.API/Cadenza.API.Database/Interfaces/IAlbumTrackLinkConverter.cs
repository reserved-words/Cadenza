using Cadenza.API.Database.Model;
using Cadenza.Domain.Models.Album;

namespace Cadenza.API.Database.Interfaces;

internal interface IAlbumTrackLinkConverter
{
    JsonAlbumTrackLink ToJsonModel(AlbumTrackLink link);
    AlbumTrackLink ToAppModel(JsonAlbumTrackLink link);
}
