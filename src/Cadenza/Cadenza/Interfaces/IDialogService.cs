namespace Cadenza;

public interface IDialogService
{
    Task<(bool Saved, TModel Data)> DisplayForm<TForm, TModel>(TModel model, string title)
        where TForm : FormBase<TModel>
        where TModel : class;

    Task Display<TView, TModel>(TModel model, string title)
        where TView : ViewBase<TModel>
        where TModel : class;
}