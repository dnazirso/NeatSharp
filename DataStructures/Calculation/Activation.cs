using System;

namespace DataStructures.Calculation
{
    public static class Activation
    {
        public static double Sigmoid(double z) => 1d / (1 + Math.Exp(-z));
    }
}
