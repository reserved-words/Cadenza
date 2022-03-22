namespace Cadenza;

public class MudDialogService : IDialogService
{
    private MudBlazor.IDialogService _mudService;

    public MudDialogService(MudBlazor.IDialogService mudService)
    {
        _mudService = mudService;
    }

    public async Task Display<TView, TModel>(TModel model, string title, bool narrow = false)
        where TView : ViewBase<TModel>
        where TModel : class
    {
        var parameters = new DialogParameters();
        parameters.Add(nameof(ViewBase<TModel>.Model), model);
        _mudService.Show<TView>(title, parameters, new DialogOptions
        {
            DisableBackdropClick = true,
            MaxWidth = narrow ? MaxWidth.Small : MaxWidth.Medium,
            FullWidth = !narrow
        });
    }

    public async Task<(bool Saved, TModel Data)> DisplayForm<TForm, TModel>(TModel model, string title, bool narrow = false)
        where TForm : FormBase<TModel>
        where TModel : class
    {
        var parameters = new DialogParameters();
        parameters.Add(nameof(FormBase<TModel>.Model), model);
        var formReference = _mudService.Show<TForm>(title, parameters, new DialogOptions
        {
            DisableBackdropClick = true,
            MaxWidth = narrow ? MaxWidth.Small : MaxWidth.Medium,
            FullWidth = !narrow
        });
        var result = await formReference.Result;
        return (!result.Cancelled, result.Data as TModel);
    }
}