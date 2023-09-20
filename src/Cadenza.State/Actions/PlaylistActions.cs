using Cadenza.Web.Common.Model;

namespace Cadenza.State.Actions;

public record PlaylistStartRequest(PlaylistDefinition Definition);
public record PlaylistMoveNextRequest();
public record PlaylistMovePreviousRequest();
public record PlaylistStopRequest();