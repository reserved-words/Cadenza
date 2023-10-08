namespace Cadenza.Common.DTO;

public class TagsDTO
{
    private const string Separator = "|";


    public TagsDTO() { }

    public TagsDTO(string tags) 
    {
        Tags = tags?.Split(Separator).ToList() ?? new List<string>();
    }

    public List<string> Tags { get; set; } = new List<string>();

    public override string ToString()
    {
        return string.Join(Separator, Tags.OrderBy(t => t));
    }
}
