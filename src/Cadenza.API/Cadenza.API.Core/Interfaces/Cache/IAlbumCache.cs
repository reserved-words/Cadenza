﻿using Cadenza.Library;

namespace Cadenza.API.Core.Interfaces.Cache;

internal interface IAlbumCache : IAlbumRepository
{
    Task Populate(FullLibrary library);
    Task UpdateAlbum(AlbumUpdate update);
}
