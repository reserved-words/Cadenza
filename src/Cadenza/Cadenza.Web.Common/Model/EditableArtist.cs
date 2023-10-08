using Cadenza.Web.Model;

namespace Cadenza.Web.Common.Model;

public class EditableArtist
{
    public int Id { get; init; }
    public string Name { get; init; }

    public GroupingVM Grouping { get; set; }
    public string Genre { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string ImageBase64 { get; set; }

    public ICollection<string> Tags { get; set; }
}
