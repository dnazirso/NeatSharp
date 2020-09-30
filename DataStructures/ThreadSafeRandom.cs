using System;
using System.Threading;

namespace DataStructures
{
    /// <summary>
    /// Thread safe random
    /// </summary>
    public static class ThreadSafeRandom
    {
        /// <summary>
        /// local instance
        /// </summary>
        [ThreadStatic] private static Random Local;

        /// <summary>
        /// thread safe random getter
        /// </summary>
        private static Random ThisThreadsRandom
        {
            get
            {
                return Local ??= new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId));
            }
        }

        /// <summary>
        /// Creates a Random float under the normal distribution probability law (Laplace–Gauss)
        /// </summary>
        /// <param name="mean">mean of the Gaussian curve (generaly 0)</param>
        /// <param name="scale">scale parameter of the normal distribution (generaly x²=1)</param>
        /// <returns>a normal random float</returns>
        public static double NormalRand(double mean = 0, double scale = 1)
        {
            double random1 = 1.0 - ThisThreadsRandom.NextDouble();
            double random2 = 1.0 - ThisThreadsRandom.NextDouble();

            return mean + scale * Math.Sqrt(-2.0f * Math.Log(random1)) * Math.Sin(2.0f * Math.PI * random2);
        }

        /// <summary>
        /// Creates a Random double under an equi-distribution probability as P(x)=1/sum(x), x=[0,1]
        /// </summary>
        /// <returns>a random double between 0 and 1</returns>
        public static double Random()
        {
            return ThisThreadsRandom.NextDouble();
        }
    }
}
