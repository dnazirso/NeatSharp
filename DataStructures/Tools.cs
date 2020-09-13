using System;
using System.Collections.Generic;
using System.Linq;
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
        public static Random ThisThreadsRandom
        {
            get
            {
                return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId)));
            }
        }

        /// <summary>
        /// Creates a Random float under the normal distribution probability law (Laplace–Gauss)
        /// </summary>
        /// <param name="mean">mean of the Gaussian curve (generaly 0)</param>
        /// <param name="scale">scale parameter of the normal distribution (generaly x²=1)</param>
        /// <returns>a normal random float</returns>
        public static float NormalRand(float mean = 0, float scale = 1)
        {
            float random1 = 1.0f - (float)ThisThreadsRandom.NextDouble();
            float random2 = 1.0f - (float)ThisThreadsRandom.NextDouble();

            return mean + scale * MathF.Sqrt(-2.0f * MathF.Log(random1)) * MathF.Sin(2.0f * MathF.PI * random2);
        }

        public static double Random()
        {
            return ThisThreadsRandom.NextDouble();
        }
    }

    /// <summary>
    /// Method extensions
    /// </summary>
    public static class MyExtensions
    {
        /// <summary>
        /// Shuffle the items of a list
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="list">list</param>
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        /// <summary>
        /// Reorganise a list by chunks of smaller lists
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="source">Source list</param>
        /// <param name="chunkSize">Chunk size</param>
        /// <returns></returns>
        public static List<List<T>> ChunkBy<T>(this IList<T> source, int chunkSize)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }
    }
}
