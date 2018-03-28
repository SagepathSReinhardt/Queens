using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using QueensLibrary;

namespace Queens
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new QueensTestConfig();

            Console.WriteLine("Searching for dlls");

            var results = config.GetDictonary();

            foreach (var dll in Directory.GetFiles(".", "*.dll")
                .Select(Assembly.LoadFrom)
                .SelectMany(assembly => assembly.GetExportedTypes())
                .Where(type => type.GetInterfaces().Contains(config.Type))
                .Select(Activator.CreateInstance)
                .OrderBy(q => q.GetType().FullName))
            {
                var test = config.GetTest(dll);
                test.Jit();
                test.Run();
                results[config.GetName(dll)] = test;
            }

            foreach (var result in results.OrderBy(r => config.GetRanking(r.Value)))
            {
                Console.WriteLine(result.Key);
                Console.WriteLine("-----------------");
                result.Value.PrintResults();
            }

            Console.WriteLine("Done");
//#if DEBUG
            Console.ReadKey();
//#endif
        }
    }
}
