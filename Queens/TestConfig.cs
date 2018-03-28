using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueensLibrary;

namespace Queens
{
    public interface ITestConfig<T1,T2> where T1 : class where T2 : class
    {
        Type Type { get; }

        Type ResultType { get; }

        List<Func<T1, T2>> Tests { get; }

        Func<object, string> GetName { get; }

        Func<object,Test<T1,T2>> GetTest { get; }

        Func<T2,string> GetResults { get; }

        Func<Test<IQueensSolver, IEnumerable<ISolution>>, double> GetRanking { get; }

        Func<Dictionary<string,Test<T1,T2>>> GetDictonary { get; }
    }

    public class QueensTestConfig : ITestConfig<IQueensSolver, IEnumerable<ISolution>>
    {
        public Type Type => typeof(IQueensSolver);
        public Type ResultType => typeof(IEnumerable<ISolution>);

        public Func<object, string> GetName
        {
            get { return s =>
            {
                var queensSolver = s as IQueensSolver;
                return queensSolver != null ? queensSolver.Name : "";
            }; }
        }

        public Func<object, Test<IQueensSolver, IEnumerable<ISolution>>> GetTest
        {
            get { return o => new Test<IQueensSolver, IEnumerable<ISolution>>(o, this); }
        }

        public Func<IEnumerable<ISolution>,string> GetResults
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
                for (var i = 0; i <= 15; i++)
                {
                    var j = i;
                    list.Add(s => s.GetSolutions(j));
                }

                return list;
            }
        }
    }
}
