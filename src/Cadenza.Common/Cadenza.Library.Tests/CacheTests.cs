using Cadenza.Domain;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cadenza.Library.Tests
{
    public class CacheTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetAllTracks_GivenEmptyLibrary_ReturnsEmptyList()
        {
            // Arrange
            var sut = new Cache(new StaticLibrary());

            // Act
            var result = await sut.GetAllTracks();

            // Assert
            result.Should().BeEmpty();
        }

        [Test]
        public async Task GetAllTracks_GivenOneTrackLibrary_ReturnsTrack()
        {
            // Arrange
            var sut = new Cache(GetOneTrackLibrary());

            // Act
            var result = await sut.GetAllTracks();

            // Assert
            result.Should().HaveCount(1);

            var track = result.Single();
            track.Should().NotBeNull();
            track.Source.Should().Be(LibrarySource.Local);
            track.Id.Should().Be("Track4");
            track.Title.Should().Be("Track 4");
            track.ArtistId.Should().Be("TrackArtist1");
            track.ArtistName.Should().Be("Track Artist 1");
            track.AlbumId.Should().Be("Album3");
            track.AlbumTitle.Should().Be("Album 3");
            track.AlbumArtist.Should().Be("Album Artist 2");
        }

        [Test]
        public async Task GetAllTracks_GivenFullLibrary_ReturnsTracks()
        {
            // Arrange
            var sut = new Cache(GetFullLibrary());

            // Act
            var result = await sut.GetAllTracks();

            // Assert
            result.Should().HaveCount(2);

            for (var i = 1; i <=2; i++)
            {
                var track = result.ToList()[i-1];
                track.Should().NotBeNull();
                track.Source.Should().Be(LibrarySource.Local);
                track.Id.Should().Be($"Track{i}");
                track.Title.Should().Be($"Track {i}");
                track.ArtistId.Should().Be($"Artist{i}");
                track.ArtistName.Should().Be($"Artist {i}");
                track.AlbumId.Should().Be($"Album{i}");
                track.AlbumTitle.Should().Be($"Album {i}");
                track.AlbumArtist.Should().Be($"Artist {i}");
            }
        }

        private StaticLibrary GetOneTrackLibrary()
        {
            return new StaticLibrary
            {
                Artists = new List<ArtistInfo>
                {
                    new ArtistInfo 
                    { 
                        Id = "TrackArtist1", 
                        Name = "Track Artist 1" 
                    },
                    new ArtistInfo 
                    { 
                        Id = "AlbumArtist2", 
                        Name = "Album Artist 2" 
                    }
                },
                Albums = new List<AlbumInfo>
                {
                    new AlbumInfo 
                    { 
                        Id = "Album3", 
                        Title = "Album 3", 
                        ArtistId = "AlbumArtist2",
                        ArtistName = "Album Artist 2"
                    }
                },
                Tracks = new List<TrackInfo>
                {
                    new TrackInfo 
                    { 
                        Id = "Track4", 
                        Title = "Track 4", 
                        ArtistId = "TrackArtist1", 
                        ArtistName = "Track Artist 1",
                        AlbumId = "Album3", 
                        Source = LibrarySource.Local }
                },
                AlbumTrackLinks = new List<AlbumTrackLink>
                {
                    new AlbumTrackLink 
                    { 
                        TrackId = "Track4", 
                        AlbumId = "Album3" 
                    }
                }
            };
        }

        private StaticLibrary GetFullLibrary()
        {
            return new StaticLibrary
            {
                Artists = new List<ArtistInfo>
                {
                    new ArtistInfo
                    {
                        Id = "Artist1",
                        Name = "Artist 1"
                    },
                    new ArtistInfo
                    {
                        Id = "Artist2",
                        Name = "Artist 2"
                    }
                },
                Albums = new List<AlbumInfo>
                {
                    new AlbumInfo
                    {
                        Id = "Album1",
                        Title = "Album 1",
                        ArtistId = "Artist1",
                        ArtistName = "Artist 1"
                    },
                    new AlbumInfo
                    {
                        Id = "Album2",
                        Title = "Album 2",
                        ArtistId = "Artist2",
                        ArtistName = "Artist 2"
                    }
                },
                Tracks = new List<TrackInfo>
                {
                    new TrackInfo
                    {
                        Id = "Track1",
                        Title = "Track 1",
                        ArtistId = "Artist1",
                        ArtistName = "Artist 1",
                        AlbumId = "Album1",
                        Source = LibrarySource.Local 
                    },
                    new TrackInfo
                    {
                        Id = "Track2",
                        Title = "Track 2",
                        ArtistId = "Artist2",
                        ArtistName = "Artist 2",
                        AlbumId = "Album2",
                        Source = LibrarySource.Local
                    }
                },
                AlbumTrackLinks = new List<AlbumTrackLink>
                {
                    new AlbumTrackLink
                    {
                        TrackId = "Track1",
                        AlbumId = "Album1"
                    },
                    new AlbumTrackLink
                    {
                        TrackId = "Track2",
                        AlbumId = "Album2"
                    }
                }
            };
        }
    }
}