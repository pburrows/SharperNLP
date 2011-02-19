using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharperNLP.MaxEntropy.Model
{
    public abstract class AbstractModel : IMaxentModel
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractModel"/> class.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="predicatesMap">The predicates map.</param>
        /// <param name="outcomeNames">The outcome names.</param>
        public AbstractModel(Context[] parameters, IndexHashTable<string> predicatesMap, string[] outcomeNames)
        {
            PredicatesMap = predicatesMap;
            OutcomeNames = outcomeNames;
            EvalParameters = new EvalParameters(parameters, outcomeNames.Length);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractModel"></see> class.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="predicateLabels">The predicate labels.</param>
        /// <param name="outcomeNames">The outcome names.</param>
        public AbstractModel(Context[] parameters, string[] predicateLabels, string[] outcomeNames)
            : this(parameters, predicateLabels, outcomeNames, 0, 0d)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractModel"></see> class.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="predicateLabels">The predicate labels.</param>
        /// <param name="outcomeNames">The outcome names.</param>
        /// <param name="correctionConstant">The correction constant.</param>
        /// <param name="correctionParameter">The correction parameter.</param>
        public AbstractModel(Context[] parameters, string[] predicateLabels, string[] outcomeNames, int correctionConstant, double correctionParameter)
        {
            InitializeComponents(predicateLabels, outcomeNames);
            EvalParameters = new EvalParameters(parameters, correctionParameter, correctionConstant, outcomeNames.Length);
        }

        #endregion

        #region Inner Constructs

        public enum ModelType
        {
            MaxEntropy,
            Perceptron
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the data structures relevant to storing the model.
        /// </summary>
        public object[] DataStructures
        {
            get
            {
                return new object[]
                {
                    EvalParameters.Parameters,
                    PredicatesMap,
                    OutcomeNames,
                    Convert.ToInt32( EvalParameters.CorrectionConstant),
                    EvalParameters.CorrectionParameter
                };
            }
        }

        /// <summary>
        /// Gets the get number of outcomes. for this model.
        /// </summary>
        public int NumberOfOutcomes
        {
            get { return EvalParameters.NumberOfOutcomes; }
        }

        /// <summary>
        /// Gets or sets the mapping between predicates/contexts and an integer representing them.
        /// </summary>
        /// <value>
        /// The predicates map.
        /// </value>
        protected IndexHashTable<string> PredicatesMap { get; set; }

        /// <summary>
        /// Gets or sets the names of the outcome.
        /// </summary>
        /// <value>
        /// The outcome names.
        /// </value>
        protected string[] OutcomeNames { get; set; }

        /// <summary>
        /// Gets or sets the model parameters.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        protected EvalParameters EvalParameters { get; set; }

        /// <summary>
        /// Gets or sets the prior distribution.
        /// </summary>
        /// <value>
        /// The prior distribution.
        /// </value>
        protected IPrior PriorDistribution { get; set; }

        /// <summary>
        /// Gets or sets the type of the model.
        /// </summary>
        /// <value>
        /// The type of the model.
        /// </value>
        protected ModelType Model { get; set; }

        #endregion

        #region Public Methods

        public abstract double[] Evaluate(string[] context);

        public abstract double[] Evaluate(string[] context, double[] probabilities);

        public abstract double[] Evaluate(string[] context, float[] values);

        /// <summary>
        /// Gets the outcome associated with the index containing the highest probability in the
        /// <paramref name="outcomes"/> array.
        /// </summary>
        /// <param name="outcomes">An array of <see cref="System.Double"/> as returned
        /// by the <see cref="Evaluate(string[] context)"/> method.</param>
        /// <returns>
        /// The name of the best outcome.
        /// </returns>
        public string GetBestOutcome(double[] outcomes)
        {
            int index = 0;
            for (int i = 1; i < outcomes.Length; i++)
            {
                if (outcomes[i] > outcomes[index])
                {
                    index = i;
                }
            }
            return OutcomeNames[index];
        }

        /// <summary>
        /// Returns a string matching all outcome names with all the probabilities as produced by the
        /// <see cref="Evaluate(string[] context)"/> method.
        /// </summary>
        /// <param name="outcomes">An array of <see cref="System.Double"/> as returned
        /// by the <see cref="Evaluate(string[] context)"/> method.</param>
        /// <returns>
        /// A string containing outcome names paired with the normalized probability for each one.
        /// </returns>
        public string GetAllOutcomes(double[] outcomes)
        {
            if (outcomes.Length != OutcomeNames.Length)
            {
                throw new ArgumentException("The length of the outcomes must be the same as the length of outcome names", "outcomes");
            }
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < outcomes.Length; i++)
            {
                stringBuilder.AppendFormat("{0}[{1:00.0000}] ", OutcomeNames[i], outcomes[i]);
            }
            return stringBuilder.ToString().Trim();
        }

        /// <summary>
        /// Gets the name of the outcome associated with the index <paramref name="outcomeIndex"/>.
        /// </summary>
        /// <param name="outcomeIndex">Index of the desired outcome.</param>
        /// <returns>
        /// The name of the outcome.
        /// </returns>
        public string GetOutcome(int outcomeIndex)
        {
            return OutcomeNames[outcomeIndex];
        }

        /// <summary>
        /// Gets the index associated with the given outcome.
        /// </summary>
        /// <param name="outcome">The outcome for which the index is desired.</param>
        /// <returns>
        /// The index if the given outcome exists for the model; -1 otherwise.
        /// </returns>
        public int GetIndex(string outcome)
        {
            int index = Array.IndexOf<string>(OutcomeNames, outcome);
            return index >= 0 ? index : -1;
        }

        #endregion

        #region Private Methods

        private void InitializeComponents(string[] predicateLabels, string[] outcomeNames)
        {
            PredicatesMap = new IndexHashTable<string>(predicateLabels, 0.7d);
            OutcomeNames = outcomeNames;
        }

        #endregion
    }
}
