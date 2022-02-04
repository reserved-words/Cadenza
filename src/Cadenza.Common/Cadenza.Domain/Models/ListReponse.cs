namespace Cadenza.Domain;

public class ListReponse<T>
{
    public List<T> Items { get; set; }
    public int Page { get; set; }
    public int Limit { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
}
