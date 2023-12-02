namespace Cadenza.Web.Actions.Effects;

public class ViewTrackEffects
{
    private readonly ITrackApi _api;

    public ViewTrackEffects(ITrackApi api)
    {
        _api = api;
    }

    [EffectMethod]
    public async Task HandleFetchViewTrackRequest(FetchViewTrackRequest action, IDispatcher dispatcher)
    {
        var track = await _api.GetTrack(action.TrackId);
        dispatcher.Dispatch(new FetchViewTrackResult(track));
    }
}
