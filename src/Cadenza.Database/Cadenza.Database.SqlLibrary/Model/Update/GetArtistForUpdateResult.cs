﻿namespace Cadenza.Database.SqlLibrary.Model.Update;

internal class GetArtistForUpdateResult
{
    public string Grouping { get; set; }
    public string Genre { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string ImageMimeType { get; set; }
    public byte[] ImageContent { get; set; }
    public string TagList { get; set; }
}
