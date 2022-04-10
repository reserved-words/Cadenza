using Cadenza.Local.Common.Model.Json;

namespace Cadenza.Local.Common.Interfaces;

public interface ILibraryOrganiser
{
    void MergeAlbum(List<JsonAlbum> jsonAlbums, List<JsonArtist> jsonArtists, JsonAlbum newAlbum);
    void MergeAlbumTrackLink(List<JsonAlbumTrackLink> jsonAlbumTrackLinks, JsonAlbumTrackLink newAlbumTrackLink);
    void MergeArtist(List<JsonArtist> artists, JsonArtist newArtist);
    void MergeTrack(List<JsonTrack> jsonTracks, List<JsonArtist> jsonArtists, JsonTrack newTrack);
    void RemoveOrphanedItems(JsonItems jsonItems);
    void RemoveTracks(JsonItems jsonData, List<string> filepaths);
}
