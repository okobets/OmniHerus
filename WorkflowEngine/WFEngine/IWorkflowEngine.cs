namespace WFEngine
{
    public interface IWorkflowEngine<T, TResult>
    {
        List<TResult> Results { get; set; }
        IWorkflow<T, TResult> Workflow { get; set; }
        TResult ExecuteWorkflow(T input, TimeSpan timeout);
        Task<TResult> ExecuteWorkflowAsync(T input, TimeSpan timeout);
        void StopExecution();
    }
}
