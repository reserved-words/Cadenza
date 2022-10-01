namespace Cadenza.Web.Components.Dialogs;

public class ViewBase<T> : ComponentBase
{
    [Parameter]
    public T Model { get; set; }

    [CascadingParameter] protected MudDialogInstance MudDialog { get; set; }

    protected void Cancel() => MudDialog.Cancel();
}
