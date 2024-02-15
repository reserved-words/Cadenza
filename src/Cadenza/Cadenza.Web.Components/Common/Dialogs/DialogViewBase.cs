namespace Cadenza.Web.Components.Common.Dialogs;

public class DialogViewBase<T> : ComponentBase
{
    [Parameter] public T Model { get; set; }

    [CascadingParameter] protected MudDialogInstance MudDialog { get; set; }

    protected void Cancel() => MudDialog.Cancel();
}
