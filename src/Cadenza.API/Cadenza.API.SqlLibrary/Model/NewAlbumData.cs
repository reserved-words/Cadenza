﻿namespace Cadenza.API.SqlLibrary.Model;

internal class NewAlbumData : AlbumDataBase
{
    public string ArtworkMimeType { get; set; }
    public byte[] ArtworkContent { get; set; }
}