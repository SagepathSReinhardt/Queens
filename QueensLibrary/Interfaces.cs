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
        int GetSolutionCount(int size);
    }

    public interface ISolution
    {
        IEnumerable<Queen> Queens { get; }
    }

    public abstract class Queen
    {
        public abstract byte Rank { get; }
        
        //0 = A, 1 = B, etc
        public abstract byte File { get; }

        public override string ToString()
        {
            return ('A'+File).ToString() + Rank;
        }
    }
}
