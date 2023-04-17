﻿using Cadenza.Common.Domain.Model.Album;
using Cadenza.Common.Domain.Model.Track;

namespace Cadenza.Common.Interfaces.Repositories;

public interface IAlbumRepository
{
    Task<AlbumInfo> GetAlbum(int id);
    Task<List<AlbumTrack>> GetAlbumTracks(int albumId);
}
