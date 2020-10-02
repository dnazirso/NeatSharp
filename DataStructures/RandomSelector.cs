using System.Collections.Generic;

namespace DataStructures
{
    /// <summary>
    /// Represents a random selector for a list of elements
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RandomSelector<T>
    {
        private readonly List<T> elements = new List<T>();
        private readonly List<double> scores = new List<double>();

        private double totalScore = 0;

        /// <summary>
        /// Add an element with its score
        /// </summary>
        /// <param name="element"></param>
        /// <param name="score"></param>
        public void Add(T element, double score)
        {
            elements.Add(element);
            scores.Add(score);
            totalScore += score;
        }

        /// <summary>
        /// Select a random element
        /// </summary>
        /// <returns></returns>
        public T Random()
        {
            double v = ThreadSafeRandom.Random() * totalScore;

            double c = 0;

            for (int i = 0; i < elements.Count; i++)
            {
                c += scores[i];
                if (c >= v)
                {
                    return elements[i];
                }
            }

            if (elements.Count == 1)
            {
                return elements[0];
            }

            return default;
        }
    }
}
