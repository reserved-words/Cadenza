using Cadenza.Core.Model;

namespace Cadenza.Core;

public interface IItemViewer
{
    Task ViewSearchResult(SourceSearchableItem item);
    Task ViewAlbum(Album album);
}