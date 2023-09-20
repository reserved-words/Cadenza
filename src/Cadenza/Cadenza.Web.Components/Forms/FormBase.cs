using Fluxor.Blazor.Web.Components;

namespace Cadenza.Web.Components.Forms;

public class FormBase<T> : FluxorComponent
{
    [Parameter]
    public T Model { get; set; }

    [CascadingParameter] protected MudDialogInstance MudDialog { get; set; }

    protected void Submit() => MudDialog.Close(DialogResult.Ok(Model));
    protected void Cancel() => MudDialog.Cancel();
}
