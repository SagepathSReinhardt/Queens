using System;
using System.Collections.Generic;
using System.Linq;
using QueensLibrary;

namespace Queens
{
    public class QueensCountTestConfig : ITestConfig<IQueensSolver, int>
    {
        public Type Type => typeof(IQueensSolver);
        public Type ResultType => typeof(IEnumerable<ISolution>);

        public Func<object, string> GetName
        {
            get
            {
                return s =>
                {
                    var queensSolver = s as IQueensSolver;
                    return queensSolver != null ? queensSolver.Name : "";
                };
            }
        }

        public Func<object, Test<IQueensSolver, int>> GetTest
        {
            get { return o => new Test<IQueensSolver, int>(o, this); }
        }

        public Func<int, string> GetResults
        {
            get { return l => l.ToString(); }

            //get { return l => string.Join("\r\n", l.Select(s => string.Join(" ", s.Queens.Select(q => q.ToString())))); }
        }

        public Func<Test<IQueensSolver, int>, double> GetRanking
        {
            get { return t => t.Results.Sum(r => r.Duration.TotalMilliseconds); }
        }


        public Func<Dictionary<string, Test<IQueensSolver, int>>> GetDictonary
        {
            get
            {
                return () => new Dictionary<string, Test<IQueensSolver, int>>();
            }
        }

        public List<Func<IQueensSolver, int>> Tests
        {
            get
            {
                var count = 17;
                var list = new List<Func<IQueensSolver, int>>(count);
                for (var i = 1; i <= count; i++)
                {
                    var j = i;
                    list.Add(s => s.GetSolutionCount(j));
                }

                return list;
            }
        }
    }
}
