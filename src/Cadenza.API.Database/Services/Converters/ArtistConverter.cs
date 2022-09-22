using Cadenza.API.Common.Interfaces.Converters;
using Cadenza.API.Common.Model.Json;
using Cadenza.Domain;

namespace Cadenza.API.Database.Services.Converters;

public class ArtistConverter : IArtistConverter
{
    public ArtistInfo ToAppModel(JsonArtist artist)
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

    public JsonArtist ToJsonModel(ArtistInfo artist)
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