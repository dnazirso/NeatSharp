namespace DataStructures.Calculation.ActivationStrategy
{
    /// <summary>
    /// ReLU : Rectified linear unit, f(a,x) = {0 | x<=0 , x | x > 0} = max{0 , x}
    /// </summary>
    public class Relu : IActivationFunction
    {
        ///<inheritdoc/>
        public double Activate(double z) => z > 0 ? z : 0;
    }
}
