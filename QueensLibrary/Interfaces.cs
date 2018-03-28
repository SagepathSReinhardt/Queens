using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueensLibrary
{
    public interface IQueensSolver
    {
        string Name { get; }
        IEnumerable<ISolution> GetSolutions(int size);
    }

    public interface ISolution
    {
        IEnumerable<Queen> Queens { get; }
    }

    public abstract class Queen
    {
        public abstract int Rank { get; }
        public abstract char File { get; }

        public override string ToString()
        {
            return File.ToString() + Rank;
        }
    }
}
