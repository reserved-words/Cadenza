﻿using Cadenza.Common.LastFm.Model;

namespace Cadenza.Common.LastFm;

public interface ILastFmService
{
    Task ScrobbleTrack(string sessionKey, Scrobble scrobble);
    Task UpdateNowPlaying(string sessionKey, NowPlaying nowPlaying);
}
