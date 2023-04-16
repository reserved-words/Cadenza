namespace Cadenza.API.SqlLibrary.Interfaces;

internal interface ITrackRemover
{
    Task RemoveTracks(List<string> id);
}
