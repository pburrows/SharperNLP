using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharperNLP.MaxEntropy.Model
{
    /// <summary>
    /// This class is a hash table which maps entries of an array to their index in the array.
    /// All entries in the array must be unique.
    /// The entry objects must implement <see cref="Object.Equals(Object)"/> and <see cref="Object.GetHashCode"/>;
    /// otherwise the behavior of this class is undefined.
    /// </summary>
    /// <typeparam name="T">The type of the objects in the array.</typeparam>
    public class IndexHashTable<T> where T : class
    {
        #region Fields

        private readonly T[] _keys;
        private readonly int[] _values;
        private readonly int _size;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexHashTable&lt;T&gt;"/> class.
        /// The <paramref name="mapping"/> array is copied into the table and later changes to the array do not affect this table.
        /// </summary>
        /// <param name="mapping">The values to be indexed. Values must be unique</param>
        /// <param name="loadFactor">The load factor. Usually it's value is 0.7.</param>
        /// <exception cref="System.ArgumentException">
        /// <para>The <paramref name="mapping"/> array has duplicates,</para> or
        /// <para>the <paramref name="loadFactor"/> is not in the range (0,1] (zero exclusive, 1 inclusive).</para>
        /// </exception>
        public IndexHashTable(T[] mapping, double loadFactor)
        {
            if (loadFactor <= 0 || loadFactor > 1)
            {
                throw new ArgumentException("Value must be greater than zero and less or equal to 1.", "loadFactor");
            }
            int arraySize = (int)(mapping.Length / loadFactor) + 1;
            _keys = new T[arraySize];
            _values = new int[arraySize];
            _size = mapping.Length;
            for (int i = 0; i < mapping.Length; i++)
            {
                int startIndex = GetIndexForHash(mapping[i].GetHashCode(), _keys.Length);
                int index = SearchKey(startIndex, null, true);
                if (index == -1)
                {
                    throw new ArgumentException("Array must contain unique keys.", "mapping");
                }
                _keys[index] = mapping[i];
                _values[index] = i;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of elements in the map.
        /// </summary>
        public int Size
        {
            get
            {
                return _size;
            }
        }

        #endregion

        #region Indexers

        /// <summary>
        /// Retrieves the index for the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The index of the key or -1 if there is no entry to the key.</returns>
        public int this[T key]
        {
            get
            {
                int startIndex = GetIndexForHash(key.GetHashCode(), _keys.Length);
                int index = SearchKey(startIndex, key, false);
                if (index != -1)
                {
                    return _values[index];
                }
                return -1;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Converts the map to an array.
        /// </summary>
        /// <returns>An array of items.</returns>
        public T[] ToArray()
        {
            T[] array = new T[_keys.Length];
            for (int i = 0; i < _keys.Length; i++)
            {
                if (_keys[i] != null)
                {
                    array[_values[i]] = _keys[i];
                }
            }
            return array;
        }

        #endregion

        #region Private Methods

        private static int GetIndexForHash(int hashCode, int length)
        {
            return (hashCode & 0x7fffffff) % length;
        }

        private int SearchKey(int startIndex, T key, bool insert)
        {
            for (int i = startIndex; true; i = (i + 1) % _keys.Length)
            {
                // The keys array contains at least one null element, which guarantees
                // termination of the loop 
                if (_keys[i] == null)
                {
                    if (insert)
                        return i;
                    else
                        return -1;
                }
                if (_keys[i].Equals(key))
                {
                    if (!insert)
                        return i;
                    else
                        return -1;
                }
            }
        }

        #endregion
    }
}
