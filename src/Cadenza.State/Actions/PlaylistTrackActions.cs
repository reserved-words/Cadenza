using Cadenza.Common.Domain.Model;

namespace Cadenza.State.Actions;

public record PlaylistTrackUpdateAction(bool IsLoading, PlayTrack Track, bool IsLastTrack);