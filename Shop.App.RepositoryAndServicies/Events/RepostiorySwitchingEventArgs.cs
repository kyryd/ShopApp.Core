using RepositoryAndServicies.Repositories;

namespace RepositoryAndServicies.Events
{
    public class RepostiorySwitchingEventArgs(RepositoryType repositoryType) : EventArgs
    {
        public RepositoryType RepostioryType { get; set; } = repositoryType;
    }

    public class RepositorySwitchingEventHandler
    {
        public event EventHandler<RepostiorySwitchingEventArgs>? SwitchingEvent;

        public void RaiseSwitchingEvent(RepositoryType repositoryType)
        {
            SwitchingEvent?.Invoke(this, new RepostiorySwitchingEventArgs(repositoryType));
        }
    }
}