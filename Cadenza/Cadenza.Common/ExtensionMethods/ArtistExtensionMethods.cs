namespace Cadenza.Common;

public static class ArtistExtensionMethods
{
    public static bool IsInSource(this Artist artist, LibrarySource source)
    {
        return artist.SourceIds.Any(s => s.Source == source);
    }

    public static bool IsInAnySource(this Artist artist, IEnumerable<LibrarySource> sources)
    {
        return sources.Any(s => artist.IsInSource(s));
    }
}