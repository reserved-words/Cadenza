﻿namespace Cadenza.Player;

public interface IPlayerApiUrl
{
    string Scrobble { get; }
    string UpdateNowPlaying { get; }
    string IsFavourite { get; }
    string Favourite { get; }
    string Unfavourite { get; }
}