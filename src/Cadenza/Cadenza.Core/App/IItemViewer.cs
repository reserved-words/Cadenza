﻿using Cadenza.Core.Model;

namespace Cadenza.Core;

public interface IItemViewer
{
    Task ViewSearchResult(SourcePlayerItem item);
    Task ViewPlaying(PlaylistId playlist);
    Task ViewAlbum(Album album);
    Task ViewArtist(Artist artist);
    Task ViewArtist(string id, string name);
    Task ViewTrack(Track track);
}