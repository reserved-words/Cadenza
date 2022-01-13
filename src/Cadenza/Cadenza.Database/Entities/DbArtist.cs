namespace Cadenza.Database;

public class DbArtist
{
    [Key]
    public string Id { get; set; }
    public string Name { get; set; }
    public Grouping Grouping { get; set; }
    public string Genre { get; set; }
    public string Country { get; set; }
    public string State { get; set; }
    public string City { get; set; }
}
