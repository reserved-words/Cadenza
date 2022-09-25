using Cadenza.API.Database.Model;
using Cadenza.Domain.Models.Artist;

namespace Cadenza.API.Database.Interfaces.Converters;

internal interface IArtistConverter
{
    JsonArtist ToJson(ArtistInfo artist);
    ArtistInfo ToModel(JsonArtist artist);
}
