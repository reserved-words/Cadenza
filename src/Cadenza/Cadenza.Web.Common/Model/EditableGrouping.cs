namespace Cadenza.Web.Common.Model;

public class EditableGrouping
{
    public int Id { get; init; }

    [Required]
    public string Name { get; set; }
    public bool IsUsed { get; init; }
}