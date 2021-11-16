﻿namespace Cadenza.Local;

public class JsonLibrary : IStaticSource
{
    private readonly IDataAccess _dataAccess;
    private readonly IJsonToModelConverter _converter;

    public JsonLibrary(IDataAccess dataAccess, IJsonToModelConverter converter)
    {
        _dataAccess = dataAccess;
        _converter = converter;
    }

    public async Task<StaticLibrary> GetStaticLibrary()
    {
        var jsonArtists = _dataAccess.GetArtists();
        var jsonAlbums = _dataAccess.GetAlbums();
        var jsonTracks = _dataAccess.GetTracks();
        var jsonAlbumTrackLinks = _dataAccess.GetAlbumTrackLinks();

        var library = new StaticLibrary();

        foreach (var jsonArtist in jsonArtists)
        {
            try
            {
                var artist = _converter.ConvertArtist(jsonArtist);
                artist.AddSourceId(Source.Local, artist.Id);
                library.Artists.Add(artist);
            }
            catch (System.Exception ex)
            {

                throw;
            }
        }

        foreach (var jsonAlbum in jsonAlbums)
        {
            var album = _converter.ConvertAlbum(jsonAlbum, jsonArtists);
            album.Source = Source.Local;
            library.Albums.Add(album);
        }

        foreach (var jsonTrack in jsonTracks)
        {
            var track = _converter.ConvertTrack(jsonTrack, jsonArtists);
            track.Source = Source.Local;
            library.Tracks.Add(track);
        }

        foreach (var jsonAlbumTrackLink in jsonAlbumTrackLinks)
        {
            var albumTrack = _converter.ConvertAlbumTrackLink(jsonAlbumTrackLink);
            library.AlbumTrackLinks.Add(albumTrack);
        }

        return library;
    }
}