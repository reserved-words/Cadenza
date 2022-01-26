﻿namespace Cadenza.Core;

public class PlaylistPlayer : IPlaylistPlayer
{
    private readonly IPlaylistCreator _playlistCreator;
    private readonly IAppController _app;

    public PlaylistPlayer(IAppController app, IPlaylistCreator playlistCreator)
    {
        _app = app;
        _playlistCreator = playlistCreator;
    }

    public async Task PlayAlbum(string id)
    {
        var playlist = await _playlistCreator.CreateAlbumPlaylist(id);
        await _app.Play(playlist);
    }

    public async Task PlayArtist(string id)
    {
        var playlist = await _playlistCreator.CreateAlbumPlaylist(id);
        await _app.Play(playlist);
    }

    public async Task PlayPlaylist(string id)
    {
        throw new NotImplementedException();
    }

    public async Task PlayTrack(string trackId, string albumId)
    {
        var playlist = await _playlistCreator.CreateTrackPlaylist(trackId, albumId);
        await _app.Play(playlist);
    }

    public async Task PlayAll()
    {
        var playlist = await _playlistCreator.CreateLibraryPlaylist();
        await _app.Play(playlist);
    }
}