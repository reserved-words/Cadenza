﻿using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.State.Actions;

public record PlayStatusPausedAction(TrackFull Track, int SecondsPlayed);

public record PlayStatusResumedAction(TrackFull Track, int SecondsPlayed);

public record PlayStatusStoppedAction(TrackFull Track, int SecondsPlayed);

public record PlayStatusPlayingAction(TrackFull Track);