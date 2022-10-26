﻿namespace Cadenza.API.Database.Services.Converters;

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
            Country = artist.Country,
            ImageUrl = artist.ImageUrl,
            Tags = new TagList(artist.Tags)
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
            Country = artist.Country.Nullify(),
            ImageUrl = artist.ImageUrl.Nullify(),
            Tags = artist.Tags.ToString().Nullify()
        };
    }
}