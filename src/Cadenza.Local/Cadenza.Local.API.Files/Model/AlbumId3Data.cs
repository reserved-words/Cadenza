﻿namespace Cadenza.Local.API.Files.Model;

internal class AlbumId3Data
{
    public string Title { get; set; }
    public string SortTitle { get; set; }
    public string ArtistName { get; set; }
    public string ArtistSortName { get; set; }
    public int DiscCount { get; set; }
    public string Year { get; set; }
    public string ReleaseType { get; set; }
    public ArtworkImage Artwork { get; set; }
}