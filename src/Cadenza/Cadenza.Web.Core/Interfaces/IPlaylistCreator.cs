﻿namespace Cadenza.Web.Core.Interfaces;

internal interface IPlaylistCreator
{
    Task<PlaylistDefinition> CreateAlbumPlaylist(int id, string startTrackId);
    Task<PlaylistDefinition> CreateArtistPlaylist(int id);
    Task<PlaylistDefinition> CreateGenrePlaylist(string id);
    Task<PlaylistDefinition> CreateGroupingPlaylist(Grouping id);
    Task<PlaylistDefinition> CreateLibraryPlaylist(string first = null);
    Task<PlaylistDefinition> CreateTagPlaylist(string id);
    Task<PlaylistDefinition> CreateTrackPlaylist(string id);
}