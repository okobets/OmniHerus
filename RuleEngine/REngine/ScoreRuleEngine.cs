namespace REngine
{
    public class ScoreRuleEngine<T> : IRuleEngine<T>
    {
        public DecisionLogic DecisionLogic { get; set; }
        public int PassingScorePercentage { get; set; }

        public ScoreRuleEngine(int passingScorePercentage)
        {
            PassingScorePercentage = passingScorePercentage;
        }
        
        public bool GetDecision(List<IRule<T>> rules, T evaluee)
        {
            var scores = rules.Select(rule => new KeyValuePair<bool,int>(rule.Evaluate(evaluee), rule.Weight)).ToList();

            if (scores.All(a => a.Value != 0)) return scores.Where(w => w.Key).Sum(s => s.Value) >= PassingScorePercentage;
            if (PassingScorePercentage == 100) PassingScorePercentage = 99;
            return 100 / rules.Count * scores.Count(w => w.Key) >= PassingScorePercentage;
        }

        public string GetDecision(RuleSet<T> ruleSet, T evaluee)
        {
            throw new NotImplementedException();
        }
    }
}
