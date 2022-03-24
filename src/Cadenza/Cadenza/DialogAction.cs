namespace Cadenza;

public class DialogAction
{
    public DialogAction(string caption, Func<Task> onClick)
    {
        Caption = caption;
        OnClick = onClick;
    }

    public string Caption { get; set; }
    public Func<Task> OnClick { get; set; }
}
