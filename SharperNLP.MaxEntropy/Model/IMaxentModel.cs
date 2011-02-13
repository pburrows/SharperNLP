using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharperNLP.MaxEntropy.Model
{
    /// <summary>
    /// Interface for max entropy models.
    /// </summary>
    public interface IMaxentModel
    {
        #region Properties

        /// <summary>
        /// Gets the data structures relevant to storing the model.
        /// </summary>
        object[] DataStructures { get; }

        /// <summary>
        /// Gets the get number of outcomes. for this model.
        /// </summary>
        int NumberOfOutcomes { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Evaluates the specified context.
        /// </summary>
        /// <param name="context">A list of names of the contextual predicates which are to be evaluated together.</param>
        /// <returns>An array of the probabilities for each of the different outcomes, all of which sum to 1.</returns>
        double[] Evaluate(string[] context);

        /// <summary>
        /// Evaluates the specified context.
        /// </summary>
        /// <param name="context">A list of names of the contextual predicates which are to be evaluated together.</param>
        /// <param name="probabilities">
        /// An array which is populated with the probabilities for each of the different outcomes,
        /// all of which sum to 1.
        /// </param>
        /// <returns>An array of the probabilities for each of the different outcomes, all of which sum to 1.</returns>
        double[] Evaluate(string[] context, double[] probabilities);

        /// <summary>
        /// Evaluates the specified context with the specified context values.
        /// </summary>
        /// <param name="context">A list of names of the contextual predicates which are to be evaluated together.</param>
        /// <param name="values">The values associated with each context.</param>
        /// <returns>An array of the probabilities for each of the different outcomes, all of which sum to 1.</returns>
        double[] Evaluate(string[] context, float[] values);

        /// <summary>
        /// Gets the outcome associated with the index containing the highest probability in the
        /// <paramref name="outcomes"/> array.
        /// </summary>
        /// <param name="outcomes">
        /// An array of <see cref="System.Double"/> as returned
        /// by the <see cref="Evaluate(string[] context)"/> method.
        /// </param>
        /// <returns>The name of the best outcome.</returns>
        string GetBestOutcome(double[] outcomes);

        /// <summary>
        /// Returns a string matching all outcome names with all the probabilities as produced by the
        /// <see cref="Evaluate(string[] context)"/> method.
        /// </summary>
        /// <param name="outcomes">
        /// An array of <see cref="System.Double"/> as returned
        /// by the <see cref="Evaluate(string[] context)"/> method.
        /// </param>
        /// <returns>A string containing outcome names paired with the normalized probability for each one.</returns>
        string GetAllOutcomes(double[] outcomes);

        /// <summary>
        /// Gets the name of the outcome associated with the index <paramref name="outcomeIndex"/>.
        /// </summary>
        /// <param name="outcomeIndex">Index of the desired outcome.</param>
        /// <returns>The name of the outcome.</returns>
        string GetOutcome(int outcomeIndex);

        /// <summary>
        /// Gets the index associated with the given outcome.
        /// </summary>
        /// <param name="outcome">The outcome for which the index is desired.</param>
        /// <returns>The index if the given outcome exists for the model; -1 otherwise.</returns>
        int GetIndex(string outcome);

        #endregion
    }
}
