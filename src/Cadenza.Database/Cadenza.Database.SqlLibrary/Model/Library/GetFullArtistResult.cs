﻿namespace Cadenza.Database.SqlLibrary.Model.Library;

public class GetFullArtistResult
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Grouping { get; set; }
    public string Genre { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string TagList { get; set; }
}