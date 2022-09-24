using Cadenza.API.Database.Model;
using Cadenza.Domain;

namespace Cadenza.API.Database.Interfaces;

internal interface IAlbumTrackLinkConverter
{
    JsonAlbumTrackLink ToJsonModel(AlbumTrackLink link);
    AlbumTrackLink ToAppModel(JsonAlbumTrackLink link);
}
