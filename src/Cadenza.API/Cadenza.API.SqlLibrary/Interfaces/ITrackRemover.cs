namespace Cadenza.API.SqlLibrary.Interfaces;

internal interface ITrackRemover
{
    Task RemoveTrack(int id);
    Task RemoveTracks(List<string> idsFromSource);
}
