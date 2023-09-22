﻿using Cadenza.Common.Domain.Model.Update;

namespace Cadenza.State.Actions;

public record ArtistUpdateRequest(int ArtistId, ArtistUpdate Update);
public record ArtistUpdatedAction(int ArtistId, ArtistUpdate Update);
public record ArtistUpdateFailedAction(int ArtistId);