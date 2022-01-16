using Cadenza.Library;

namespace Cadenza.Local;

public class Id3UpdateQueuer : ILibraryUpdater
{
    private readonly IFileUpdateService _queue;

    public Id3UpdateQueuer(IFileUpdateService queue)
    {
        _queue = queue;
    }

    public async Task<bool> Update(AlbumInfo album, List<ItemPropertyUpdate> updates)
    {
        foreach (var u in updates)
        {
            _queue.Add(u);
        }

        return true;
    }

    public async Task<bool> Update(ArtistInfo artist, List<ItemPropertyUpdate> updates)
    {
        foreach (var u in updates)
        {
            _queue.Add(u);
        }

        return true;
    }

    public async Task<bool> Update(TrackInfo track, List<ItemPropertyUpdate> updates)
    {
        foreach (var u in updates)
        {
            _queue.Add(u);
        }

        return true;
    }
}