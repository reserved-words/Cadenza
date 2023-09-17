using Cadenza.Web.Common.Model;

namespace Cadenza.State.Actions;

public record PlaylistStartRequest(PlaylistDefinition Definition);

public record PlaylistStopRequest();