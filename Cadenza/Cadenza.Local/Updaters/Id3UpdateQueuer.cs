namespace Cadenza.Local;

public class Id3UpdateQueuer : ILibraryUpdater
{
    private readonly IFileUpdateService _queue;

    public Id3UpdateQueuer(IFileUpdateService queue)
    {
        _queue = queue;
    }

    public async Task<bool> UpdateAlbum(AlbumUpdate album)
    {
        foreach (var u in album.Updates)
        {
            _queue.Add(u);
        }

        return true;
    }

    public async Task<bool> UpdateArtist(ArtistUpdate artist)
    {
        foreach (var u in artist.Updates)
        {
            _queue.Add(u);
        }

        return true;
    }

    public async Task<bool> UpdateTrack(TrackUpdate track)
    {
        foreach (var u in track.Updates)
        {
            _queue.Add(u);
        }

        return true;
    }
}