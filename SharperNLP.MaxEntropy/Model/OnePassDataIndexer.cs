using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharperNLP.MaxEntropy.Model
{
    /// <summary>
    /// An indexer for max entropy model data which handles cutoffs for uncommon
    /// contextual predicates and provides a unique integer index for each of the predicates.
    /// </summary>
    public class OnePassDataIndexer : AbstractDataIndexer
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="OnePassDataIndexer"/> class.
        /// </summary>
        /// <param name="eventStream">The event stream which contains all the events seen in the training data.</param>
        /// <param name="cutoff">The minimum number of times a predicate must have been observed 
        /// in order to be included in the model.</param>
        /// <param name="sort">if set to <c>true</c> sort the predicates.</param>
        public OnePassDataIndexer(IEventStream eventStream, int cutoff, bool sort)
        {
            Dictionary<string, int> predicateIndex = new Dictionary<string, int>();
            Console.WriteLine("Indexing events using cutoff of {0}", cutoff);
            Console.Write("Computing event counts...");
            LinkedList<Event> events = ComputeEventCounts(eventStream, predicateIndex, cutoff);
            Console.WriteLine("done. {0} events.", events.Count);
            Console.Write("Indexing...");
            List<ComparableEvent> eventsToCompare = Index(events, predicateIndex);
            Console.WriteLine("done.");
            Console.Write("Sorting and merging events...");
            SortAndMerge(eventsToCompare, sort);
            Console.WriteLine("Done indexing");
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Indexes the specified events.
        /// </summary>
        /// <param name="events">The events.</param>
        /// <param name="predicateIndex">Index of the predicate.</param>
        /// <returns></returns>
        protected List<ComparableEvent> Index(LinkedList<Event> events, Dictionary<string, int> predicateIndex)
        {
            Dictionary<string, int> outcomeMap = new Dictionary<string, int>();
            int numberOfEvents = events.Count;
            int outcomeCount = 0;
            List<ComparableEvent> eventsToCompare = new List<ComparableEvent>(numberOfEvents);
            List<int> indexedContext = new List<int>();
            for (int eventIndex = 0; eventIndex < numberOfEvents; eventIndex++)
            {
                Event ev = events.First.Value;
                events.RemoveFirst();
                string[] eventContext = ev.Context;
                int outcomeId;
                string outcome = ev.Outcome;
                if (outcomeMap.ContainsKey(outcome))
                {
                    outcomeId = outcomeMap[outcome];
                }
                else
                {
                    outcomeId = outcomeCount++;
                    outcomeMap[outcome] = outcomeId;
                }
                UpdateIndexedContextList(indexedContext, predicateIndex, eventContext);
                ProcessIndexedContextList(indexedContext, eventsToCompare, eventContext, outcomeId, outcome);
                indexedContext.Clear();
            }
            OutcomeLabels = ToIndexedStringArray(outcomeMap);
            PredicateLabels = ToIndexedStringArray(predicateIndex);
            return eventsToCompare;
        }

        #endregion

        #region Private Methods

        private static void ProcessIndexedContextList(List<int> indexedContext, List<ComparableEvent> eventsToCompare, string[] eventContext, int outcomeId, string outcome)
        {
            if (indexedContext.Count > 0)
            {
                int[] cons = new int[indexedContext.Count];
                indexedContext.CopyTo(cons);
                eventsToCompare.Add(new ComparableEvent(outcomeId, cons));
            }
            else
            {
                Console.WriteLine("Dropped event {0}: {1}", outcome, String.Join(",", eventContext));
            }
        }

        private static void UpdateIndexedContextList(List<int> indexedContext, Dictionary<string, int> predicateIndex, string[] eventContext)
        {
            for (int i = 0; i < eventContext.Length; i++)
            {
                string context = eventContext[i];
                if (predicateIndex.ContainsKey(context))
                {
                    indexedContext.Add(predicateIndex[context]);
                }
            }
        }

        /// <summary>
        /// Reads the events from <paramref name="eventStream"/> into a <see cref="Collections.Generic.LinkedList"/>.
        /// The predicates associated with each event are counted and any which occur at least <paramref name="cutoff"/>
        /// times are added to the <paramref name="predicateIndex"/> dictionary along
        /// with an unique integer index.
        /// </summary>
        /// <param name="eventStream">The event stream.</param>
        /// <param name="predicateIndex">Index of the predicate.</param>
        /// <param name="cutoff">The cutoff.</param>
        /// <returns>A <see cref="Collections.Generic.LinkedList"/> of events.</returns>
        private LinkedList<Event> ComputeEventCounts(IEventStream eventStream, Dictionary<string, int> predicateIndex, int cutoff)
        {
            LinkedList<Event> events = new LinkedList<Event>();
            HashSet<string> predicatesSet = new HashSet<string>();
            IDictionary<string, int> counter = new Dictionary<string, int>();
            while (eventStream.HasNext())
            {
                Event next = eventStream.Next();
                events.AddLast(next);
                Update(next.Context, predicatesSet, counter, cutoff);
            }
            PredicateCounts = new int[predicatesSet.Count];
            int index = 0;
            HashSet<string>.Enumerator enumerator = predicatesSet.GetEnumerator();
            while (enumerator.MoveNext())
            {
                string predicate = enumerator.Current;
                PredicateCounts[index] = counter[predicate];
                predicateIndex[predicate] = index;
            }
            return events;
        }

        #endregion
    }
}
