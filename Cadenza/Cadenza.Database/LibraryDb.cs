using IndexedDB.Blazor;
using Microsoft.JSInterop;

namespace Cadenza.Database;

public class LibraryDb : IndexedDb
{
    public LibraryDb(IJSRuntime jSRuntime, string name, int version) 
        : base(jSRuntime, name, version) 
    { 
        
    }
    
    public IndexedSet<DbArtist> Artists { get; set; }
    public IndexedSet<DbAlbum> Albums { get; set; }
    public IndexedSet<DbTrack> Tracks { get; set; }
    public IndexedSet<DbAlbumTrack> AlbumTracks { get; set; }
    public IndexedSet<DbArtistTrack> ArtistTracks { get; set; }
}

