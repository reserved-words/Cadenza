namespace Cadenza.UI.Shared.Dialogs;

public class FormBase<T> : ComponentBase
{
    [Parameter]
    public T Model { get; set; }

    [CascadingParameter] protected MudDialogInstance MudDialog { get; set; }

    protected void Submit() => MudDialog.Close(DialogResult.Ok(Model));
    protected void Cancel() => MudDialog.Cancel();
}
