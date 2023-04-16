namespace Cadenza.Web.Common.Interfaces.Play;

public interface IItemPlayer
{
    Task PlayAll();
    Task PlayAlbum(int id, string startTrackId = null);
    Task PlayArtist(int id);
    Task PlayGenre(string id);
    Task PlayGrouping(Grouping id);
    Task PlayTag(string id);
    Task PlayTrack(string id);
}
