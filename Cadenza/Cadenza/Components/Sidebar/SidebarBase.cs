namespace Cadenza;

public class SidebarBase : ComponentBase
{
    [Parameter]
    public Func<Task> OnPause { get; set; }

    [Parameter]
    public Func<Task> OnResume { get; set; }

    [Parameter]
    public Func<Task> OnSkipNext { get; set; }

    [Parameter]
    public Func<Task> OnSkipPrevious { get; set; }

    [Parameter]
    public Func<PlaylistDefinition, Task> OnPlay { get; set; }
}