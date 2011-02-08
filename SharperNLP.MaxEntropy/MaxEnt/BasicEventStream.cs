using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharperNLP.MaxEntropy.Model;

namespace SharperNLP.MaxEntropy.MaxEnt
{
    public class BasicEventStream : AbstractEventStream
    {
        #region Fields

        private readonly string _separator = " ";
        private IContextGenerator _contextGenerator;
        private IDataStream _dataStream;
        private Event _nextEvent;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicEventStream"></see> class.
        /// </summary>
        /// <param name="dataStream">The data stream.</param>
        public BasicEventStream(IDataStream dataStream)
            : this(dataStream, " ")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicEventStream"/> class.
        /// </summary>
        /// <param name="dataStream">The data stream.</param>
        /// <param name="separator">The separator.</param>
        public BasicEventStream(IDataStream dataStream, string separator)
        {
            _dataStream = dataStream;
            _separator = separator;

            _contextGenerator = new BasicContextGenerator(_separator);
            if (_dataStream.HasNext())
            {
                _nextEvent = CreateEvent((string)_dataStream.NextToken());
            }
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Returns the next Event object held in this EventStream. Each call to nextEvent advances the EventStream.
        /// </summary>
        /// <returns>
        /// The Event object which is next in this EventStream if there are any events in the stream;
        /// otherwise <c>null</c>.
        /// </returns>
        public override Event Next()
        {
            LoopForNextEvent();
            Event currentEvent = _nextEvent;
            if (_dataStream.HasNext())
            {
                _nextEvent = CreateEvent((string)_dataStream.NextToken());
            }
            else
            {
                _nextEvent = null;
            }
            return currentEvent;
        }

        /// <summary>
        /// Determines whether there are any events remaining in this stream.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if stream has more events; otherwise, <c>false</c>.
        /// </returns>
        public override bool HasNext()
        {
            LoopForNextEvent();
            return _nextEvent != null;
        }

        #endregion

        #region Private Methods

        private Event CreateEvent(string obs)
        {
            int lastSeparator = obs.LastIndexOf(_separator);
            if (lastSeparator < 0)
            {
                return null;
            }
            Event e = new Event(obs.Substring(lastSeparator + 1), _contextGenerator.GetContext(obs.Substring(0, lastSeparator)));
            return e;
        }

        private void LoopForNextEvent()
        {
            while (_nextEvent == null && _dataStream.HasNext())
            {
                _nextEvent = CreateEvent((string)_dataStream.NextToken());
            }
        }

        #endregion
    }
}
