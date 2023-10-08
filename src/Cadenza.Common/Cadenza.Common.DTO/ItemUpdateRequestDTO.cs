using Cadenza.Common.Enums;

namespace Cadenza.Common.DTO;

public class ItemUpdateRequestDTO
{
    public LibraryItemType Type { get; set; }
    public int Id { get; set; }
    public List<PropertyUpdateDTO> Updates { get; set; } = new();
}
