namespace Cadenza.Web.Common.Model;

public struct EditItem
{
    public EditItem(LibraryItemType type, int id, string name)
    {
        Type = type;
        Id = id;
        Name = name;
    }

    public LibraryItemType Type { get; }
    public int Id { get; }
    public string Name { get; }
}
