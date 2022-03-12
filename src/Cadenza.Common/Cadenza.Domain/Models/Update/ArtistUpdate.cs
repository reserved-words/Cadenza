using System.ComponentModel.DataAnnotations;

namespace Cadenza.Domain;

public class ArtistUpdate : ItemUpdate<ArtistInfo>
{
    public ArtistUpdate()
        : base() { }

    public ArtistUpdate(ArtistInfo artist)
        : base(LibraryItemType.Artist, artist.Id, artist) { }


    [Required]
    public string Name
    {
        get
        { return GetUpdated(ItemProperty.Artist); }
        set
        {
            Item.Name = value;
            SetUpdated(ItemProperty.Artist, value);
        }
    }

    [Required]
    public Grouping Grouping
    {
        get { return GetUpdated(ItemProperty.Grouping).Parse<Grouping>(); }
        set
        {
            Item.Grouping = value;
            SetUpdated(ItemProperty.Grouping, value.ToString());
        }
    }

    [Required]
    public string Genre
    {
        get { return GetUpdated(ItemProperty.Genre); }
        set
        {
            Item.Genre = value;
            SetUpdated(ItemProperty.Genre, value);
        }
    }

    public string City
    {
        get { return GetUpdated(ItemProperty.City); }
        set
        {
            Item.City = value;
            SetUpdated(ItemProperty.City, value);
        }
    }

    public string State
    {
        get { return GetUpdated(ItemProperty.State); }
        set
        {
            Item.State = value;
            SetUpdated(ItemProperty.State, value);
        }
    }

    public string Country
    {
        get { return GetUpdated(ItemProperty.Country); }
        set
        {
            Item.Country = value;
            SetUpdated(ItemProperty.Country, value);
        }
    }
}