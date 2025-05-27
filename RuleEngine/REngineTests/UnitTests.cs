using System.Diagnostics.CodeAnalysis;
using REngine;

namespace REngineTests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void DecisionStringOrPositive()
        {
            var rules = new List<IRule<string>>
            {
                new Rule<string>(s => s.Contains("mama")),
                new Rule<string>(s => s.Contains("mia")),
                new Rule<string>(s => s.Contains("papa")),
                new Rule<string>(s => s.Contains("bia"))
            };

            var bre = new DecisionRuleEngine<string>();
            var decision = bre.GetDecision(rules, "bia was here");
            Assert.IsTrue(decision);
        }

        [TestMethod]
        public void DecisionStringOrNegative()
        {
            var rules = new List<IRule<string>>
            {
                new Rule<string>(s => s.Contains("mama")),
                new Rule<string>(s => s.Contains("mia")),
                new Rule<string>(s => s.Contains("papa")),
                new Rule<string>(s => s.Contains("bia"))
            };

            var bre = new DecisionRuleEngine<string>();
            var decision = bre.GetDecision(rules, "moshik was here");
            Assert.IsFalse(decision);
        }

        [TestMethod]
        public void DecisionStringAndPositive()
        {
            var rules = new List<IRule<string>>
            {
                new Rule<string>(s => s.Contains("here")),
                new Rule<string>(s => s.Contains("was")),
                new Rule<string>(s => s.Contains("bia"))
            };

            var bre = new DecisionRuleEngine<string>(DecisionLogic.And);
            var decision = bre.GetDecision(rules, "bia was here");
            Assert.IsTrue(decision);
        }

        [TestMethod]
        public void DecisionStringAndNegative()
        {
            var rules = new List<IRule<string>>
            {
                new Rule<string>(s => s.Contains("mama")),
                new Rule<string>(s => s.Contains("mia")),
                new Rule<string>(s => s.Contains("papa")),
                new Rule<string>(s => s.Contains("bia"))
            };

            var bre = new DecisionRuleEngine<string>(DecisionLogic.And);
            var decision = bre.GetDecision(rules, "bia was here");
            Assert.IsFalse(decision);
        }

        [TestMethod]
        public void DecisionComplexOrPositive()
        {
            var rules = new List<IRule<Poco>>
            {
                new Rule<Poco>(s => s.String.Contains("my")),
                new Rule<Poco>(s => s.Int > 3),
                new Rule<Poco>(s => s.Bool)
            };

            var bre = new DecisionRuleEngine<Poco>();
            var decision = bre.GetDecision(rules, new Poco { String = "my string", Int = 5, Bool = true });
            Assert.IsTrue(decision);
        }
        
        [TestMethod]
        public void DecisionComplexOrNegative()
        {
            var rules = new List<IRule<Poco>>
            {
                new Rule<Poco>(s => s.String.Contains("my")),
                new Rule<Poco>(s => s.Int > 3),
                new Rule<Poco>(s => s.Bool)
            };

            var bre = new DecisionRuleEngine<Poco>();
            var decision = bre.GetDecision(rules, new Poco { String = "his string", Int = 2, Bool = false });
            Assert.IsFalse(decision);
        }

        [TestMethod]
        public void DecisionComplexAndPositive()
        {
            var rules = new List<IRule<Poco>>
            {
                new Rule<Poco>(s => s.String.Contains("my")),
                new Rule<Poco>(s => s.Int > 3),
                new Rule<Poco>(s => s.Bool)
            };

            var bre = new DecisionRuleEngine<Poco>(DecisionLogic.And);
            var decision = bre.GetDecision(rules, new Poco { String = "my string", Int = 5, Bool = true });
            Assert.IsTrue(decision);
        }

        [TestMethod]
        public void DecisionComplexAndNegative()
        {
            var rules = new List<IRule<Poco>>
            {
                new Rule<Poco>(s => s.String.Contains("my")),
                new Rule<Poco>(s => s.Int > 3),
                new Rule<Poco>(s => s.Bool)
            };

            var bre = new DecisionRuleEngine<Poco>(DecisionLogic.And);
            var decision = bre.GetDecision(rules, new Poco { String = "my string", Int = 5, Bool = false });
            Assert.IsFalse(decision);
        }

        [TestMethod]
        public void ScoreStringPositive()
        {
            var rules = new List<IRule<string>>
            {
                new Rule<string>(s => s.Contains("mama")),
                new Rule<string>(s => s.Contains("mia")),
                new Rule<string>(s => s.Contains("papa")),
                new Rule<string>(s => s.Contains("bia"))
            };

            var bre = new ScoreRuleEngine<string>(25);
            var decision = bre.GetDecision(rules, "bia was here");
            Assert.IsTrue(decision);
        }

        [TestMethod]
        public void ScoreStringNegative()
        {
            var rules = new List<IRule<string>>
            {
                new Rule<string>(s => s.Contains("mama")),
                new Rule<string>(s => s.Contains("mia")),
                new Rule<string>(s => s.Contains("papa")),
                new Rule<string>(s => s.Contains("bia"))
            };

            var bre = new ScoreRuleEngine<string>(50);
            var decision = bre.GetDecision(rules, "bia was here");
            Assert.IsFalse(decision);
        }

        [TestMethod]
        public void ScoreComplexPositive()
        {
            var rules = new List<IRule<Poco>>
            {
                new Rule<Poco>(s => s.String.Contains("my")),
                new Rule<Poco>(s => s.Int > 3),
                new Rule<Poco>(s => s.Bool)
            };

            var bre = new ScoreRuleEngine<Poco>(100);
            var decision = bre.GetDecision(rules, new Poco { String = "my string", Int = 5, Bool = true });
            Assert.IsTrue(decision);
        }

        [TestMethod]
        public void ScoreComplexNegative()
        {
            var rules = new List<IRule<Poco>>
            {
                new Rule<Poco>(s => s.String.Contains("my")),
                new Rule<Poco>(s => s.Int > 3),
                new Rule<Poco>(s => s.Bool)
            };

            var bre = new ScoreRuleEngine<Poco>(100);
            var decision = bre.GetDecision(rules, new Poco { String = "my string", Int = 5, Bool = false });
            Assert.IsFalse(decision);
        }

        [TestMethod]
        public void WeightedStringPositive()
        {
            var rules = new List<IRule<string>>
            {
                new Rule<string>(s => s.Contains("mama"), null, 10),
                new Rule<string>(s => s.Contains("mia"), null, 20),
                new Rule<string>(s => s.Contains("papa"), null, 10),
                new Rule<string>(s => s.Contains("bia"), null, 60)
            };

            var bre = new ScoreRuleEngine<string>(50);
            var decision = bre.GetDecision(rules, "bia was here");
            Assert.IsTrue(decision);
        }

        [TestMethod]
        public void WeightedStringNegative()
        {
            var rules = new List<IRule<string>>
            {
                new Rule<string>(s => s.Contains("mama"), null, 10),
                new Rule<string>(s => s.Contains("mia"), null, 20),
                new Rule<string>(s => s.Contains("papa"), null, 30),
                new Rule<string>(s => s.Contains("bia"), null, 40)
            };

            var bre = new ScoreRuleEngine<string>(50);
            var decision = bre.GetDecision(rules, "bia was here");
            Assert.IsFalse(decision);
        }

        [TestMethod]
        public void WeightedComplexPositive()
        {
            var rules = new List<IRule<Poco>>
            {
                new Rule<Poco>(s => s.String.Contains("my"), null, 50),
                new Rule<Poco>(s => s.Int > 3, null, 30),
                new Rule<Poco>(s => s.Bool, null, 20)
            };

            var bre = new ScoreRuleEngine<Poco>(100);
            var decision = bre.GetDecision(rules, new Poco { String = "my string", Int = 5, Bool = true });
            Assert.IsTrue(decision);
        }

        [TestMethod]
        public void WeightedComplexNegative()
        {
            var rules = new List<IRule<Poco>>
            {
                new Rule<Poco>(s => s.String.Contains("my"), null, 50),
                new Rule<Poco>(s => s.Int > 3, null, 30),
                new Rule<Poco>(s => s.Bool, null, 20)
            };

            var bre = new ScoreRuleEngine<Poco>(100);
            var decision = bre.GetDecision(rules, new Poco { String = "my string", Int = 5, Bool = false });
            Assert.IsFalse(decision);
        }

        [TestMethod]
        public void DecisionComplex()
        {
            var ruleSet = new RuleSet<Chain>
            {
                new Rule<Chain>(c => c.Nodes.SequenceEqual(new List<Node>
                {
                    new Node { Name = "Regression cycle too long", State = TriState.Yes },
                    new Node { Name = "Automation in use", State = TriState.Yes },
                    new Node { Name = "Automation coverage less then 80%", State = TriState.Yes },
                    new Node { Name = "Many production defects", State = TriState.NotRelevant }
                }, new NodeComparer()),
                "Improve automation coverage"),
                new Rule<Chain>(c => c.Nodes.SequenceEqual(new List<Node>
                {
                        new Node { Name = "Regression cycle too long", State = TriState.Yes },
                        new Node { Name = "Automation in use", State = TriState.Yes },
                        new Node { Name = "Automation coverage less then 80%", State = TriState.No },
                        new Node { Name = "Many production defects", State = TriState.NotRelevant }
                }, new NodeComparer()),
                "Improve automation efficiency"),
                new Rule<Chain>(c => c.Nodes.SequenceEqual(new List<Node>
                {
                        new Node { Name = "Regression cycle too long", State = TriState.Yes },
                        new Node { Name = "Automation in use", State = TriState.No },
                        new Node { Name = "Automation coverage less then 80%", State = TriState.NotRelevant },
                        new Node { Name = "Many production defects", State = TriState.Yes }
                }, new NodeComparer()),
                "Use automation")
            };

            var engine = new DecisionRuleEngine<Chain>();
            var outcome = engine.GetDecision(ruleSet, new Chain
            {
                Nodes = new List<Node>
                {
                        new Node { Name = "Regression cycle too long", State = TriState.Yes },
                        new Node { Name = "Automation in use", State = TriState.No },
                        new Node { Name = "Automation coverage less then 80%", State = TriState.NotRelevant },
                        new Node { Name = "Many production defects", State = TriState.Yes }
                }
            });

            Assert.IsTrue(outcome == "Use automation");
        }
    }

    public class Chain
    {
        public List<Node> Nodes { get; set; }
        public string Outcome { get; set; }
    }

    public class Node
    {
        public TriState State { get; set; }
        public string Name { get; set; }

        private bool Equals(Node other)
        {
            return State == other.State && string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Node)obj);
        }

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            unchecked
            {
                return ((int)State * 397) ^ (Name?.GetHashCode() ?? 0);
            }
        }

        public static bool operator ==(Node x, Node y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (((object) x == null) || ((object) y == null)) return false;
            return x.Name == y.Name && x.State == y.State;
        }

        public static bool operator !=(Node x, Node y)
        {
            return !(x == y);
        }
    }

    public class NodeComparer : IEqualityComparer<Node>
    {
        public bool Equals(Node x, Node y)
        {
            return x.Name == y.Name && x.State == y.State;
        }

        public int GetHashCode(Node obj)
        {
            return (obj.Name + obj.State).GetHashCode();
        }
    }

    [Flags]
    public enum TriState
    {
        Yes = 1,
        No = 0,
        NotRelevant = -1
    }

    public class Poco
    {
        public string String { get; set; }
        public int Int { get; set; }
        public bool Bool { get; set; }
    }
}
