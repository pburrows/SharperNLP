using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharperNLP.MaxEntropy.Model
{
    /// <summary>
    /// An object which can deliver a stream of training events for the GIS procedure.
    /// Event streams don't need to use a <see cref="MaxEntropy.DataStream"/>, but doing so would 
    /// provide greater flexibility for producing events from data stored in different formats.
    /// </summary>
    public interface IEventStream
    {
        /// <summary>
        ///Returns the next <see cref="Event"/> object from the stream.
        /// </summary>
        /// <returns>
        /// The next event from the stream.
        /// </returns>
        /// <exception cref="System.IO.IOException">The stream cannot be read.</exception>
        Event Next();

        /// <summary>
        /// Determines whether there are any remaining events in the stream.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if there are any remaining events in the stream; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="System.IO.IOException">
        /// The stream cannot be read.
        /// </exception>
        bool HasNext();
    }
}
