using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharperNLP.MaxEntropy.Model
{
    /// <summary>
    /// A <see cref="Event"/> representation that can be used in sorting operations
    /// based on the predicate indices contained in the events.
    /// </summary>
    public class ComparableEvent : IComparable<ComparableEvent>, IComparable
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="ComparableEvent"></see> class.
        /// </summary>
        /// <param name="outcome">The outcome.</param>
        /// <param name="predicateIndices">The predicate indices.</param>
        public ComparableEvent(int outcome, int[] predicateIndices)
            : this(outcome, predicateIndices, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComparableEvent"/> class.
        /// </summary>
        /// <param name="outcome">The outcome.</param>
        /// <param name="predicateIndices">The predicate indices.</param>
        /// <param name="values">The values.</param>
        public ComparableEvent(int outcome, int[] predicateIndices, float[] values)
        {
            Seen = 1;
            Outcome = outcome;
            if (Values != null)
            {
                Array.Sort<int, float>(predicateIndices, values);
            }
            else
            {
                Array.Sort<int>(predicateIndices);
            }
            PredicateIndices = predicateIndices;
            Values = values;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the outcome.
        /// </summary>
        /// <value>
        /// The outcome.
        /// </value>
        public int Outcome { get; set; }

        /// <summary>
        /// Gets or sets the predicate indices.
        /// </summary>
        /// <value>
        /// The predicate indices.
        /// </value>
        public int[] PredicateIndices { get; set; }

        /// <summary>
        /// Gets or sets the number of times this event has been seen.
        /// </summary>
        /// <value>
        /// The seen.
        /// </value>
        public int Seen { get; set; }

        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        /// <value>
        /// The values.
        /// </value>
        public float[] Values { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has the following meanings:
        /// Value
        /// Meaning
        /// Less than zero
        /// This object is less than the <paramref name="other"/> parameter.
        /// Zero
        /// This object is equal to <paramref name="other"/>.
        /// Greater than zero
        /// This object is greater than <paramref name="other"/>.
        /// </returns>
        public int CompareTo(ComparableEvent other)
        {
            if (Outcome < other.Outcome)
                return -1;
            if (Outcome > other.Outcome)
                return 1;
            int minLength = Math.Min(PredicateIndices.Length, other.PredicateIndices.Length);
            for (int i = 0; i < minLength; i++)
            {
                if (PredicateIndices[i] < other.PredicateIndices[i])
                    return -1;
                if (PredicateIndices[i] > other.PredicateIndices[i])
                    return 1;
                if (Values != null && other.Values != null)
                {
                    if (Values[i] < other.Values[i])
                        return -1;
                    if (Values[i] > other.Values[i])
                        return 1;
                }
                if (Values != null)
                {
                    if (Values[i] < 1)
                        return -1;
                    if (Values[i] > 1)
                        return 1;
                }
                if (other.Values != null)
                {
                    if (1 < other.Values[i])
                        return -1;
                    if (1 > other.Values[i])
                        return 1;
                }
            }
            return PredicateIndices.Length.CompareTo(other.PredicateIndices.Length);
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance is less than <paramref name="obj"/>. Zero This instance is equal to <paramref name="obj"/>. Greater than zero This instance is greater than <paramref name="obj"/>.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">
        ///   <paramref name="obj"/> is not the same type as this instance. </exception>
        public int CompareTo(object obj)
        {
            if (obj is ComparableEvent)
            {
                return CompareTo(obj as ComparableEvent);
            }
            throw new ArgumentException("Parameter is of different type.", "obj");
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}:", Outcome);
            for (int i = 0; i < PredicateIndices.Length; i++)
            {
                sb.AppendFormat(" {0}", PredicateIndices[i]);
                if (Values != null)
                {
                    sb.AppendFormat("={0}", Values[i]);
                }
            }
            return sb.ToString();
        }

        #endregion
    }
}
