using Cadenza.API.Database.Interfaces.Converters;
using Cadenza.API.Database.Model;
using Cadenza.Domain.Enums;
using Cadenza.Domain.Extensions;
using Cadenza.Domain.Models.Artist;

namespace Cadenza.API.Database.Converters;

internal class ArtistConverter : IArtistConverter
{
    public ArtistInfo ToModel(JsonArtist artist)
    {
        return new ArtistInfo
        {
            Id = artist.Id,
            Name = artist.Name,
            Grouping = artist.Grouping.Parse<Grouping>(),
            Genre = artist.Genre,
            City = artist.City,
            State = artist.State,
            Country = artist.Country
        };
    }

    public JsonArtist ToJson(ArtistInfo artist)
    {
        return new JsonArtist
        {
            Id = artist.Id,
            Name = artist.Name,
            Grouping = artist.Grouping.ToString(),
            Genre = artist.Genre.Nullify(),
            City = artist.City.Nullify(),
            State = artist.State.Nullify(),
            Country = artist.Country.Nullify()
        };
    }
}