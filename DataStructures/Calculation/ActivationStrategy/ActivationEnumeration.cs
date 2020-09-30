using System.Linq;

namespace DataStructures.Calculation.ActivationStrategy
{
    public class ActivationEnumeration : ActivationBase
    {
        public static readonly ActivationEnumeration Abs = new ActivationEnumeration(new Abs(), ActivationName.Abs);
        public static readonly ActivationEnumeration Elu = new ActivationEnumeration(new Elu(), ActivationName.Elu);
        public static readonly ActivationEnumeration Relu = new ActivationEnumeration(new Relu(), ActivationName.Relu);
        public static readonly ActivationEnumeration Sigmoid = new ActivationEnumeration(new Sigmoid(), ActivationName.Sigmoid);

        public ActivationEnumeration(IActivationFunction Activation, string Name) : base(Activation, Name) { }

        public static ActivationEnumeration Random()
        {
            int count = GetAll<ActivationEnumeration>().Count();
            int i = (int)(ThreadSafeRandom.Random() * count);
            return GetAll<ActivationEnumeration>().ToList()[i];
        }
    }
}
