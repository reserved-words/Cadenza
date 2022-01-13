using System.ComponentModel.DataAnnotations;

namespace Cadenza.Common;

public class TrackUpdate : ItemUpdate<TrackInfo>
{
    public TrackUpdate()
        : base() { }

    public TrackUpdate(TrackInfo track)
        : base(ItemType.Track, track.Id, track) { }


    [Required]
    public string Title
    {
        get
        { return GetUpdated(ItemProperty.TrackTitle); }
        set
        {
            Item.Title = value;
            SetUpdated(ItemProperty.TrackTitle, value);
        }
    }

    [Required]
    public string Lyrics
    {
        get { return GetUpdated(ItemProperty.Lyrics); }
        set
        {
            Item.Lyrics = value;
            SetUpdated(ItemProperty.Lyrics, value.ToString());
        }
    }
}