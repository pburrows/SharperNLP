using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharperNLP.MaxEntropy.Model
{
    /// <summary>
    /// Objects which compresses events in memory and performs feature selection.
    /// </summary>
    public interface IDataIndexer
    {
        /// <summary>
        /// Returns the array of predicates seen in each event.
        /// </summary>
        /// <value>
        /// A 2 dimension array whose first dimension is the event index and array.
        /// </value>
        int[][] Contexts { get; }

        /// <summary>
        /// Gets the number of times a particular event was seen.
        /// </summary>
        int[] NumberOfTimesEventsSeen { get; }

        /// <summary>
        /// Gets the array containing outcome index for each event.
        /// </summary>
        int[] OutcomeList { get; }

        /// <summary>
        /// Gets the predicate/context names.
        /// </summary>
        /// <value>
        /// An array of predicate/context names indexed by context index. These indices are the value of the array returned by
        /// <see cref="Contexts"/> property.
        /// </value>
        string[] PredicateLabels { get; }

        /// <summary>
        /// Gets an array of the count of each predicate in the events.
        /// </summary>
        int[] PredicateCounts { get; }

        /// <summary>
        /// Gets the array of outcome names.
        /// </summary>
        string[] OutcomeLabels { get; }

        /// <summary>
        /// Gets the values associated with each event context or null if integer values are to be used.
        /// </summary>
        float[][] Values { get; }

        /// <summary>
        /// Gets the number of total events indexed.
        /// </summary>
        int NumberOfEvents { get; }
    }
}
