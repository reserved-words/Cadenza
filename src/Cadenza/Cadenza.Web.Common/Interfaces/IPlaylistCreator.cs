﻿using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.Common.Interfaces;

public interface IPlaylistCreator
{
    Task<PlaylistDefinition> CreateAlbumPlaylist(int id, int startTrackId);
    Task<PlaylistDefinition> CreateArtistPlaylist(int id);
    Task<PlaylistDefinition> CreateGenrePlaylist(string id);
    Task<PlaylistDefinition> CreateGroupingPlaylist(GroupingVM grouping);
    Task<PlaylistDefinition> CreateLibraryPlaylist();
    Task<PlaylistDefinition> CreateTagPlaylist(string id);
    Task<PlaylistDefinition> CreateTrackPlaylist(int id);
}