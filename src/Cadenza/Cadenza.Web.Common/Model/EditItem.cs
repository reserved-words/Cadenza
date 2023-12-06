namespace Cadenza.Web.Common.Model;

public struct EditItem
{
    public EditItem(PlayerItemType type, int id, string name)
    {
        Type = type;
        Id = id;
        Name = name;
    }

    public PlayerItemType Type { get; }
    public int Id { get; }
    public string Name { get; }
}
