namespace Cadenza.API.SqlLibrary.Interfaces;

internal interface ITrackRemover
{
    Task RemoveTracks(LibrarySource source, List<string> id);
}
