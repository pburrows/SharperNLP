using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharperNLP.MaxEntropy.Model
{
    public abstract class AbstractEventStream : IEventStream
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractEventStream"/> class.
        /// </summary>
        public AbstractEventStream()
        {
        }

        #endregion

        #region Public Methods

        public void Remove()
        {
            throw new NotSupportedException();
        }

        #endregion

        #region IEventStream Members
        
        public abstract Event Next();

        public abstract bool HasNext(); 
        
        #endregion
    }
}
