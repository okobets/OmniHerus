namespace REngine
{
    public class DecisionRuleEngine<T> : IRuleEngine<T>
    {
        public DecisionLogic DecisionLogic { get; set; }
        public int PassingScorePercentage { get; set; }

        public DecisionRuleEngine(DecisionLogic decisionLogic = DecisionLogic.Or)
        {
            DecisionLogic = decisionLogic;
        }

        public bool GetDecision(List<IRule<T>> rules, T evaluee)
        {
            return DecisionLogic == DecisionLogic.Or ? rules.Any(a => a.Evaluate(evaluee)) : rules.All(a => a.Evaluate(evaluee));
        }

        public string? GetDecision(RuleSet<T> ruleSet, T evaluee)
        {
            return ruleSet.Any(a => a.Evaluate(evaluee)) ? ruleSet.First(a => a.Evaluate(evaluee)).Outcome : null;
        }
    }
}
