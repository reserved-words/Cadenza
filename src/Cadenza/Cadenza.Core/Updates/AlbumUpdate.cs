using Cadenza.Domain;
using Cadenza.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Cadenza.Core.Updates;

public class AlbumUpdate : ItemUpdate<AlbumInfo>
{
    public AlbumUpdate()
        : base() { }

    public AlbumUpdate(AlbumInfo album)
        : base(LibraryItemType.Album, album.Id, album) { }

    [Required]
    public string Title
    {
        get { return GetUpdated(ItemProperty.AlbumTitle); }
        set { Item.Title = value; SetUpdated(ItemProperty.AlbumTitle, value); }
    }

    [Required]
    public ReleaseType ReleaseType
    {
        get { return GetUpdated(ItemProperty.ReleaseType).Parse<ReleaseType>(); }
        set { Item.ReleaseType = value; SetUpdated(ItemProperty.ReleaseType, value.ToString()); }
    }

    [Required]
    [StringLength(maximumLength: 4, MinimumLength = 4)]
    public string Year
    {
        get { return GetUpdated(ItemProperty.ReleaseYear); }
        set { Item.Year = value; SetUpdated(ItemProperty.ReleaseYear, value); }
    }
}