using System;

namespace DataStructures.Calculation.ActivationStrategy
{
    /// <summary>
    /// Abs : Absolute value, f(x) = |x|
    /// </summary>
    public class Abs : IActivationFunction
    {
        ///<inheritdoc/>
        public double Activate(double z) => Math.Abs(z);
    }
}
