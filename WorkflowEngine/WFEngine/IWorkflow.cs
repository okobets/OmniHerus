namespace WFEngine
{
    public interface IWorkflow<T, TResult>
    {
        State State { get; set; }
        IActivity<T, TResult> CurrentActivity { get; set; }
        List<IActivity<T, TResult>> Activities { get; set; }
    }
}
