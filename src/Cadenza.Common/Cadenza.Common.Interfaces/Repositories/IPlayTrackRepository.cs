﻿using Cadenza.Common.Domain.Enums;
using Cadenza.Common.Domain.Model;

namespace Cadenza.Common.Interfaces.Repositories;

public interface IPlayTrackRepository
{
    Task<List<PlayTrack>> PlayAll();
    Task<List<PlayTrack>> PlayAlbum(string id);
    Task<List<PlayTrack>> PlayArtist(string id);
    Task<List<PlayTrack>> PlayGenre(string id);
    Task<List<PlayTrack>> PlayGrouping(Grouping id);
    Task<List<PlayTrack>> PlayTag(string id);
}
