namespace Cadenza.Core;

public class PlaylistCreator : IPlaylistCreator
{
    private readonly IShuffler _shuffler;
    private readonly IAlbumRepository _albumRepository;
    private readonly IArtistRepository _artistRepository;
    private readonly IPlayTrackRepository _repository;

    public PlaylistCreator(IShuffler shuffler, IPlayTrackRepository repository, IAlbumRepository albumRepository, IArtistRepository artistRepository)
    {
        _shuffler = shuffler;
        _repository = repository;
        _albumRepository = albumRepository;
        _artistRepository = artistRepository;
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

    public async Task<PlaylistDefinition> CreateAlbumPlaylist(string id)
    {
        // may or may not need shuffling

        var album = await _albumRepository.GetAlbum(id);
        var tracks = await _repository.GetByAlbum(id);
        return new PlaylistDefinition
        {
            Type = PlaylistType.Album,
            Name = $"{album.Title} by {album.ArtistName}",
            Tracks = tracks.ToList(),
            MixedSource = false
        };
    }

    public async Task<PlaylistDefinition> CreateTrackPlaylist(string trackId, string albumId)
    {
        var track = (await _repository.GetByAlbum(albumId)).Single(a => a.Id == trackId);
        var artist = await _artistRepository.GetArtist(track.ArtistId);

        var tracks = new List<BasicTrack> { track };

        return new PlaylistDefinition
        {
            Type = PlaylistType.Track,
            Name = $"{track.Title} by {artist.Name}",
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