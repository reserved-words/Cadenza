namespace Cadenza;

public class DialogBase : ComponentBase
{
    [CascadingParameter] protected MudDialogInstance MudDialog { get; set; }

    protected void Submit() => MudDialog.Close(DialogResult.Ok(true));
}