using System;

namespace DataStructures.Calculation.ActivationStrategy
{
    /// <summary>
    /// ELu : Exponential linear unit, f(a,x) = {a (exp(x-1)) | x<=0 , x | x > 0}
    /// </summary>
    public class Elu : IActivationFunction
    {
        ///<inheritdoc/>
        public double Activate(double z) => z > 0 ? z : Math.Exp(z) - 1;
    }
}
