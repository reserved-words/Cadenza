namespace Cadenza.Common;

public class SplitList<T>
{
    public List<ListSection<T>> Sections { get; set; }

    public bool MultipleSections => Sections.Count > 1;

    public List<T> Single => Sections.Single().Items;

    public List<T> All => Sections.SelectMany(s => s.Items).ToList();

    public T First => Sections.First().Items.First();

    public T SelectedItem { get; set; }
}
