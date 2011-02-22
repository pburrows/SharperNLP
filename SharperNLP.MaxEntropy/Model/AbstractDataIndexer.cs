using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace SharperNLP.MaxEntropy.Model
{
    /// <summary>
    /// Abstract class for collecting event and context counts used in training.
    /// </summary>
    public abstract class AbstractDataIndexer : IDataIndexer
    {
        #region Public Properties

        public int[][] Contexts { get; protected set; }

        public int[] NumberOfTimesEventsSeen { get; protected set; }

        public int[] OutcomeList { get; protected set; }

        public string[] PredicateLabels { get; protected set; }

        public int[] PredicateCounts { get; protected set; }

        public string[] OutcomeLabels { get; protected set; }

        public float[][] Values
        {
            get { return null; }
        }

        public int NumberOfEvents { get; private set; }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Sorts the collection of <see cref="ComparableEvent"/> and returns the number of distinct elements.
        /// This method will alter the collection - it does a sort over it and removes the duplicates.
        /// </summary>
        /// <param name="eventsToCompare">The events to compare.</param>
        /// <param name="sort">if set to <c>true</c> sort the collection.</param>
        /// <returns>The number of distinct elements in the collection.</returns>
        protected int SortAndMerge(List<ComparableEvent> eventsToCompare, bool sort)
        {
            NumberOfEvents = eventsToCompare.Count;
            int uniqueEvents = SortAndRemoveDuplicates(eventsToCompare, sort);
            InitializeArrays(uniqueEvents);
            FillArrays(eventsToCompare);
            return uniqueEvents;
        }

        /// <summary>
        /// Updates the set of predicates and counter with the specified event contexts and cutoff.
        /// </summary>
        /// <param name="contexts">The contexts/features which occur in an event.</param>
        /// <param name="predicatesSet">The set of predicates which will be used for model building.</param>
        /// <param name="counter">The predicate counters.</param>
        /// <param name="cutoff">The cutoff value which determines if a predicate is included.</param>
        protected static void Update(string[] contexts, HashSet<string> predicatesSet, IDictionary<string, int> counter, int cutoff)
        {
            for (int i = 0; i < contexts.Length; i++)
            {
                string context = contexts[i];
                if (!counter.ContainsKey(context))
                {
                    counter[context] = 1;
                }
                else
                {
                    counter[context] += 1;
                }
                if (!predicatesSet.Contains(context) && counter[context] >= cutoff)
                {
                    predicatesSet.Add(context);
                }
            }
        }

        /// <summary>
        /// Utility method for creating a read-only collection of strings from a dictionary whose
        /// keys are labels to be stored in the array and whose values are the indices at which the corresponding 
        /// labels should be inserted.
        /// </summary>
        /// <param name="labelToIndexMap">The label to index map.</param>
        /// <returns>A <see cref="System.Collections.ObjectModel.ReadOnlyCollection"/> of strings.</returns>
        protected static ReadOnlyCollection<string> ToIndexedStringArray(IDictionary<string, int> labelToIndexMap)
        {
            List<string> list = new List<string>(labelToIndexMap.Count);
            foreach (var label in labelToIndexMap.Keys)
            {
                list[labelToIndexMap[label]] = label;
            }
            return new ReadOnlyCollection<string>(list);
        }

        #endregion

        #region Private Methods

        private void FillArrays(List<ComparableEvent> eventsToCompare)
        {
            int j = 0;
            for (int i = 0; i < NumberOfEvents; i++)
            {
                ComparableEvent ce = eventsToCompare[i];
                if (ce != null)
                {
                    NumberOfTimesEventsSeen[j] = ce.Seen;
                    OutcomeList[j] = ce.Outcome;
                    Contexts[j] = ce.PredicateIndices;
                    j++;
                }
            }
        }


        private void InitializeArrays(int uniqueEvents)
        {
            Contexts = new int[uniqueEvents][];
            OutcomeList = new int[uniqueEvents];
            NumberOfTimesEventsSeen = new int[uniqueEvents];
        }

        private int SortAndRemoveDuplicates(List<ComparableEvent> eventsToCompare, bool sort)
        {
            int uniqueEvents = 1;
            if (sort)
            {
                eventsToCompare.Sort();
                if (NumberOfEvents <= 1)
                {
                    return uniqueEvents;
                }
                ComparableEvent ce1 = eventsToCompare[0];
                for (int i = 1; i < eventsToCompare.Count; i++)
                {
                    ComparableEvent ce2 = eventsToCompare[i];
                    if (ce1.CompareTo(ce2) == 0)
                    {
                        ce1.Seen++;
                        eventsToCompare[i] = null;
                    }
                    else
                    {
                        ce1 = ce2;
                        uniqueEvents++;
                    }
                }
            }
            else
            {
                uniqueEvents = eventsToCompare.Count;
            }
            return uniqueEvents;
        }

        #endregion
    }
}
