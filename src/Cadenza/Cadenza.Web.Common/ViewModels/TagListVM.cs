namespace Cadenza.Web.Common.ViewModels;

public class TagListVM
{
    private const string Separator = "|";

    private readonly List<string> _tags = new List<string>();

    public IReadOnlyList<string> Tags => _tags;

    public TagListVM()
    {
    }

    public TagListVM(string tags)
    {
        if (tags != null)
        {
            _tags = tags?.Split(Separator).ToList();
        }
    }

    public TagListVM(ICollection<string> tags)
    {
        if (tags != null)
        {
            _tags = new List<string>(tags);
        }
    }

    public TagListVM(TagListVM tagList)
    {
        if (tagList != null)
        {
            _tags = new List<string>(tagList.Tags);
        }
    }

    public void Add(string tag)
    {
        if (string.IsNullOrWhiteSpace(tag))
            return;

        tag = tag.Replace(Separator, "");

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
        if (obj is not TagListVM list)
            return false;

        return Tags.ToString() == list.ToString();
    }

    public override int GetHashCode()
    {
        return Tags.GetHashCode();
    }
}