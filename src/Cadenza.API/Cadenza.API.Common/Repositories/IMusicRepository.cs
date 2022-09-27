﻿using Cadenza.API.Common.Model;
using Cadenza.Domain.Enums;
using Cadenza.Domain.Models.Track;
using Cadenza.Domain.Models.Updates;

namespace Cadenza.API.Common.Repositories;

public interface IMusicRepository
{
    Task<FullLibrary> Get(LibrarySource? source);
    Task RemoveTracks(LibrarySource source, List<string> id);
    Task UpdateArtist(ItemUpdates updates);
    Task UpdateAlbum(LibrarySource source, ItemUpdates updates);
    Task UpdateTrack(LibrarySource source, ItemUpdates updates);
    Task AddTrack(LibrarySource source, TrackFull track);
}
