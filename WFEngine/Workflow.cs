namespace WFEngine
{
    public class Workflow<T, TResult> : IWorkflow<T, TResult>
    {
        public Workflow()
        {
            State = State.Stopped;
        }

        public State State { get; set; }
        public IActivity<T, TResult> CurrentActivity { get; set; }
        public List<IActivity<T, TResult>> Activities { get; set; }
    }
}
