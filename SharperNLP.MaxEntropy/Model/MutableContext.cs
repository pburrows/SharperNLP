using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharperNLP.MaxEntropy.Model
{
    /// <summary>
    /// Class used to store parameters or expected values associated with this context 
    /// which can be updated or assigned.
    /// </summary>
    public class MutableContext : Context
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="MutableContext"/> class with the specified parameters
        /// associated with the specified outcome pattern.
        /// </summary>
        /// <param name="outcommePattern">The outcome pattern.</param>
        /// <param name="parameters">The parameters.</param>
        public MutableContext(int[] outcomePattern, double[] parameters)
            : base(outcomePattern, parameters)
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Assigns the value of <paramref name="value"/> parameter to the context parameter or expected value specified by <paramref name="outcomeIndex"/>.
        /// </summary>
        /// <param name="outcomeIndex">Index of the outcome.</param>
        /// <param name="value">The value to be assigned.</param>
        public void SetParameter(int outcomeIndex, double value)
        {
            Parameters[outcomeIndex] = value;
        }

        /// <summary>
        /// Updates the parameter or expected value specified by <paramref name="outcomeIndex"/> by adding the <paramref name="value"/> 
        /// to current value of the parameter or expected value.
        /// </summary>
        /// <param name="outcomeIndex">Index of the outcome.</param>
        /// <param name="value">The value to be added.</param>
        public void UpdateParameter(int outcomeIndex, double value)
        {
            Parameters[outcomeIndex] += value;
        }

        /// <summary>
        /// Determines whether this instance contains the specified outcome.
        /// </summary>
        /// <param name="outcome">The outcome.</param>
        /// <returns>
        ///   <c>true</c> if this instance contains the specified outcome; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(int outcome)
        {
            return Outcomes.Contains<int>(outcome);
        }

        #endregion
    }
}
