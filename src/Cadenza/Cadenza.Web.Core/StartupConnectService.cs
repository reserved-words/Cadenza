namespace Cadenza.Web.Core
{
    public class StartupConnectService : IStartupConnectService
    {
        private readonly IStoreSetter _storeSetter;
        private readonly IEnumerable<IConnectionTaskBuilder> _taskBuilders;

        public StartupConnectService(IStoreSetter storeSetter, IEnumerable<IConnectionTaskBuilder> taskBuilders)
        {
            _storeSetter = storeSetter;
            _taskBuilders = taskBuilders;
        }

        public TaskGroup GetStartupTasks()
        {
            var taskGroup = new TaskGroup
            {
                PreTask = ClearSessionData
            };

            foreach (var builder in _taskBuilders)
            {
                taskGroup.Tasks.Add(builder.GetConnectionTask());
            }

            return taskGroup;
        }

        private async Task ClearSessionData()
        {
            await _storeSetter.Clear(StoreKey.CurrentTrackSource);
            await _storeSetter.Clear(StoreKey.CurrentTrack);
        }
    }
}
