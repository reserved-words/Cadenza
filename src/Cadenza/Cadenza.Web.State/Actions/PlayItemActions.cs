namespace Cadenza.Web.State.Actions;

public record PlayAlbumRequest(int Id, int StartTrackId);
public record PlayArtistRequest(int Id);
public record PlayGenreRequest(string Id);
public record PlayGroupingRequest(string Grouping);
public record PlayTagRequest(string Id);
public record PlayTrackRequest(int Id);
public record PlayAllRequest();