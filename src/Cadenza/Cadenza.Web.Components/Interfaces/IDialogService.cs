using Cadenza.Web.Components.Forms;

namespace Cadenza.Web.Components.Interfaces;

public interface IDialogService
{
    Task<(bool Saved, TModel Data)> DisplayForm<TForm, TModel>(TModel model, string title, DialogWidth width)
        where TForm : FormBase<TModel>
        where TModel : class;

    Task Display<TView, TModel>(TModel model, string title, DialogWidth width)
        where TView : DialogViewBase<TModel>
        where TModel : class;
}



public enum DialogWidth
{
    Small = 1,
    Medium = 2,
    Large = 3
}