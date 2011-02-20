using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharperNLP.MaxEntropy.Model
{
    /// <summary>
    /// Class which turns a sequence stream into an event stream.
    /// </summary>
    public class SequenceStreamEventStream : IEventStream
    {
        #region Fields

        private IEnumerator<Sequence<Event>> _enumerator;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceStreamEventStream"/> class.
        /// </summary>
        /// <param name="sequenceStream">The sequence stream.</param>
        public SequenceStreamEventStream(ISequenceStream<Event> sequenceStream)
        {
            _enumerator = sequenceStream.GetEnumerator();
            EventIndex = -1;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the index of the event.
        /// </summary>
        /// <value>
        /// The index of the event.
        /// </value>
        internal int EventIndex { get; set; }

        /// <summary>
        /// Gets or sets the events.
        /// </summary>
        /// <value>
        /// The events.
        /// </value>
        internal Event[] Events { get; set; }

        #endregion

        #region IEventStream Methods

        /// <summary>
        /// Returns the next <see cref="Event"/> object from the stream.
        /// </summary>
        /// <returns>
        /// The next event from the stream.
        /// </returns>
        /// <exception cref="System.IO.IOException">The stream cannot be read.</exception>
        public Event Next()
        {
            return Events[EventIndex++];
        }

        /// <summary>
        /// Determines whether there are any remaining events in the stream.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if there are any remaining events in the stream; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="System.IO.IOException">
        /// The stream cannot be read.
        ///   </exception>
        public bool HasNext()
        {
            if (Events != null && EventIndex < Events.Length)
            {
                return true;
            }
            else
            {
                if (_enumerator.MoveNext())
                {
                    Sequence<Event> current = _enumerator.Current;
                    EventIndex = 0;
                    Events = current.Events;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        #endregion

        #region Public Methods

        public void Remove()
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}
