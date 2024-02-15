namespace Cadenza.Components.Dialogs;

public class DialogBase : FluxorComponent
{
    [CascadingParameter] protected MudDialogInstance MudDialog { get; set; }

    protected void Submit() => MudDialog.Close(DialogResult.Ok(true));
}