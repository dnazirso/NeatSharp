using System.Collections.Generic;

namespace DataStructures
{
    public class RandomSelector<T>
    {
        private readonly List<T> elements = new List<T>();
        private readonly List<double> scores = new List<double>();

        private double totalScore = 0;

        public void Add(T element, double score)
        {
            elements.Add(element);
            scores.Add(score);
            totalScore += score;
        }

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

            return default;
        }

        public void Reset()
        {
            elements.Clear();
            scores.Clear();
            totalScore = 0;
        }
    }
}
