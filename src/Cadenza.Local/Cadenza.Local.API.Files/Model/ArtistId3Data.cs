using Cadenza.Common.Domain.Model;

namespace Cadenza.Local.API.Files.Model;

internal class ArtistId3Data
{
    public string Name { get; set; }
    public string Genre { get; set; }
    public string Grouping { get; set; }
    public ArtworkImage Image { get; set; }
}