﻿namespace Cadenza.Domain;

public class BasicTrack
{
    public LibrarySource Source { get; set; }
    public string Id { get; set; }
    public string Title { get; set; }
    public string ArtistId { get; set; }
    public string AlbumId { get; set; }
}
