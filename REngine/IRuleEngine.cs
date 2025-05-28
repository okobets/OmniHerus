namespace REngine
{
    public interface IRuleEngine<T>
    {
        DecisionLogic DecisionLogic { get; set; }
        int PassingScorePercentage { get; set; }
        bool GetDecision(List<IRule<T>> rules, T evaluee);
        string? GetDecision(RuleSet<T> ruleSet, T evaluee);
    }

    public enum DecisionLogic
    {
        And,
        Or
    }
}
