using Cadenza.Common.Domain.Enums;
using Cadenza.Common.Domain.Model.Album;

namespace Cadenza.Web.Common.Model;

public class ArtistReleaseGroup
{
    public ReleaseTypeGroup Group { get; set; }
    public List<Album> Albums { get; set; }
}
