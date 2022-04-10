using Cadenza.Domain;
using Cadenza.Local.Common.Model.Id3;
using Cadenza.Local.Common.Model.Json;

namespace Cadenza.Local.Common.Interfaces.Converters;

public interface IAlbumConverter
{
    JsonAlbum ToJsonModel(AlbumInfo artist);
    AlbumInfo ToAppModel(JsonAlbum artist, ICollection<JsonArtist> artists);
    JsonAlbum ToJsonModel(Id3Data id3Data);
}