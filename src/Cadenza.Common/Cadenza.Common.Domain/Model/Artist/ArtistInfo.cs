﻿namespace Cadenza.Common.Domain.Model.Artist;

public class ArtistInfo : Artist
{
    [ItemProperty(ItemProperty.City)]
    public string City { get; set; }

    [ItemProperty(ItemProperty.State)]
    public string State { get; set; }

    [ItemProperty(ItemProperty.Country)]
    public string Country { get; set; }

    [ItemProperty(ItemProperty.ArtistImage)]
    public string ImageUrl { get; set; }

    public TagList Tags { get; set; } = new TagList();
}
