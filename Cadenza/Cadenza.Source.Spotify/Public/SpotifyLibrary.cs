﻿namespace Cadenza.Source.Spotify;

public class SpotifyLibrary : SourceLibrary
{
    public SpotifyLibrary(ILibrary libraryConsumer)
        : base(libraryConsumer, LibrarySource.Spotify)
    {
    }
}

