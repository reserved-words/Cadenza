﻿namespace Cadenza.Local;

public class JsonArtist
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Grouping { get; set; }
    public string Genre { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public List<JsonLink> Links { get; set; } = new List<JsonLink>();

}
