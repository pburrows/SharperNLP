using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharperNLP.MaxEntropy.Model
{
    /// <summary>
    /// Class which associates a real valued parameter or expected value with a particular contextual predicate or feature.
    /// This is used to store maxent model parameters as well as model and empirical expected values.
    /// </summary>
    public class Context
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="Context"/> class with the specified parameters associated with 
        /// the specified outcome pattern.
        /// </summary>
        /// <param name="outcomePattern">An array of outcomes for which parameters exist for this context.</param>
        /// <param name="parameters">The parameters for the specified outcomes.</param>
        public Context(int[] outcomePattern, double[] parameters)
        {
            Outcomes = outcomePattern;
            Parameters = parameters;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the outcomes for which parameters exists for this context.
        /// </summary>
        /// <value>
        /// The outcomes.
        /// </value>
        public int[] Outcomes { get; protected set; }

        /// <summary>
        /// Gets parameters or expected values for the outcomes which occur in this context.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        public double[] Parameters { get; protected set; }

        #endregion
    }
}
