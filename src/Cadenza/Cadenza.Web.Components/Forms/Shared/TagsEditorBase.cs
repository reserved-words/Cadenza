using System.Text.RegularExpressions;

namespace Cadenza.Web.Components.Forms.Shared;

public class TagsEditorBase : ComponentBase
{
    private const string RegexPattern = @"^[0-9a-zß-ÿ\-]*$";

    [Parameter] public TagListVM Model { get; set; } = new TagListVM();

    protected DefaultFocus DefaultFocus { get; set; }

    protected string ErrorMessage { get; set; }

    public void OnAddTag(string tag)
    {
        if (Regex.IsMatch(tag, RegexPattern))
        {
            ErrorMessage = null;
            Model.Add(tag);
        }
        else
        {
            ErrorMessage = "Only aphanumerics and hyphens allowed";
        }

        DefaultFocus = DefaultFocus.FirstChild;
    }

    public void OnRemoveTag(MudChip chip)
    {
        Model.Remove(chip.Text);
    }

    public bool IsNewTagTextEmpty => string.IsNullOrWhiteSpace(NewTagText);

    public string NewTagText { get; set; }
}