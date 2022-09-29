using Cadenza.Common.Domain.Enums;

namespace Cadenza.API.Database.Interfaces;

internal interface IFilePathService
{
    string Albums(LibrarySource source);
    string AlbumTracks(LibrarySource source);
    string Artists();
    string Tracks(LibrarySource source);
    string Updates(LibrarySource source);
}
