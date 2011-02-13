using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharperNLP.MaxEntropy.Model
{
    /// <summary>
    /// An interface for objects which read events during training.
    /// </summary>
    public interface IEventCollector
    {
        #region Properties

        /// <summary>
        /// Gets the events gathered by this collector. It must get its data from a constructor.
        /// </summary>
        Event[] Events { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the events this collector has gathered based on whether we wish to train a model or evaluate one based
        /// on those events.
        /// </summary>
        /// <param name="evaluationMode">
        /// <c>true</c> if the model is evaluating based on the events.
        /// <c>false</c> if the model is training.
        /// </param>
        /// <returns></returns>
        Event[] GetEvents(bool evaluationMode);

        #endregion
    }
}
