﻿using Cadenza.Common.Enums;

namespace Cadenza.Web.Model;

public record ArtistReleaseGroupVM(ReleaseTypeGroup Group, IReadOnlyCollection<AlbumVM> Albums);