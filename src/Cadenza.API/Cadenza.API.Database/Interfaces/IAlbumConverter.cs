using Cadenza.API.Database.Model;
using Cadenza.Domain;

namespace Cadenza.API.Database.Interfaces;

internal interface IAlbumConverter
{
    JsonAlbum ToJsonModel(AlbumInfo artist);
    AlbumInfo ToAppModel(JsonAlbum artist, ICollection<JsonArtist> artists);
}