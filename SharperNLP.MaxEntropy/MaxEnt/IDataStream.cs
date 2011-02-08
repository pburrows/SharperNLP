using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharperNLP.MaxEntropy.MaxEnt
{
    public interface IDataStream
    {
        /// <summary>
        /// Returns the next slice of data held in this <see cref="IDataStream"/>.
        /// </summary>
        /// <returns>
        /// The object representing the data which is next in this <see cref="IDataStream"/>.
        /// </returns>
        object NextToken();

        /// <summary>
        /// Determines whether there are any data tokens remaining in this stream.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance has any more data tokens; otherwise, <c>false</c>.
        /// </returns>
        bool HasNext();
    }
}
