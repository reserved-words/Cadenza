namespace Cadenza.Web.Common.Model;

public class LibraryBreadcrumbItem
{
    public LibraryBreadcrumbItem(PlayerItemType type, string id, string name)
    {
        Type = type;
        Id = id;
        Name = name;
    }

    public LibraryBreadcrumbItem(PlayerItemType type, int id, string name)
        :this(type, id.ToString(), name)
    {
    }

    public PlayerItemType Type { get; set; }
    public string Id { get; set; }
    public string Name { get; set; }
}