using Cadenza.Web.Components.Forms;
using IDialogService = Cadenza.Web.Components.Interfaces.IDialogService;

namespace Cadenza.Web.Components.MudServices;

internal class MudDialogService : IDialogService
{
    private readonly MudBlazor.IDialogService _mudService;

    public MudDialogService(MudBlazor.IDialogService mudService)
    {
        _mudService = mudService;
    }

    public Task Display<TView, TModel>(TModel model, string title, bool fullWidth = true)
        where TView : DialogViewBase<TModel>
        where TModel : class
    {
        var parameters = new DialogParameters
        {
            { nameof(DialogViewBase<TModel>.Model), model }
        };
        _mudService.Show<TView>(title, parameters, new DialogOptions
        {
            DisableBackdropClick = true,
            MaxWidth = fullWidth ? MaxWidth.Medium : MaxWidth.Small,
            FullWidth = fullWidth
        });

        return Task.CompletedTask;
    }

    public async Task<(bool Saved, TModel Data)> DisplayForm<TForm, TModel>(TModel model, string title, bool fullWidth = true)
        where TForm : FormBase<TModel>
        where TModel : class
    {
        var parameters = new DialogParameters
        {
            { nameof(FormBase<TModel>.Model), model }
        };
        var formReference = _mudService.Show<TForm>(title, parameters, new DialogOptions
        {
            DisableBackdropClick = true,
            MaxWidth = fullWidth ? MaxWidth.Medium : MaxWidth.Small,
            FullWidth = fullWidth
        });
        var result = await formReference.Result;
        return (!result.Cancelled, result.Data as TModel);
    }
}