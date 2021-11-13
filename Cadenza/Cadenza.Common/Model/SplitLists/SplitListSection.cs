namespace Cadenza.Common;

public class ListSection<T>
{
    public string Name { get; set; }
    public List<T> Items { get; set; }

    public T SelectedItem { get; set; }
}
