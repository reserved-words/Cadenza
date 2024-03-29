﻿namespace Cadenza.Web.State.Actions;

public record PlaylistQueueUpdateRequest(PlaylistDefinition Definition);
public record PlaylistQueueMoveNextRequest();
public record PlaylistQueueMovePreviousRequest();
public record PlaylistQueueRemoveTrackRequest(int TrackId);