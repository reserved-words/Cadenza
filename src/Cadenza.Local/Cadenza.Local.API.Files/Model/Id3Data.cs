﻿namespace Cadenza.Local.API.Files.Model;

internal class Id3Data
{
    public TrackId3Data Track { get; set; }
    public ArtistId3Data Artist { get; set; }
    public AlbumId3Data Album { get; set; }
    public DiscId3Data Disc { get; set; }
}
