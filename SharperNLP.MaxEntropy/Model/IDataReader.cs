using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharperNLP.MaxEntropy.Model
{
    /// <summary>
    /// Represents a data reader.
    /// </summary>
    public interface IDataReader
    {
        /// <summary>
        /// Reads a double.
        /// </summary>
        /// <returns>The read double value.</returns>
        double ReadDouble();

        /// <summary>
        /// Reads an integer.
        /// </summary>
        /// <returns>The integer value.</returns>
        int ReadInt();

        /// <summary>
        /// Reads a string in the UTF.
        /// </summary>
        /// <returns>The string.</returns>
        string ReadUTF();
    }
}
