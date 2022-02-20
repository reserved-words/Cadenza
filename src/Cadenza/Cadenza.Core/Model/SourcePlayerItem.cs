
namespace Cadenza.Core.Model;

public class SourcePlayerItem : PlayerItem
{
    public SourcePlayerItem(LibrarySource? source, PlayerItem item)
    {
        Type = item.Type;
        Id = item.Id;
        Name = item.Name;
        Artist = item.Artist;
        Album = item.Album;
        Source = source;
    }

    public LibrarySource? Source { get; }
}
