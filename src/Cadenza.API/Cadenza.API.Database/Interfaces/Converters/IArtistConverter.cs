using Cadenza.Common.Domain.Model.Artist;

namespace Cadenza.API.Database.Interfaces.Converters;

internal interface IArtistConverter
{
    JsonArtist ToJson(ArtistInfo artist);
    ArtistInfo ToModel(JsonArtist artist);
}
