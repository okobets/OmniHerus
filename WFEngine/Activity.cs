namespace WFEngine
{
    public class Activity<T, TResult> : IActivity<T, TResult>
    {
        public State State { get; set; }
        public required string Name { get; set; }
        public required Func<T, CancellationToken, TResult> Content { get; set; }

        private readonly CancellationTokenSource cancellationTokenSource = new();

        public TResult Execute(T input, TimeSpan timeout)
        {
            try
            {
                var t = ExecuteAsync(input);
                var r = t.Wait(timeout);
                State = State.Stopped;
                if (r) return t.Result;
                cancellationTokenSource.Cancel(false);
                t.Dispose();
            }
            catch (AggregateException ae)
            {
                Console.WriteLine(ae.InnerExceptions.Aggregate("", (s, exception) => s + exception + ";"));
            }

            return default;
        }

        public Task<TResult> ExecuteAsync(T input)
        {
            State = State.Running;
            var t = Task.Factory.StartNew(() => Content.Invoke(input, cancellationTokenSource.Token), cancellationTokenSource.Token);

            return t;
        }

        public void Stop()
        {
            if (State != State.Running) return;
            cancellationTokenSource.Cancel();
            State = State.Stopped;
        }
    }
}
