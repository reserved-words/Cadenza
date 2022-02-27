﻿using Cadenza.Core.Model;
using Cadenza.Core.Playlists;

namespace Cadenza.Core.App;

public interface IItemViewer
{
    Task ViewSearchResult(SourcePlayerItem item);
    Task ViewPlaying(PlaylistId playlist);
    Task ViewGrouping(Grouping id);
    Task ViewGenre(string id);
    Task ViewArtist(Artist artist);
    Task ViewArtist(string id, string name);
    Task ViewArtist(string name);
    Task ViewAlbum(Album album);
    Task ViewTrack(Track track);
    Task ViewTrack(LibrarySource source, string id, string title);
}