namespace Cadenza.Core.App;

public interface IItemPlayer
{
    Task PlayAll();
    Task PlayGrouping(Grouping id);
    Task PlayGenre(string id);
    Task PlayArtist(string id);
    Task PlayAlbum(LibrarySource source, string id);
    Task PlayTrack(LibrarySource source, string id);
}
