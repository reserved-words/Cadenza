﻿namespace Cadenza.Source.Spotify.Player;

public class SpotifyApiPlayState
{
    public bool is_playing { get; set; }
    public int? progress_ms { get; set; }
    public SpotifyApiPlayStateItem item { get; set; }

}
