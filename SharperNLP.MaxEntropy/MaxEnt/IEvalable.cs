using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharperNLP.MaxEntropy.Model;
using System.IO;

namespace SharperNLP.MaxEntropy.MaxEnt
{
    /// <summary>
    /// Interface for components which use maximum entropy models and can evaluate
    /// the performance of the models using <see cref="TrainEval"/> class.
    /// </summary>
    public interface IEvalable
    {
        #region Properties

        /// <summary>
        /// Gets the outcome that should be considered a negative result.
        /// This is used for computing recall.
        /// </summary>
        string NegativeOutcome { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the <see cref="Model.IEventCollector"/> that is used to collect relevant information 
        /// from the data file. This is used to test the predictions of the model.
        /// </summary>
        /// <param name="reader">The reader containing the data for the Event collector.</param>
        /// <returns>An <see cref="Model.IEventCollector"/>.</returns>
        /// <remarks>
        /// If some of your features are the outcomes of previous events, this method will give you results assuming 100%
        /// performance on the previous events.
        /// If you don't like this, use the <see cref="LocalEvaluation"/> method.
        /// </remarks>
        IEventCollector GetEventCollector(TextReader reader);

        /// <summary>
        /// If the -1 option is selected for evaluation, this method will be called rather 
        /// than training evaluation method. This is good if your features includes the outcome of previous events.
        /// </summary>
        /// <param name="model">The model to evaluate.</param>
        /// <param name="reader">The reader containing the data to process.</param>
        /// <param name="evalable">The original evalable.</param>
        /// <param name="verbose">if set to <c>true</c> print more specific processing information.</param>
        void LocalEvaluation(IMaxentModel model, TextReader reader, IEvalable evalable, bool verbose);

        #endregion
    }
}
