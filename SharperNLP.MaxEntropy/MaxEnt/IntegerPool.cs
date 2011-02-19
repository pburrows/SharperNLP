using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharperNLP.MaxEntropy.MaxEnt
{
    /// <summary>
    /// A pool of read-only unsigned integers within a fixed, non-sparse range.
    /// Use this class for operations in which a large number of integer wrapper objects will be created.
    /// </summary>
    /// <remarks>
    /// This class is heavily redundant because in .NET <see cref="System.Int32"/> is a value type, not a reference type as in Java.
    /// This class is kept only for consistency.
    /// </remarks>
    public class IntegerPool
    {
        #region Fields

        private int[] _table;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="IntegerPool"/> class with size specified by <paramref name="size"/> parameter.
        /// </summary>
        /// <param name="size">The size of the pool.</param>
        public IntegerPool(int size)
        {
            _table = new int[size];
            for (int i = 0; i < _table.Length; i++)
            {
                _table[i] = i;
            }
        }

        #endregion

        #region Indexer

        /// <summary>
        /// Gets the shared <see cref="System.Int32"/> wrapper for <paramref name="value"/> if it's inside the range managed by the pool.
        /// If the <paramref name="value"/> is outside the pool, the value of the parameter is returned.
        /// </summary>
        public int this[int value]
        {
            get
            {
                if (value>=0&&value<_table.Length)
                {
                    return _table[value];
                }
                else
                {
                    return value;
                }
            }
        }

        #endregion
    }
}
