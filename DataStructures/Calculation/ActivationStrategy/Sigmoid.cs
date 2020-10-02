using System;

namespace DataStructures.Calculation.ActivationStrategy
{
    /// <summary>
    /// Sigmoid : f(x) = 1 / (1 + exp(-x))
    /// </summary>
    public class Sigmoid : IActivationFunction
    {
        ///<inheritdoc/>
        public double Activate(double z) => 1d / (1 + Math.Exp(-z));
    }
}
