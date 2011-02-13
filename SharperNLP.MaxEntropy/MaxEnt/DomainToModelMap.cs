using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SharperNLP.MaxEntropy.Model;

namespace SharperNLP.MaxEntropy.MaxEnt
{
    /// <summary>
    /// A class which stores mapping from Model objects to MaxEnt objects.
    /// This permits an application to replace an old model for a domain with a newly trained one in a thread-safe manner.
    /// By calling the <see cref="GetModel"/> method, the application can create new instances of classes which use the relevant models.
    /// </summary>
    public class DomainToModelMap
    {
        #region Fields

        private ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        private Dictionary<IModelDomain, MaxentModel> _map = new Dictionary<IModelDomain, MaxentModel>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets the keys of the current map.
        /// </summary>
        public HashSet<IModelDomain> Keys
        {
            get
            {
                _lock.EnterReadLock();
                HashSet<IModelDomain> hashSet = new HashSet<IModelDomain>(_map.Keys);
                _lock.ExitReadLock();
                return hashSet;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the model for the given domain.
        /// </summary>
        /// <param name="domain">The <see cref="MaxEnt.IModelDomain"/> object which keys to the model.</param>
        /// <param name="model">The <see cref="Model.MaxentModel"/> trained for the domain.</param>
        public void SetModelForDomain(IModelDomain domain, MaxentModel model)
        {
            _lock.EnterWriteLock();
            _map.Add(domain, model);
            _lock.ExitWriteLock();
        }

        /// <summary>
        /// Gets the model mapped to the given domain.
        /// </summary>
        /// <param name="domain">The domain object which keys to the desired model..</param>
        /// <returns>The <see cref="Model.MaxentModel"/> corresponding to the given domain.</returns>
        public MaxentModel GetModel(IModelDomain domain)
        {
            MaxentModel result = null;
            _lock.EnterReadLock();
            bool mapContainsKey = _map.ContainsKey(domain);
            if (mapContainsKey)
            {
                result = _map[domain];
            }
            _lock.ExitReadLock();
            if (!mapContainsKey)
            {
                throw new KeyNotFoundException(String.Format("No model has been created for domain: {0}", domain));
            }
            return result;
        }

        /// <summary>
        /// Removes the mapping for this <see cref="MaxEnt.IModelDomain"/> key from the map if present.
        /// </summary>
        /// <param name="domain">The key whose mapping is to be removed from the map.</param>
        public void RemoveDomain(IModelDomain domain)
        {
            _lock.EnterWriteLock();
            _map.Remove(domain);
            _lock.ExitWriteLock();
        }

        #endregion
    }
}
