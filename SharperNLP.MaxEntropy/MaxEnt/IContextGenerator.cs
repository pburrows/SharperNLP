using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharperNLP.MaxEntropy.MaxEnt
{
    /// <summary>
    /// Generate contexts for maxent decisions. 
    /// </summary>
    public interface IContextGenerator
    {
        /// <summary>
        /// Builds up the contextual predicates given an <see cref="Object"/>.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        string[] GetContext(object obj);
    }
}
