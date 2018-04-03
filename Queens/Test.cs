using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Queens
{
    public class Test<T1,T2> where T1: class //where T2 : class
    {
        private readonly object _obj;
        public List<TestResult<T2>> Results { get; private set; }
        private readonly ITestConfig<T1, T2> _config;

        public Test(object obj, ITestConfig<T1, T2> config)
        {
            _obj = obj;
            _config = config;
        }

        public void Jit()
        {
            _config.Tests.First()(_obj as T1);
        }

        public void Run() 
        {
            Results = new List<TestResult<T2>>();
            var watch = new Stopwatch();
            foreach (var test in _config.Tests)
            {
                watch.Restart();
                var result = test(_obj as T1);
                watch.Stop();
                Results.Add(new TestResult<T2>(watch.Elapsed, result));
            }
        }

        public void PrintResults()
        {
            foreach (var result in Results)
            {
                Console.WriteLine("Duration: {0}\tResult: {1}", result.Duration, _config.GetResults(result.Result));
            }
        }
    }

    public class TestResult<T>
    {
        public TimeSpan Duration { get; }
        public T Result { get; }

        public TestResult(TimeSpan duration, T result)
        {
            Duration = duration;
            Result = result;
        }
    }
}
