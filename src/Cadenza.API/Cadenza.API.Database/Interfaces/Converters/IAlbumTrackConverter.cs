using Cadenza.Domain.Model.Album;

namespace Cadenza.API.Database.Interfaces.Converters;

internal interface IAlbumTrackConverter
{
    JsonAlbumTrack ToJson(AlbumTrackLink link);
    AlbumTrackLink ToModel(JsonAlbumTrack link);
}
