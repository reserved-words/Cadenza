namespace Cadenza.Web.Common.ViewModels;

public class PropertyUpdateVM
{
    public int Id { get; set; }
    public ItemProperty Property { get; set; }
    public string OriginalValue { get; set; }
    public string UpdatedValue { get; set; }
}