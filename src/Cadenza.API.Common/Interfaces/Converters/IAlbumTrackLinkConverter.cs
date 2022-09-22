using Cadenza.API.Common.Model.Json;
using Cadenza.Domain;

namespace Cadenza.API.Common.Interfaces.Converters;

public interface IAlbumTrackLinkConverter
{
    JsonAlbumTrackLink ToJsonModel(AlbumTrackLink link);
    AlbumTrackLink ToAppModel(JsonAlbumTrackLink link);
}
