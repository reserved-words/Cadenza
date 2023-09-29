﻿using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.State.Actions;

public record FetchViewArtistRequest(int ArtistId);

public record FetchViewArtistResult(ArtistInfo Artist, List<ArtistReleaseGroup> Releases);
