using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueensLibrary;

namespace Queens
{
    public interface ITestConfig<T1,T2> where T1 : class //where T2 : class
    {
        Type Type { get; }

        Type ResultType { get; }

        List<Func<T1, T2>> Tests { get; }

        Func<object, string> GetName { get; }

        Func<object,Test<T1,T2>> GetTest { get; }

        Func<T2,string> GetResults { get; }

        Func<Test<T1, T2>, double> GetRanking { get; }

        Func<Dictionary<string,Test<T1,T2>>> GetDictonary { get; }
    }
}
