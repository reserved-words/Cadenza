﻿using Cadenza.Common;
using IndexedDB.Blazor;

namespace Cadenza.Database;

public class MainRepository : IMainRepository
{
    private readonly IIndexedDbFactory _dbFactory;

    public MainRepository(IIndexedDbFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task AddAlbums(ICollection<AlbumInfo> albums)
    {
        using var db = await _dbFactory.Create<LibraryDb>();

        foreach (var album in albums)
        {
            AddAlbum(db, album);
        }

        await db.SaveChanges();
    }

    public async Task AddAlbumTrackLinks(ICollection<AlbumTrackLink> tracks)
    {
        using var db = await _dbFactory.Create<LibraryDb>();

        await db.SaveChanges();
    }

    public async Task AddArtists(ICollection<ArtistInfo> artists)
    {
        using var db = await _dbFactory.Create<LibraryDb>();

        var alreadyPopulated = db.Artists.Any();

        if (alreadyPopulated)
        {
            foreach (var artist in artists)
            {
                AddOrUpdateArtist(db, artist);
            }
        }
        else
        {
            foreach (var artist in artists)
            {
                AddArtist(db, artist);
            }
        }

        await db.SaveChanges();
    }

    private void AddAlbum(LibraryDb db, AlbumInfo album)
    {
        db.Albums.Add(new DbAlbum
        {
            Id = album.Id,
            ArtistId = album.ArtistId,
            Title = album.Title,
            Year = album.Year,
            ReleaseType = album.ReleaseType,
            Artwork = album.ImageUrl,
            Source = album.Source
        });
    }

    private void AddArtist(LibraryDb db, ArtistInfo artist)
    {
        db.Artists.Add(new DbArtist
        {
            Id = artist.Id,
            Name = artist.Name,
            Grouping = artist.Grouping,
            Genre = artist.Genre,
            Country = artist.Country,
            State = artist.State,
            City = artist.City
        });
    }

    private void AddOrUpdateArtist(LibraryDb db, ArtistInfo artist)
    {
        var existing = db.Artists.SingleOrDefault(a => a.Id == artist.Id);

        if (existing == null)
        {
            AddArtist(db, artist);
            return;
        }

        existing.Grouping = existing.Grouping == Grouping.None ? artist.Grouping : existing.Grouping;
        existing.Genre = string.IsNullOrEmpty(existing.Genre) ? artist.Genre : existing.Genre;
        existing.Country = string.IsNullOrEmpty(existing.Country) ? artist.Country : existing.Country;
        existing.State = string.IsNullOrEmpty(existing.State) ? artist.State : existing.State;
        existing.City = string.IsNullOrEmpty(existing.City) ? artist.City : existing.City;
    }

    public async Task AddTracks(ICollection<TrackInfo> tracks)
    {
        using var db = await _dbFactory.Create<LibraryDb>();

        await db.SaveChanges();
    }

    public async Task Clear()
    {
        using var db = await _dbFactory.Create<LibraryDb>();
        db.Artists.ToList().ForEach(a => db.Artists.Remove(a));
        db.Albums.ToList().ForEach(a => db.Albums.Remove(a));
        db.Tracks.ToList().ForEach(a => db.Tracks.Remove(a));
        db.PlayTracks.ToList().ForEach(a => db.PlayTracks.Remove(a));
        await db.SaveChanges();
    }
}