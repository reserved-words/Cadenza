﻿namespace Cadenza.API.SqlLibrary.Interfaces;

internal interface ILibraryReader
{
    Task<FullLibrary> Get(LibrarySource? source);
    Task<List<string>> GetAllTracks(LibrarySource source);
    Task<string> GetAlbumArtwork(int id);
    Task<string> GetArtistImage(string nameId);
}
