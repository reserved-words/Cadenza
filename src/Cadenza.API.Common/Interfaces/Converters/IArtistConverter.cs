using Cadenza.API.Common.Model.Json;
using Cadenza.Domain;

namespace Cadenza.API.Common.Interfaces.Converters;

public interface IArtistConverter
{
    JsonArtist ToJsonModel(ArtistInfo artist);
    ArtistInfo ToAppModel(JsonArtist artist);
}
