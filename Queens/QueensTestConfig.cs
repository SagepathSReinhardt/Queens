using System;
using System.Collections.Generic;
using System.Linq;
using QueensLibrary;

namespace Queens
{
    public class QueensTestConfig : ITestConfig<IQueensSolver, IEnumerable<ISolution>>
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

        public Func<object, Test<IQueensSolver, IEnumerable<ISolution>>> GetTest
        {
            get { return o => new Test<IQueensSolver, IEnumerable<ISolution>>(o, this); }
        }

        public Func<IEnumerable<ISolution>, string> GetResults
        {
            get { return l => l.Count().ToString(); }

            //get { return l => string.Join("\r\n", l.Select(s => string.Join(" ", s.Queens.Select(q => q.ToString())))); }
        }

        public Func<Test<IQueensSolver, IEnumerable<ISolution>>, double> GetRanking
        {
            get { return t => t.Results.Sum(r => r.Duration.TotalMilliseconds); }
        }


        public Func<Dictionary<string, Test<IQueensSolver, IEnumerable<ISolution>>>> GetDictonary
        {
            get
            {
                return () => new Dictionary<string, Test<IQueensSolver, IEnumerable<ISolution>>>();
            }
        }

        public List<Func<IQueensSolver, IEnumerable<ISolution>>> Tests
        {
            get
            {
                var list = new List<Func<IQueensSolver, IEnumerable<ISolution>>>(12);
                for (var i = 1; i <= 17; i++)
                {
                    var j = i;
                    list.Add(s => s.GetSolutions(j).ToArray());
                }

                return list;
            }
        }
    }
}
