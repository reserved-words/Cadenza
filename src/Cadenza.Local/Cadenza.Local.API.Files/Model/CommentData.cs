﻿namespace Cadenza.Local.API.Files.Model;

internal class CommentData
{
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string TrackYear { get; set; }
    public TagList Tags { get; set; } // Track tags - leave name as Tags
}