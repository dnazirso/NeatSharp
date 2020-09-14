using System;

namespace DataStructures
{
    public sealed class Constants
    {
        public static readonly int MAX_NODES = (int)Math.Pow(2, 20);
        public static readonly int InputSize = 10;
        public static readonly int OutputSize = 1;
        public static readonly int MaxClients = 1000;

        public static readonly double C1 = 1;
        public static readonly double C2 = 1;
        public static readonly double C3 = 1;
        public static readonly double CP = 4;

        public static readonly double WEIGHT_SHIFT_STRENGTH = 0.3;
        public static readonly double WEIGHT_RANDOM_STRENGTH = 1;

        public static readonly double PROBABILITY_MUTATE_LINK = 0.01;
        public static readonly double PROBABILITY_MUTATE_NODE = 0.03;
        public static readonly double PROBABILITY_MUTATE_WEIGHT_SHIFT = 0.02;
        public static readonly double PROBABILITY_MUTATE_WEIGHT_RANDOM = 0.02;
        public static readonly double PROBABILITY_MUTATE_WEIGHT_TOGGLE_LINK = 0;

        public static readonly double SURVIVAL_RATE = 0.8;
    }
}
