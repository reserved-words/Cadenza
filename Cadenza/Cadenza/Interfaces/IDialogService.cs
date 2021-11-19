namespace Cadenza;

public interface IDialogService
{
    Task<(bool Saved, TModel Data)> DisplayForm<TForm, TModel>(TModel model, string title)
        where TForm : FormBase<TModel>
        where TModel : class;
}