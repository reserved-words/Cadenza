﻿namespace Cadenza.Common;

public class TrackInfo : Track
{
    [ItemProperty(ItemProperty.TrackYear)]
    public string Year { get; set; }

    [ItemProperty(ItemProperty.Lyrics)]
    public string Lyrics { get; set; }

    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
}