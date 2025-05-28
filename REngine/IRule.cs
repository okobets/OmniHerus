namespace REngine
{
    public interface IRule<in T>
    {
        string? Outcome { get; set; }
        int Weight { get; set; }
        bool Evaluate(T obj);
    }
}
