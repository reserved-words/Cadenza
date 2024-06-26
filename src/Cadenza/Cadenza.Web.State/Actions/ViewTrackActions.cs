﻿using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.State.Actions;

public record FetchViewTrackRequest(int TrackId);

public record FetchViewTrackResult(TrackFullVM Track);
