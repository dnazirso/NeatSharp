using System.Linq;

namespace DataStructures.Calculation.ActivationStrategy
{
    /// <summary>
    /// Activation function enumeration
    /// </summary>
    public class ActivationEnumeration : ActivationBase
    {
        /// <summary>
        /// Abs : Absolute value, f(x) = |x|
        /// </summary>
        public static readonly ActivationEnumeration Abs = new ActivationEnumeration(new Abs(), ActivationName.Abs);

        /// <summary>
        /// ELu : Exponential linear unit, f(a,x) = {a (exp(x-1)) | x<=0 , x | x > 0}
        /// </summary>
        public static readonly ActivationEnumeration Elu = new ActivationEnumeration(new Elu(), ActivationName.Elu);

        /// <summary>
        /// ReLU : Rectified linear unit, f(a,x) = {0 | x<=0 , x | x > 0} = max{0 , x}
        /// </summary>
        public static readonly ActivationEnumeration Relu = new ActivationEnumeration(new Relu(), ActivationName.Relu);

        /// <summary>
        /// Sigmoid : f(x) = 1 / (1 + exp(-x))
        /// </summary>
        public static readonly ActivationEnumeration Sigmoid = new ActivationEnumeration(new Sigmoid(), ActivationName.Sigmoid);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Activation">Activation function object</param>
        /// <param name="Name">Activation function name</param>
        private ActivationEnumeration(IActivationFunction Activation, string Name) : base(Activation, Name) { }

        /// <summary>
        /// Get a random activation function
        /// </summary>
        /// <returns>a Activation function</returns>
        public static ActivationEnumeration Random()
        {
            int count = GetAll<ActivationEnumeration>().Count();
            int i = (int)(ThreadSafeRandom.Random() * count);
            return GetAll<ActivationEnumeration>().ToList()[i];
        }
    }
}
