using Cadenza.Common.LastFm;
using Cadenza.Common.LastFm.Model;
using Cadenza.Database.Interfaces;

namespace Cadenza.SyncService.Updaters;

internal class Scrobbler : IService
{
    private readonly IHistoryRepository _historyRepository;
    private readonly ILastFmService _lastFmService;

    public Scrobbler(IHistoryRepository repository, ILastFmService lastFmService)
    {
        _historyRepository = repository;
        _lastFmService = lastFmService;
    }

    public async Task Run()
    {
        var newScrobbles = await _historyRepository.GetNewScrobbles();

        foreach (var newScrobble in newScrobbles)
        {
            await TryScrobble(newScrobble);
        }
    }

    private async Task TryScrobble(NewScrobbleDTO newScrobble)
    {
        // What if marking as scrobbled fails and keeps failing? Would keep re-resending the same scrobble
        // Safer to mark as scrobbled FIRST and then if scrobbling fails, revert it
        // Make sure to log if reverting it fails, as not reverting it means that will never get scrobbled
        await _historyRepository.MarkScrobbled(newScrobble.Id);

        try
        {
            var scrobble = new Scrobble
            {
                Timestamp = newScrobble.ScrobbledAt,
                Title = newScrobble.Track,
                Artist = newScrobble.Artist,
                AlbumTitle = newScrobble.Album,
                AlbumArtist = newScrobble.AlbumArtist
            };

            await _lastFmService.ScrobbleTrack(newScrobble.SessionKey, scrobble);
        }
        catch (Exception ex)
        {
            // Mark as errored? 
            // Mark number of failed attempts so can stop after max number?
            // await _historyRepository.MarkNotScrobbled(newScrobble.Id);
            // log
            throw;
        }
    }
}