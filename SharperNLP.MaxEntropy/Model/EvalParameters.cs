using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharperNLP.MaxEntropy.Model
{
    /// <summary>
    /// Encapsulates the variables using in producing probabilities from a model and facilitates passing those variables to the 
    /// evaluation method.
    /// </summary>
    public class EvalParameters
    {
        #region Fields

        private readonly int _numberOfOutcomes;
        private readonly double _correctionConstantInverse;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="EvalParameters"></see> class.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="numberOfOutcomes">The number of outcomes.</param>
        public EvalParameters(Context[] parameters, int numberOfOutcomes)
            : this(parameters, 0d, 0d, numberOfOutcomes)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EvalParameters"/> class.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="correctionParameter">The correction parameter.</param>
        /// <param name="correctionConstant">The correction constant.</param>
        /// <param name="numberOfOutcomes">The number of outcomes.</param>
        public EvalParameters(Context[] parameters, double correctionParameter, double correctionConstant, int numberOfOutcomes)
        {
            Parameters = parameters;
            CorrectionParameter = correctionParameter;
            _numberOfOutcomes = numberOfOutcomes;
            CorrectionConstant = correctionConstant;
            if (correctionConstant != 0)
            {
                _correctionConstantInverse = 1.0 / correctionConstant;
            }
            else
            {
                _correctionConstantInverse = 0;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the correction parameter of the model.
        /// </summary>
        /// <value>
        /// The correction parameter.
        /// </value>
        public double CorrectionParameter { get; set; }

        /// <summary>
        /// Gets the mapping between outcomes and parameter values for each context.
        /// </summary>
        public Context[] Parameters { get; private set; }

        /// <summary>
        /// Gets the number of outcomes being predicted.
        /// </summary>
        public int NumberOfOutcomes
        {
            get
            {
                return _numberOfOutcomes;
            }
        }

        /// <summary>
        /// Gets the maximum number of features fired in an event.
        /// Usually referred to as C. This is used to normalize the number of features which occur in an event.
        /// </summary>
        public double CorrectionConstant { get; private set; }


        /// <summary>
        /// Gets the correction constant inverse.
        /// </summary>
        public double CorrectionConstantInverse
        {
            get
            {
                return _correctionConstantInverse;
            }
        }

        #endregion
    }
}
