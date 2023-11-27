namespace Cadenza.Web.State.Actions;

public record FavouriteRequest(int TrackId);
public record UnfavouriteRequest(int TrackId);
public record FavouriteStatusChangedAction(int TrackId, bool IsFavourite);