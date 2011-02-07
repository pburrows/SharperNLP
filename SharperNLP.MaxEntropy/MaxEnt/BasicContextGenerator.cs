using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharperNLP.MaxEntropy.MaxEnt
{
    /// <summary>
    /// Generate contexts for maxent decisions, assuming that the input 
    /// given to the <see cref="IContextGenerator.GetContext"/> method is a <see cref="String"/> containing contextual 
    /// predicates separated by spaces.
    /// </summary>
    /// <example>
    /// <para>
    /// pred1 pred2 ... predN
    /// </para>
    /// </example>
    public class BasicContextGenerator : IContextGenerator
    {
        #region Fields

        private readonly string _separator = " ";

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicContextGenerator"/> class.
        /// </summary>
        /// <param name="separator">The separator.</param>
        public BasicContextGenerator(string separator)
        {
            _separator = separator;
        }

        #endregion

        #region IContextGenerator Members

        /// <summary>
        /// Builds up the contextual predicates given an <see cref="Object"/>.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public string[] GetContext(object obj)
        {
            string[] result = null;
            string parameter = obj as string;
            if (parameter != null)
            {
                result = parameter.Split(_separator.ToCharArray());
            }
            return result;
        }

        #endregion
    }
}
