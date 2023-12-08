namespace Cadenza.Web.Actions.Effects;

public class EditTrackEffects
{
    private readonly ILibraryApi _api;

    public EditTrackEffects(ILibraryApi api)
    {
        _api = api;
    }

    [EffectMethod]
    public async Task HandleFetchEditTrackRequest(FetchEditTrackRequest action, IDispatcher dispatcher)
    {
        var track = await _api.GetTrack(action.TrackId);
        dispatcher.Dispatch(new FetchEditTrackResult(track.Track));
    }
}
