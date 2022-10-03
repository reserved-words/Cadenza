namespace Cadenza.Local.API.Files;

internal class MusicService : IMusicFilesService
{
    private readonly IId3Fetcher _fetcher;
    private readonly IId3Updater _updater;

    public MusicService(IId3Updater updater, IId3Fetcher fetcher)
    {
        _updater = updater;
        _fetcher = fetcher;
    }

    public TrackFull GetFileData(string filepath)
    {
        return _fetcher.GetFileData(filepath);
    }

    public void UpdateFileData(string filepath, List<PropertyUpdate> updates)
    {
        _updater.UpdateTags(filepath, updates);
    }
}