﻿using Cadenza.Domain.Model;

namespace Cadenza.API.Interfaces.Controllers;

public interface IPlayTrackService
{
    Task<List<PlayTrack>> GetPlayTracks();
    Task<List<PlayTrack>> GetPlayTracksByArtist(string id);
    Task<List<PlayTrack>> GetPlayTracksByAlbum(string id);
    Task<List<PlayTrack>> GetPlayTracksByGenre(string id);
    Task<List<PlayTrack>> GetPlayTracksByGrouping(Grouping id);
}
