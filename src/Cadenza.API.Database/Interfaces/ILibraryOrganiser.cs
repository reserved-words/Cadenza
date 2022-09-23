using Cadenza.API.Common.Model.Json;

namespace Cadenza.API.Common.Interfaces;

internal interface ILibraryOrganiser
{
    void MergeAlbum(List<JsonAlbum> jsonAlbums, List<JsonArtist> jsonArtists, JsonAlbum newAlbum);
    void MergeAlbumTrackLink(List<JsonAlbumTrackLink> jsonAlbumTrackLinks, JsonAlbumTrackLink newAlbumTrackLink);
    void MergeArtist(List<JsonArtist> artists, JsonArtist newArtist);
    void MergeTrack(List<JsonTrack> jsonTracks, List<JsonArtist> jsonArtists, JsonTrack newTrack);
    void RemoveOrphanedItems(JsonItems jsonItems);
    void RemoveTracks(JsonItems jsonData, List<string> filepaths);
}
