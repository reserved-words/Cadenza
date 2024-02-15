using Cadenza.Components.Dialogs;
using Cadenza.Interfaces;
using IDialogService = Cadenza.Interfaces.IDialogService;

namespace Cadenza.Services;

internal class MudDialogService : IDialogService
{
    private readonly MudBlazor.IDialogService _mudService;

    public MudDialogService(MudBlazor.IDialogService mudService)
    {
        _mudService = mudService;
    }

    public void Display<TView, TModel>(TModel model, string title, DialogWidth width)
        where TView : DialogViewBase<TModel>
        where TModel : class
    {
        var parameters = new DialogParameters
        {
            { nameof(DialogViewBase<TModel>.Model), model }
        };

        var mbWidth = GetMudBlazorWidth(width);

        _mudService.Show<TView>(title, parameters, new DialogOptions
        {
            DisableBackdropClick = true,
            MaxWidth = mbWidth.MaxWidth,
            FullWidth = mbWidth.FullWidth
        });
    }

    private (MaxWidth MaxWidth, bool FullWidth) GetMudBlazorWidth(DialogWidth width)
    {
        var maxWidth = width switch
        {
            DialogWidth.Small => MaxWidth.Small,
            DialogWidth.Medium => MaxWidth.Medium,
            DialogWidth.Large => MaxWidth.Large,
            _ => MaxWidth.Medium
        };

        var fullWidth = width >= DialogWidth.Medium;

        return (maxWidth, fullWidth);
    }
}