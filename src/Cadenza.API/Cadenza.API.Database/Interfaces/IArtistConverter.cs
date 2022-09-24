using Cadenza.API.Database.Model;
using Cadenza.Domain;

namespace Cadenza.API.Database.Interfaces;

internal interface IArtistConverter
{
    JsonArtist ToJsonModel(ArtistInfo artist);
    ArtistInfo ToAppModel(JsonArtist artist);
}
