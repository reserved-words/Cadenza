namespace Cadenza.Common;

public class MetaDataUpdate
{
    public ItemType ItemType { get; set; }
    public string Id { get; set; }
    public string Item { get; set; }
    public ItemProperty PropertyName { get; set; }
    public string OriginalValue { get; set; }
    public string UpdatedValue { get; set; }

    public override bool Equals(object obj)
    {
        if (obj == null || !(obj is MetaDataUpdate update))
            return false;

        return update.ItemType == this.ItemType
            && update.Id == this.Id
            && update.PropertyName == this.PropertyName;
    }
}
