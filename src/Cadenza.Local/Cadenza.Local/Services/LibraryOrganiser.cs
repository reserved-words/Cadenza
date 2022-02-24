namespace Cadenza.Local;

public class LibraryOrganiser : ILibraryOrganiser
{
    private readonly IListComparer _listComparer;
    private readonly IJsonMerger _merger;

    public LibraryOrganiser(IJsonMerger merger, IListComparer listComparer)
    {
        _merger = merger;
        _listComparer = listComparer;
    }

    public void MergeAlbumTrackLink(List<JsonAlbumTrackLink> jsonAlbumTrackLinks, JsonAlbumTrackLink newAlbumTrackLink)
    {
        var existingTrackLink = jsonAlbumTrackLinks.SingleOrDefault(a => a.TrackPath == newAlbumTrackLink.TrackPath);
        if (existingTrackLink == null)
        {
            existingTrackLink = newAlbumTrackLink;
        }
        else
        {
            jsonAlbumTrackLinks.Remove(existingTrackLink);
        }
        existingTrackLink = _merger.Merge(existingTrackLink, newAlbumTrackLink);
        jsonAlbumTrackLinks.Add(existingTrackLink);
    }

    public void MergeAlbum(List<JsonAlbum> jsonAlbums, List<JsonArtist> jsonArtists, JsonAlbum newAlbum)
    {
        var existingAlbum = jsonAlbums.SingleOrDefault(t => t.Id == newAlbum.Id);
        if (existingAlbum == null)
        {
            existingAlbum = newAlbum;
        }
        else
        {
            jsonAlbums.Remove(existingAlbum);
        }
        existingAlbum = _merger.Merge(existingAlbum, newAlbum, jsonArtists);
        jsonAlbums.Add(existingAlbum);
    }

    public void MergeTrack(List<JsonTrack> jsonTracks, List<JsonArtist> jsonArtists, JsonTrack newTrack)
    {
        var existingTrack = jsonTracks.SingleOrDefault(t => t.Path == newTrack.Path);
        if (existingTrack == null)
        {
            existingTrack = newTrack;
        }
        else
        {
            jsonTracks.Remove(existingTrack);
        }
        existingTrack = _merger.Merge(existingTrack, newTrack, jsonArtists);
        jsonTracks.Add(existingTrack);
    }

    public void MergeArtist(List<JsonArtist> artists, JsonArtist newArtist)
    {
        var existingTrackArtist = artists.SingleOrDefault(t => t.Id == newArtist.Id);
        if (existingTrackArtist == null)
        {
            existingTrackArtist = newArtist;
        }
        else
        {
            artists.Remove(existingTrackArtist);
        }
        existingTrackArtist = _merger.Merge(existingTrackArtist, newArtist);
        artists.Add(existingTrackArtist);
    }

    public void RemoveOrphanedItems(JsonItems jsonItems)
    {
        var allAlbumIDs = jsonItems.Albums
            .Select(a => a.Id)
            .ToList();

        var allArtistIDs = jsonItems.Artists
            .Select(a => a.Id)
            .ToList();

        var albumIDsToKeep = jsonItems.AlbumTrackLinks
            .Select(t => t.AlbumId)
            .Distinct()
            .ToList();

        var albumArtistsToKeep = jsonItems.Albums
            .Where(a => albumIDsToKeep.Contains(a.Id))
            .Select(a => a.ArtistId)
            .Distinct();

        var artistsToKeep = jsonItems.Tracks
            .Select(t => t.ArtistId)
            .Union(albumArtistsToKeep)
            .Distinct()
            .ToList();

        var albumsToRemove = _listComparer.GetMissingItems(allAlbumIDs, albumIDsToKeep);
        var artistsToRemove = _listComparer.GetMissingItems(allArtistIDs, artistsToKeep);

        foreach (var id in albumsToRemove)
        {
            var album = jsonItems.Albums.SingleOrDefault(a => a.Id == id);
            if (album != null)
            {
                jsonItems.Albums.Remove(album);
            }
        }

        foreach (var id in artistsToRemove)
        {
            var artist = jsonItems.Artists.SingleOrDefault(a => a.Id == id);
            if (artist != null)
            {
                jsonItems.Artists.Remove(artist);
            }
        }
    }

    public void RemoveTracks(JsonItems jsonData, List<string> filepaths)
    {
        foreach (var path in filepaths)
        {
            var track = jsonData.Tracks.SingleOrDefault(t => t.Path == path);
            if (track != null)
            {
                jsonData.Tracks.Remove(track);
            }
            var albumTrackLink = jsonData.AlbumTrackLinks.SingleOrDefault(t => t.TrackPath == path);
            if (track != null)
            {
                jsonData.AlbumTrackLinks.Remove(albumTrackLink);
            }
        }

        RemoveOrphanedItems(jsonData);
    }
}
