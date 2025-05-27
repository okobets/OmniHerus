namespace REngine
{
    public class Rule<T>(Predicate<T> condition, string? outcome = null, int weight = 0) : IRule<T>
    {
        public string? Outcome { get; set; } = outcome;
        public int Weight { get; set; } = weight;
        public Predicate<T> Condition { get; set; } = condition;

        public bool Evaluate(T obj)
        {
            return Condition.Invoke(obj);
        }
    }
}
