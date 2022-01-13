namespace Cadenza.Player;

public class OverrideViewModel
{
    public OverrideViewModel(LibrarySource source, MetaDataUpdate model)
    {
        Source = source;
        Id = model.Id;
        Item = model.Item;
        ItemType = model.ItemType;
        PropertyName = model.Property;
        OriginalValue = model.OriginalValue;
        OverrideValue = model.UpdatedValue;
    }

    public LibrarySource Source { get; set; }
    public ItemType ItemType { get; set; }
    public string Id { get; set; }
    public string Item { get; set; }
    public ItemProperty PropertyName { get; set; }
    public string OriginalValue { get; set; }
    public string OverrideValue { get; set; }
}