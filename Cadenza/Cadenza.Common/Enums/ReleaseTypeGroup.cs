using System.ComponentModel.DataAnnotations;

namespace Cadenza.Common;

[DefaultValue(Albums)]
public enum ReleaseTypeGroup
{
    [Display(Name = "Studio Albums")]
    Albums = 1,
    [Display(Name = "Compilations")]
    Compilations = 2,
    [Display(Name = "EPs & Singles")]
    Singles = 3,
    [Display(Name = "Other Releases")]
    Other = 4,
    [Display(Name = "By Other Artists")]
    ByOtherArtists = 5,
    [Display(Name = "Playlists")]
    Playlists = 6
}
