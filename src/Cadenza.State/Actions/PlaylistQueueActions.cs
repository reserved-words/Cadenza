using Cadenza.Common.Domain.Model;
using Cadenza.Web.Common.Model;

namespace Cadenza.State.Actions;

public record PlaylistQueueUpdateRequest(PlaylistDefinition Definition);
public record PlaylistQueueMoveNextRequest();
public record PlaylistQueueMovePreviousRequest();
public record PlaylistQueueRemoveTrackRequest(int TrackId);