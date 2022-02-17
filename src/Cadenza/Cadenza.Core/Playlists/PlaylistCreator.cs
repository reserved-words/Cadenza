﻿namespace Cadenza.Core;

public class PlaylistCreator : IPlaylistCreator
{
    private readonly IShuffler _shuffler;
    private readonly IMergedAlbumRepository _albumRepository;
    private readonly IMergedArtistRepository _artistRepository;
    private readonly IMergedPlayTrackRepository _repository;
    private readonly IMergedTrackRepository _trackRepository;

    public PlaylistCreator(IShuffler shuffler, IMergedPlayTrackRepository repository, IMergedArtistRepository artistRepository,
        IMergedAlbumRepository albumRepository, IMergedTrackRepository trackRepository)
    {
        _shuffler = shuffler;
        _repository = repository;
        _artistRepository = artistRepository;
        _albumRepository = albumRepository;
        _trackRepository = trackRepository;
    }

    public async Task<PlaylistDefinition> CreateArtistPlaylist(string id)
    {
        // this is the album tracks, probably need to change this to their actual tracks

        var artist = await _artistRepository.GetArtist(id);
        var tracks = await _repository.GetByArtist(id);

        var firstSource = tracks.First().Source;
        var mixedSource = tracks.Any(t => t.Source != firstSource);

        var shuffledTracks = _shuffler.Shuffle(tracks.ToList());

        return new PlaylistDefinition
        {
            Type = PlaylistType.Artist,
            Name = artist.Name,
            Tracks = shuffledTracks.ToList(),
            MixedSource = mixedSource
        };
    }

    public async Task<PlaylistDefinition> CreateAlbumPlaylist(LibrarySource source, string id)
    {
        var tracks = await _repository.GetByAlbum(id);
        var album = await _albumRepository.GetAlbum(source, id);

        return new PlaylistDefinition
        {
            Type = PlaylistType.Album,
            Name = $"{album.Title} by {album.ArtistName}",
            Tracks = tracks.ToList(),
            MixedSource = false
        };
    }

    public async Task<PlaylistDefinition> CreateTrackPlaylist(LibrarySource source, string id)
    {
        var track = await _trackRepository.GetTrack(source, id);

        var playTrack = new PlayTrack
        {
             Id = id, 
             Source = source,
             ArtistId = track.ArtistId,
             AlbumId = track.AlbumId,
             Title = track.Title
        };

        var tracks = new List<PlayTrack> { playTrack };

        return new PlaylistDefinition
        {
            Type = PlaylistType.Track,
            Name = $"{track.Title} by {track.Artist}",
            Tracks = tracks,
            MixedSource = false
        };
    }

    public async Task<PlaylistDefinition> CreateLibraryPlaylist(string first = null)
    {
        var tracks = await _repository.GetAll();

        var shuffledTracks = _shuffler.Shuffle(tracks.ToList());

        return new PlaylistDefinition
        {
            Type = PlaylistType.All,
            Name = $"All Library",
            Tracks = shuffledTracks.ToList(),
            MixedSource = true
        };
    }
}