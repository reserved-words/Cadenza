namespace Cadenza.Common.Domain.Model.Library;

public class Grouping
{
    public Grouping(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; set; }
    public string Name { get; set; }

    public override string ToString()
    {
        return Name;
    }

    public override bool Equals(object obj)
    {
        return obj is Grouping grp && grp.Id == Id && grp.Name == Name;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name);
    }
}

