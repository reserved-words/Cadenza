﻿namespace Cadenza.Common.Domain.Model.Library;

public class AlbumDetails : Album
{
    public int DiscCount { get; set; }

    public List<int> TrackCounts { get; set; } = new List<int>();

    [ItemProperty(ItemProperty.AlbumTags)]
    public TagList Tags { get; set; } = new TagList();
}