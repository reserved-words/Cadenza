namespace Cadenza.Web.State.Actions;

public record FavouriteRequest(string Artist, string Title);
public record FavouriteStatusChangedAction(string Artist, string Title, bool IsFavourite);

public record IsFavouriteRequest(string Artist, string Title);
public record IsFavouriteResultAction(string Artist, string Title, bool Result);

public record UnfavouriteRequest(string Artist, string Title);