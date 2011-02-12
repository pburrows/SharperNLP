using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharperNLP.MaxEntropy.MaxEnt
{
    /// <summary>
    /// Provides counting functionality.
    /// </summary>
    public class Counter
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="Counter"/> class.
        /// </summary>
        public Counter()
        {
            Value = 1;
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets the value.
        /// </summary>
        public int Value { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Increments the <see cref="Value"/> of this instance.
        /// </summary>
        public void Increment()
        {
            Value += 1;
        }

        /// <summary>
        /// Determines whether the <see cref="Value"/> of this counter passes the <paramref name="cutoff"/> value.
        /// </summary>
        /// <param name="cutoff">The cutoff value to test against.</param>
        /// <returns>
        ///     <c>true</c> if the counter is greater or equal to the cutoff value; <c>false</c> otherwise.
        /// </returns>
        public bool PassesCutoff(int cutoff)
        {
            return Value >= cutoff;
        }

        #endregion
    }
}
