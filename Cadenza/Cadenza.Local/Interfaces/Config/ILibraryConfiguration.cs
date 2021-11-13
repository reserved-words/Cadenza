namespace Cadenza.Local;

public interface ILibraryConfiguration
{
    string LibraryArtistsPath { get; }
    string LibraryAlbumsPath { get; }
    string LibraryTracksPath { get; }
    string LibraryAlbumTrackLinksPath { get; }
    string LibraryUpdatePath { get; }

    string CurrentlyPlayingLocation { get; }
    string CurrentlyPlayingPrefix { get; }
    List<string> FileExtensions { get; }
    string UpdateQueueFilePath { get; }
}
