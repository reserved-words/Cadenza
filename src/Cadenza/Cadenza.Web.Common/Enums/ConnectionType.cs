namespace Cadenza.Web.Common.Enums;

public enum ConnectionType
{
    [Display(Name = "Database")]
    Api,
    [Display(Name = "Last.FM")]
    LastFm,
    [Display(Name = "Local Source")]
    Local
}
