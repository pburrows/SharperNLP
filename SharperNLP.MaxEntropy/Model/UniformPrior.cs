using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharperNLP.MaxEntropy.Model
{
    /// <summary>
    /// Provides a maximum entropy model with a uniform prior.
    /// </summary>
    public class UniformPrior : IPrior
    {
        #region Fields

        private int _numberOfOutcomes;
        private double _r;

        #endregion

        #region IPrior Members

        /// <summary>
        /// Returns an array with the log of the distribution for the specified context.
        /// </summary>
        /// <param name="context">The indices of the contextual predicates for an event.</param>
        /// <returns>
        /// An array with the log of the distribution.
        /// </returns>
        public double[] LogPrior(int[] context)
        {
            return LogPrior(context, null);
        }

        /// <summary>
        /// Returns an array with the log of the distribution for the specified context.
        /// </summary>
        /// <param name="context">The indices of the contextual predicates for an event.</param>
        /// <param name="values">The values associated with the context.</param>
        /// <returns>
        /// An array with the log of the distribution.
        /// </returns>
        public double[] LogPrior(int[] context, float[] values)
        {
            double[] result = new double[_numberOfOutcomes];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = _r;
            }
            return result;
        }

        /// <summary>
        /// Specifies the labels for the outcomes and contexts.
        /// This method is used to map integer outcomes and contexts to their string values. This method is
        /// called prior to any call to <see cref="LogPrior"/>.
        /// </summary>
        /// <param name="outcomeLabels">The outcome labels.</param>
        /// <param name="contextLabels">The context labels.</param>
        public void SetLabels(string[] outcomeLabels, string[] contextLabels)
        {
            _numberOfOutcomes = outcomeLabels.Length;
            _r = Math.Log(1.0 / _numberOfOutcomes);
        }

        #endregion
    }
}
