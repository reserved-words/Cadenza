namespace Cadenza.Core;

public class PlaylistCreator : IPlaylistCreator
{
    private readonly IShuffler _shuffler;
    private readonly IPlayTrackRepository _repository;

    public PlaylistCreator(IShuffler shuffler, IPlayTrackRepository repository)
    {
        _shuffler = shuffler;
        _repository = repository;
    }

    public async Task<PlaylistDefinition> CreateArtistPlaylist(LibraryArtist artist)
    {
        // this is the album tracks, probably need to change this to their actual tracks

        var tracks = await _repository.GetByArtist(artist.Id);

        var shuffledTracks = _shuffler.Shuffle(tracks).ToList();

        return new PlaylistDefinition
        {
            Type = PlaylistType.Artist,
            Name = artist.Name,
            Tracks = shuffledTracks
        };
    }

    public async Task<PlaylistDefinition> CreateAlbumPlaylist(LibraryAlbum album)
    {
        var tracks = await _repository.GetByAlbum(album.Id);

        if (album.ReleaseType == ReleaseType.Playlist)
        {
            tracks = _shuffler.Shuffle(tracks).ToList();
        }

        return new PlaylistDefinition
        {
            Type = PlaylistType.Album,
            Name = $"{album.Title} by {album.Artist}",
            Tracks = tracks
        };
    }

    public async Task<PlaylistDefinition> CreateLibraryPlaylist(string first = null)
    {
        var tracks = await _repository.GetAll();

        var shuffledTracks = _shuffler.Shuffle(tracks).ToList();

        return new PlaylistDefinition
        {
            Type = PlaylistType.All,
            Name = $"All Library",
            Tracks = shuffledTracks
        };
    }
}