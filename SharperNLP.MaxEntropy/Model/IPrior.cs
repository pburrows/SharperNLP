using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharperNLP.MaxEntropy.Model
{
    /// <summary>
    /// This interface allows to implement a prior distribution for use in maximum entropy model training.
    /// </summary>
    public interface IPrior
    {
        /// <summary>
        /// Returns an array with the log of the distribution for the specified context.
        /// </summary>
        /// <param name="context">The indices of the contextual predicates for an event.</param>
        /// <returns>An array with the log of the distribution.</returns>
        /// <remarks>
        /// This method differs from the original Java method <code>Prior.logPrior(double[] dist, int[] context)</code> for the 
        /// purpose of forcing the future implementations of this method to return a new array instead of using a <c>ref</c> modifier
        /// for the input.
        /// </remarks>
        double[] LogPrior(int[] context);

        /// <summary>
        /// Returns an array with the log of the distribution for the specified context.
        /// </summary>
        /// <param name="context">The indices of the contextual predicates for an event.</param>
        /// <param name="values">The values associated with the context.</param>
        /// <returns>An array with the log of the distribution.</returns>
        /// <remarks>
        /// This method differs from the original Java method <code>Prior.logPrior(double[] dist, int[] context, float[] values)</code> for the 
        /// purpose of forcing the future implementations of this method to return a new array instead of using a <c>ref</c> modifier
        /// for the input.
        /// </remarks>
        double[] LogPrior(int[] context, float[] values);

        /// <summary>
        /// Specifies the labels for the outcomes and contexts.
        /// This method is used to map integer outcomes and contexts to their string values. This method is
        /// called prior to any call to <see cref="LogPrior"/>.
        /// </summary>
        /// <param name="outcomeLabels">The outcome labels.</param>
        /// <param name="contextLabels">The context labels.</param>
        void SetLabels(string[] outcomeLabels, string[] contextLabels);
    }
}