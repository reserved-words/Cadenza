using Cadenza.Domain;
using Cadenza.Local.Common.Model.Id3;
using Cadenza.Local.Common.Model.Json;

namespace Cadenza.Local.Common.Interfaces.Converters;

public interface IAlbumTrackLinkConverter
{
    JsonAlbumTrackLink ToJsonModel(AlbumTrackLink link);
    AlbumTrackLink ToAppModel(JsonAlbumTrackLink link);
    JsonAlbumTrackLink ToJsonModel(Id3Data id3Data);
}
