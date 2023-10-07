namespace Cadenza.Common.DTO;

public class PropertyUpdateDTO
{
    public int Id { get; set; }
    public ItemProperty Property { get; set; }
    public string OriginalValue { get; set; }
    public string UpdatedValue { get; set; }
}