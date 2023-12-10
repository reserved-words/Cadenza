﻿using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.State.Actions;

public record ArtistUpdateRequest(ArtistDetailsVM UpdatedArtist);
public record ArtistUpdatedAction(ArtistDetailsVM UpdatedArtist);
public record ArtistUpdateFailedAction(int ArtistId);