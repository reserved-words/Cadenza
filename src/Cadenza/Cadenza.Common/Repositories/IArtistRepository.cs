﻿using Cadenza.Domain;

namespace Cadenza.Common;

public interface IArtistRepository
{
    Task<List<LibraryArtist>> GetAlbumArtists();
    Task<LibraryArtistDetails> GetArtist(string id);
}
