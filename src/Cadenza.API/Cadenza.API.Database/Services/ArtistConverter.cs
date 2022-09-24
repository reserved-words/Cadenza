using Cadenza.API.Database.Interfaces;
using Cadenza.API.Database.Model;
using Cadenza.Domain;

namespace Cadenza.API.Database.Services;

internal class ArtistConverter : IArtistConverter
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