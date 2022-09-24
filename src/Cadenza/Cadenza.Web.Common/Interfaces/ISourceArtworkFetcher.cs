﻿using Cadenza.Domain.Enums;

namespace Cadenza.Web.Common.Interfaces;

public interface ISourceArtworkFetcher : IArtworkFetcher
{
    public LibrarySource Source { get; }
}