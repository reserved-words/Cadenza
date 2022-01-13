using System.ComponentModel.DataAnnotations;

namespace Cadenza.Common;

[DefaultValue(Album)]
public enum ReleaseType
{
    [Display(Name = "Album")]
    [ReleaseTypeGroup(ReleaseTypeGroup.Albums)]
    Album = 1,
    [Display(Name = "Best Of")]
    [ReleaseTypeGroup(ReleaseTypeGroup.Compilations)]
    BestOf = 2,
    [Display(Name = "Live")]
    [ReleaseTypeGroup(ReleaseTypeGroup.Compilations)]
    Live = 3,
    [Display(Name = "Compilation")]
    [ReleaseTypeGroup(ReleaseTypeGroup.Compilations)]
    Compilation = 4,
    [Display(Name = "EP")]
    [ReleaseTypeGroup(ReleaseTypeGroup.Singles)]
    EP = 5,
    [Display(Name = "Single")]
    [ReleaseTypeGroup(ReleaseTypeGroup.Singles)]
    Single = 6,
    [Display(Name = "Other")]
    [ReleaseTypeGroup(ReleaseTypeGroup.Other)]
    Other = 7,
    [Display(Name = "Various Artists")]
    [ReleaseTypeGroup(ReleaseTypeGroup.ByOtherArtists)]
    VariousArtists = 8,
    [Display(Name = "Playlist")]
    [ReleaseTypeGroup(ReleaseTypeGroup.Playlists)]
    Playlist = 9
}