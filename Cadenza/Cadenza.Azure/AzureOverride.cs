namespace Cadenza.Azure;

public class AzureOverride
{
    public string id { get; set; }
    public string item { get; set; }
    public string itemType { get; set; }
    public string propertyName { get; set; }
    public string originalValue { get; set; }
    public string overrideValue { get; set; }

    public string partitionKey
    {
        get { return id; }
        set { id = value; }
    }

    public string rowKey
    {
        get { return propertyName; }
        set { propertyName = value; }
    }
}
