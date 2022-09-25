using Cadenza.API.Database.Model;
using Cadenza.Domain.Models.Album;

namespace Cadenza.API.Database.Interfaces.Converters;

internal interface IAlbumTrackLinkConverter
{
    JsonAlbumTrackLink ToJson(AlbumTrackLink link);
    AlbumTrackLink ToModel(JsonAlbumTrackLink link);
}
