using Cadenza.Web.Common.Interfaces.Store;

namespace Cadenza.Web.Components.Shared.Dialogs;

public class ProgressDialogBase : DialogBase, IDisposable
{
	[Inject]
	public ILongRunningTaskService Service { get; set; }

	[Inject]
	public IMessenger Messenger { get; set; }

	[Parameter]
	public Func<TaskGroup> TaskGroupFactory { get; set; }

	[Parameter]
	public bool AutoStart { get; set; }

	[Parameter]
	public string StartPromptText { get; set; }

	public bool Started => State.Started();
	public bool InProgress => State.InProgress();
	public bool Ended => State.Ended();

	public bool AtLeastOneTaskErrored => SubTasks.Any(t => t.Value.State == TaskState.Errored);

	public string ProgressMessage { get; set; }
	public TaskState State { get; set; }

	private Guid _taskGroupProgressSubscriptionId = Guid.Empty;
	private Guid _subTaskProgressSubscriptionId = Guid.Empty;

	public Dictionary<string, SubTaskProgress> SubTasks { get; set; } = new Dictionary<string, SubTaskProgress>();

	private CancellationTokenSource _cancellationTokenSource;
	private CancellationToken _cancellationToken;

	protected override void OnInitialized()
	{
		Messenger.Subscribe<TaskGroupProgressEventArgs>(OnTaskGroupProgressChanged, out _taskGroupProgressSubscriptionId);
		Messenger.Subscribe<SubTaskProgressEventArgs>(OnSubTaskProgressChanged, out _subTaskProgressSubscriptionId);
	}

	protected async override Task OnParametersSetAsync()
	{
		if (Started)
			return;

		if (AutoStart)
		{
			await OnStart();
		}
	}

	private Task OnTaskGroupProgressChanged(object sender, TaskGroupProgressEventArgs e)
	{
		ProgressMessage = e.Message;
		State = e.State;
		StateHasChanged();
		return Task.CompletedTask;
	}

	private Task OnSubTaskProgressChanged(object sender, SubTaskProgressEventArgs e)
	{
		var task = SubTasks[e.Id];
		task.Message = e.Message;
		task.State = e.State;
		StateHasChanged();
		return Task.CompletedTask;
	}

	protected async Task OnStart()
	{
		var taskGroup = TaskGroupFactory();

		SubTasks = taskGroup.Tasks
			.ToDictionary(t => t.Id, t => new SubTaskProgress
			{
				Title = t.Title,
				State = TaskState.None,
				Message = ""
			});

		_cancellationTokenSource = new CancellationTokenSource();
		_cancellationToken = _cancellationTokenSource.Token;
		await Service.RunTasks(taskGroup, _cancellationToken);
	}

	protected void OnCancel()
	{
		State = TaskState.Cancelling;
		ProgressMessage = "Cancelling";
		_cancellationTokenSource.Cancel();
	}

	protected void OnClose()
	{
		Messenger.Unsubscribe<TaskGroupProgressEventArgs>(_taskGroupProgressSubscriptionId);
		Messenger.Unsubscribe<SubTaskProgressEventArgs>(_subTaskProgressSubscriptionId);
		Submit();
	}

	public void Dispose()
	{
		_cancellationTokenSource?.Dispose();
		GC.SuppressFinalize(this);
	}
}
