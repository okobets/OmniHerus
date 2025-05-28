using System.Globalization;
using WFEngine;

namespace WFEngineTests
{
    [TestClass]
    public sealed class WorkflowTests
    {
        private Workflow<Title, Title> workflow;
        private Title title;
            
        [TestInitialize]
        public void Setup()
        {
            workflow = new Workflow<Title, Title>
            {
                Activities = new List<IActivity<Title, Title>>
                {
                    new Activity<Title, Title>
                    {
                        Name = "1",
                        Content = (t, c) =>
                        {
                            var n = t.Name.ToCharArray();
                            n[3] = n[3].ToString(CultureInfo.InvariantCulture).ToUpperInvariant().ToCharArray()[0];
                            t.Name = new string(n);
                            t.Popularity = 3;
                            return t;
                        }
                    },
                    new Activity<Title, Title>
                    {
                        Name = "2",
                        Content = (t, c) =>
                        {
                            t.Popularity++;
                            return t;
                        }
                    },
                    new Activity<Title, Title>
                    {
                        Name = "3",
                        Content = (t, c) =>
                        {
                            t.Popularity++;
                            return t;
                        }
                    }
                }
            };

            title = new Title
            {
                Name = "12 monkeys",
                Category = Category.Tv,
            };
            title.Similar.Add(new Title
            {
                Name = "Defiance",
                Category = Category.Tv
            });
        }

        [TestMethod]
        public void CreateWorkflow()
        {
            var workflowEngine = new WorkflowEngine<Title, Title>(workflow);
            var result = workflowEngine.ExecuteWorkflow(title, TimeSpan.FromMinutes(2));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Name == "12 Monkeys");
            Assert.IsTrue(result.Category == Category.Tv);
            Assert.IsTrue(result.Popularity == 5);
            Assert.IsTrue(result.Similar[0].Name == "Defiance");
        }

        [TestMethod]
        public void StopAsync()
        {
            workflow.Activities[1].Content = (t, c) => {
                                                      while (true)
                                                      {
                                                          if (c.IsCancellationRequested) break;
                                                          Thread.Sleep(100);
                                                      }
                                                      return t;
            };

            var workflowEngine = new WorkflowEngine<Title, Title>(workflow);
            var task = workflowEngine.ExecuteWorkflowAsync(title, TimeSpan.FromMinutes(2));
            Thread.Sleep(2000);
            workflowEngine.StopExecution();
            Task.WaitAll(task);

            Assert.IsNotNull(task.Result);
            Assert.IsTrue(task.Result.Popularity == 3);
            Assert.IsTrue(task.Result.Name == "12 Monkeys");
        }

        [TestMethod]
        public void Dynamic()
        {
            var mw = new Workflow<dynamic, dynamic>
            {
                Activities = new List<IActivity<dynamic, dynamic>>
                {
                    new Activity<dynamic, dynamic>
                    {
                        Name = "1",
                        Content = (t, c) => t is string ? new Title {Name = t} : null
                    },
                    new Activity<dynamic, dynamic>
                    {
                        Name = "2",
                        Content = (t, c) => t is Title ? new List<Title> { t } : null
                    },
                    new Activity<dynamic, dynamic>
                    {
                        Name = "3",
                        Content = (t, c) => t is List<Title> ? t[0].Name : null
                    }
                }
            };

            const string input = "Hello world!";
            var workflowEngine = new WorkflowEngine<dynamic, dynamic>(mw);
            var result = workflowEngine.ExecuteWorkflow(input, TimeSpan.FromMinutes(2));
            Assert.IsNotNull(result);
            Assert.IsTrue(result == input);
        }
    }

    public class Title
    {
        public Title()
        {
            Similar = new List<Title>();
        }

        public string Name { get; set; }
        public Category Category { get; set; }
        public List<Title> Similar { get; set; }
        public int Popularity { get; set; }
    }

    public enum Category
    {
        Movie,
        Tv
    }
}
