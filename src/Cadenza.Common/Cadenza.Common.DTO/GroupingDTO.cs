namespace Cadenza.Common.DTO;

public class GroupingDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsUsed { get; set; }

    public override string ToString()
    {
        return Name;
    }

    public override bool Equals(object obj)
    {
        return obj is GroupingDTO grp && grp.Id == Id && grp.Name == Name;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name);
    }
}

