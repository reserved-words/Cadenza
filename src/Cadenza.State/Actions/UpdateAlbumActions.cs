using Cadenza.Common.Domain.Model.Update;

namespace Cadenza.State.Actions;

public record AlbumUpdateRequest(int AlbumId, AlbumUpdate Update);
public record AlbumUpdatedAction(int AlbumId, AlbumUpdate Update);
public record AlbumUpdateFailedAction(int AlbumId);