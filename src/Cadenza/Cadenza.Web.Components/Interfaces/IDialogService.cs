using Cadenza.Web.Components.Forms;

namespace Cadenza.Web.Components.Interfaces;

public interface IDialogService
{
    Task<(bool Saved, TModel Data)> DisplayForm<TForm, TModel>(TModel model, string title, bool fullWidth = true)
        where TForm : FormBase<TModel>
        where TModel : class;

    Task Display<TView, TModel>(TModel model, string title, bool fullWidth = true)
        where TView : ViewBase<TModel>
        where TModel : class;
}