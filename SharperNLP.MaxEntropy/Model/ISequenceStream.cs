using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharperNLP.MaxEntropy.Model
{
    /// <summary>
    /// Interface for streams of sequences used to trains sequence models.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISequenceStream<T> : IEnumerable<Sequence<T>>
    {
        /// <summary>
        /// Creates a new event array based on the outcomes predicted by the specified parameters for the specified sequence.
        /// </summary>
        /// <param name="sequence">The sequence to be evaluated.</param>
        /// <param name="model">The parameters of the current model.</param>
        /// <returns></returns>
        Event[] UpdateContext(Sequence<T> sequence, AbstractModel model);
    }
}
