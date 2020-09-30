using System;

namespace DataStructures.Calculation.ActivationStrategy
{
    public class Sigmoid : IActivationFunction
    {
        public double Activate(double z) => 1d / (1 + Math.Exp(-z));
    }
}
