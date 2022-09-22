using Cadenza.API.Common.Model.Json;
using Cadenza.Domain;

namespace Cadenza.API.Common.Interfaces.Converters;

public interface IAlbumConverter
{
    JsonAlbum ToJsonModel(AlbumInfo artist);
    AlbumInfo ToAppModel(JsonAlbum artist, ICollection<JsonArtist> artists);
}