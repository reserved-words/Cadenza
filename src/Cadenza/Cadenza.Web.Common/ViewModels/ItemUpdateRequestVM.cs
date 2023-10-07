namespace Cadenza.Web.Common.ViewModels;

public class ItemUpdateRequestVM
{
    public LibraryItemType Type { get; set; }
    public int Id { get; set; }
    public List<PropertyUpdateVM> Updates { get; set; } = new();
}
