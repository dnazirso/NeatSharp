using System;

namespace DataStructures.Calculation.ActivationStrategy
{
    public class Elu : IActivationFunction
    {
        public double Activate(double z) => z > 0 ? z : Math.Exp(z) - 1;
    }
}
