namespace Cadenza.Web.Common.Interfaces.Play;

public interface IItemPlayer
{
    Task PlayAll();
    Task PlayAlbum(int id, int startTrackId = 0);
    Task PlayArtist(int id);
    Task PlayGenre(string id);
    Task PlayGrouping(Grouping id);
    Task PlayTag(string id);
    Task PlayTrack(int id);
}
