using Cadenza.Domain;
using Cadenza.Local.Common.Model.Id3;
using Cadenza.Local.Common.Model.Json;

namespace Cadenza.Local.Common.Interfaces.Converters;

public interface IArtistConverter
{
    JsonArtist ToJsonModel(ArtistInfo artist);
    ArtistInfo ToAppModel(JsonArtist artist);
    JsonArtist ToJsonModel(Id3Data data, bool albumArtist);
}
