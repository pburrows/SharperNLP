using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharperNLP.MaxEntropy.MaxEnt
{
    /// <summary>
    /// An interface that represents a domain to which a particular maxent model is primarily applicable.
    /// This interface is used by <see cref="DomainToModelMap"/> class to allow an application to grab the models relevant for 
    /// the different domains.
    /// </summary>
    public interface IModelDomain
    {
        /// <summary>
        /// Gets the name of this domain.
        /// </summary>
        string Name { get; }
    }
}
