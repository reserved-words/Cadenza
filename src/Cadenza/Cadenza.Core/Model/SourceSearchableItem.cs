
namespace Cadenza.Core.Model;

public class SourceSearchableItem : SearchableItem
{
    public SourceSearchableItem(LibrarySource? source, SearchableItem item)
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
