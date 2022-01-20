﻿using Cadenza.Domain;

namespace Cadenza.Database;

public class DbAlbum
{
    [Key]
    public string Id { get; set; }
    public string ArtistId { get; set; }
    public string ArtistName { get; set; }
    public string Title { get; set; }
    public string Artwork { get; set; }
    public string Year { get; set; }
    public string ReleaseType { get; set; }
    public string Source { get; set; }
}