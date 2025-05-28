namespace WFEngine
{
    public interface IActivity<T, TResult>
    {
        State State { get; set; }
        string Name { get; set; }
        Func<T, CancellationToken, TResult> Content { get; set; }
        TResult Execute(T input, TimeSpan timeout);
        Task<TResult> ExecuteAsync(T input);
        void Stop();
    }
}
