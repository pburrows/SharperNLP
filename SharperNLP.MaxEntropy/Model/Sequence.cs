using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharperNLP.MaxEntropy.Model
{
    /// <summary>
    /// Class which models a sequence.
    /// </summary>
    /// <typeparam name="T">The type of the object which is a source of this sequence.</typeparam>
    public class Sequence<T>
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="Sequence&lt;T&gt;"/> class made up by specified events and 
        /// derived from specified source.
        /// </summary>
        /// <param name="events">The events of the sequence.</param>
        /// <param name="source">The source object of the sequence.</param>
        public Sequence(Event[] events, T source)
        {
            Events = events;
            Source = source;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the events which make up this sequence.
        /// </summary>
        public Event[] Events { get; private set; }

        /// <summary>
        /// Gets the object from which this sequence can be derived.
        /// This object is used when the events for this sequence need to be re-derived such as in a call to 
        /// <see cref="SequenceStream.UpdateContext"/>.
        /// </summary>
        public T Source { get; private set; }

        #endregion
    }
}
