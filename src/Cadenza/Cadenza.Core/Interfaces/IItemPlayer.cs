namespace Cadenza.Core.App;

public interface IItemPlayer
{
    Task PlayAll();
    Task PlayGrouping(Grouping id);
    Task PlayGenre(string id);
    Task PlayArtist(string id);
    Task PlayAlbum(string id);
    Task PlayTrack(string id);
}
