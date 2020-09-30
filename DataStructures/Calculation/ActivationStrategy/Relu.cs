namespace DataStructures.Calculation.ActivationStrategy
{
    public class Relu : IActivationFunction
    {
        public double Activate(double z) => z > 0 ? z : 0;
    }
}
