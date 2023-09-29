using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.Web.Common.Model;

public class ArtistReleaseGroup
{
    public ReleaseTypeGroup Group { get; set; }
    public List<Album> Albums { get; set; }
}
