using Cadenza.Core.Model;

namespace Cadenza.Core;

public interface IItemViewer
{
    Task ViewSearchResult(SourcePlayerItem item);
    Task ViewAlbum(Album album);
}