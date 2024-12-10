using System;

namespace Synergy.SDK.Jobs
{
    public class Job
    {
        public Func<bool> isFinished { get; }
        public Action OnStart { get; }
        public bool HasStarted { get; set; } = false;

        public Job (Func<bool> isFinished, Action onStart)
        {
            this.isFinished = isFinished;
            OnStart = onStart;
        }
    }
}
