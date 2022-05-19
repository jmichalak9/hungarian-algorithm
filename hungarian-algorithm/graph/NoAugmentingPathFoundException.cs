using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graph
{
    public class NoAugmentingPathFoundException: Exception
    {
        public NoAugmentingPathFoundException()
        {
        }

        public NoAugmentingPathFoundException(string message)
            : base(message)
        {
        }
    }
}
