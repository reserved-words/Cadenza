﻿namespace Cadenza.Database.SqlLibrary.Model.Library;

public class GetFullTrackResult
{
    public int SourceId { get; set; }
    public int Id { get; set; }
    public string IdFromSource { get; set; }
    public string TrackTitle { get; set; }
    public bool IsLoved { get; set; }
    public int DurationSeconds { get; set; }
    public string TrackYear { get; set; }
    public string Lyrics { get; set; }
    public string TrackTagList { get; set; }

    public int DiscNo { get; set; }
    public int TrackNo { get; set; }
    public int DiscCount { get; set; }
    public int TrackCount { get; set; }

    public int ArtistId { get; set; }
    public string ArtistName { get; set; }
    public string ArtistGrouping { get; set; }
    public string ArtistGenre { get; set; }
    public string ArtistCity { get; set; }
    public string ArtistState { get; set; }
    public string ArtistCountry { get; set; }
    public string ArtistTagList { get; set; }

    public int AlbumId { get; set; }
    public string AlbumTitle { get; set; }
    public int ReleaseTypeId { get; set; }
    public string AlbumYear { get; set; }
    public string AlbumTagList { get; set; }


    public int AlbumArtistId { get; set; }
    public string AlbumArtistName { get; set; }
    public string AlbumArtistGrouping { get; set; }
    public string AlbumArtistGenre { get; set; }
    public string AlbumArtistCity { get; set; }
    public string AlbumArtistState { get; set; }
    public string AlbumArtistCountry { get; set; }
    public string AlbumArtistTagList { get; set; }
}