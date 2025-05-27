using System.Collections.Concurrent;

namespace WFEngine
{
    public class WorkflowEngine<T, TResult> : IWorkflowEngine<T, TResult>
    {
        public List<TResult> Results { get; set; }
        public IWorkflow<T, TResult> Workflow { get; set; }
        
        private readonly CancellationTokenSource cancellationTokenSource = new();
        private readonly ConcurrentBag<Task> tasks = []; 

        public WorkflowEngine()
        { }

        public WorkflowEngine(IWorkflow<T, TResult> wrkflow)
        {
            Workflow = wrkflow;
        }

        public TResult ExecuteWorkflow(T input, TimeSpan timeout)
        {
            if (Workflow?.Activities == null || Workflow.Activities.Count == 0 && Workflow.State != State.Running) return default;
            return ExecuteWorkflowAsync(input, timeout).Result;
        }

        public Task<TResult> ExecuteWorkflowAsync(T input, TimeSpan timeout)
        {
            var task = Task.Factory.StartNew(() => ExecutionDelegate(input, timeout, cancellationTokenSource.Token), cancellationTokenSource.Token);
            tasks.Add(task);
            return task;
        }

        public void StopExecution()
        {
            if (Workflow == null || Workflow.State != State.Running) return;
            Workflow.CurrentActivity.Stop();
            cancellationTokenSource.Cancel();
            Workflow.State = State.Stopped;
        }

        private TResult ExecutionDelegate(T input, TimeSpan timeout, CancellationToken token)
        {
            dynamic lastResult = input;
            Results = [];
            Workflow.State = State.Running;

            foreach (var activity in Workflow.Activities.TakeWhile(activity => Workflow.State != State.Stopped))
            {
                try
                {
                    Workflow.CurrentActivity = activity;
                    lastResult = activity.Execute(lastResult, timeout);
                    Results.Add(lastResult);
                    if (token.IsCancellationRequested) break;
                }
                catch (BreakFlowException)
                {
                    break;
                }
                finally
                {
                    activity.State = State.Stopped;
                }
            }

            Workflow.State = State.Stopped;
            return lastResult;
        }
    }
}
