using Cadenza.Components.Shared.Dialogs;

namespace Cadenza.Interfaces;

public interface IDialogService
{
    Task<(bool Saved, TModel Data)> DisplayForm<TForm, TModel>(TModel model, string title, bool narrow = false)
        where TForm : FormBase<TModel>
        where TModel : class;

    Task Display<TView, TModel>(TModel model, string title, bool narrow = false)
        where TView : ViewBase<TModel>
        where TModel : class;
}