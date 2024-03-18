using Cadenza.Components.Dialogs;

namespace Cadenza.Interfaces;

public interface IDialogService
{
    void Display<TView, TModel>(TModel model, string title, DialogWidth width)
        where TView : DialogViewBase<TModel>
        where TModel : class;
}

public enum DialogWidth
{
    Small = 1,
    Medium = 2,
    Large = 3
}