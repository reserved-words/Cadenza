namespace Cadenza.Common.Domain.Model;

public class TagList
{
    private const string Separator = "|";

    private readonly List<string> _tags = new List<string>();

    public IReadOnlyList<string> Tags => _tags;

    public TagList()
    {
    }

    public TagList(string tags)
    {
        if (tags != null) 
        {
            _tags = tags?.Split(Separator).ToList();
        }
    }

    public TagList(ICollection<string> tags)
    {
        if (tags != null)
        {
            _tags = new List<string>(tags);
        }
    }

    public void Add(string tag)
    {
        if (!Tags.Contains(tag))
        {
            _tags.Add(tag);
            _tags.Sort();
        }
    }

    public void Remove(string tag)
    {
        _tags.Remove(tag);
    }

    public override string ToString()
    {
        return string.Join(Separator, Tags);
    }

    public override bool Equals(object obj)
    {
        if (obj is not TagList list)
            return false;

        return Tags.ToString() == list.ToString(); 
    }

    public override int GetHashCode()
    {
        return Tags.GetHashCode();
    }
}