﻿using Cadenza.Domain;

namespace Cadenza.Core;

public interface ITrackRepository
{
    Task<PlayingTrack> GetSummary(LibrarySource source, string id);
    Task<FullTrack> GetDetails(LibrarySource source, string id);
    Task<List<AlbumTrackInfo>> GetAlbumTracks(LibrarySource source, string id);
}