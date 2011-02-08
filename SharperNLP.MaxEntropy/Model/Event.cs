using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharperNLP.MaxEntropy.Model
{
    /// <summary>
    /// The context of a decision point during training. This includes contextual predicates and an outcome. 
    /// </summary>
    public class Event
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="Event"/> class.
        /// </summary>
        /// <param name="outcome">The outcome.</param>
        /// <param name="context">The context.</param>
        public Event(string outcome, string[] context)
            : this(outcome, context, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Event"/> class.
        /// </summary>
        /// <param name="outcome">The outcome.</param>
        /// <param name="context">The context.</param>
        /// <param name="values">The values.</param>
        public Event(string outcome, string[] context, float[] values)
        {
            Outcome = outcome;
            Context = context;
            Values = values;
        }

        #endregion

        #region Properties

        public string Outcome { get; private set; }
        public string[] Context { get; private set; }
        public float[] Values { get; private set; }

        #endregion

        #region Overrides

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Outcome);
            sb.Append("[");
            if (Context != null && Context.Any() && Values != null && Values.Any())
            {
                sb.AppendFormat("{0}={1}", Context[0], Values[0]);
            }
            for (int i = 1; i < Context.Length && i < Values.Length; i++)
            {
                sb.AppendFormat(" {0}={1}", Context[i], Values[i]);
            }
            sb.Append("]");
            return sb.ToString();
        }
        #endregion
    }
}
