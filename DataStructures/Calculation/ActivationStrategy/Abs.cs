using System;

namespace DataStructures.Calculation.ActivationStrategy
{
    public class Abs : IActivationFunction
    {
        public double Activate(double z) => Math.Abs(z);
    }
}
