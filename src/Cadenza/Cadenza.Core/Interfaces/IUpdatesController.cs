﻿namespace Cadenza.Core.App;

public interface IUpdatesController
{
    Task UpdateArtist(ArtistUpdate artist);
}