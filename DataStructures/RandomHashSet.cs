using DataStructures.GeneticAggregate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures
{
    public class RandomHashSet<T>
    {
        private HashSet<T> Set { get; set; }
        public List<T> Data { get; }

        public RandomHashSet()
        {
            Set = new HashSet<T>();
            Data = new List<T>();
        }

        public int Size() => Data.Count;

        public bool Contains(T obj) => Set.Contains(obj);

        public T RandomElement()
        {
            if (Set.Any())
            {
                int i = (int)(ThreadSafeRandom.Random() * Size());
                return Data[i];
            }

            return default;
        }

        public void Add(T obj)
        {
            if (!Set.Contains(obj))
            {
                Set.Add(obj);
                Data.Add(obj);
            }
        }

        public void AddSorted(Gene gene)
        {
            T g = (T)Convert.ChangeType(gene, typeof(T));

            for (int i = 0; i < Size(); i++)
            {
                int innovationNb = (Data[i] as Gene).InnovationNumber;
                if (gene.InnovationNumber < innovationNb)
                {
                    Data.Insert(i, g);
                    Set.Add(g);
                    return;
                }
            }

            Data.Add(g);
            Set.Add(g);
        }

        public void Clear()
        {
            Set.Clear();
            Data.Clear();
        }

        public T Get(int index)
        {
            if (index < 0 || index >= Size()) return default;
            return Data[index];
        }

        public void Remove(int index)
        {
            if (index < 0 || index >= Size()) return;
            Set.Remove(Data[index]);
            Data.Remove(Data[index]);
        }

        public void Remove(T obj)
        {
            Set.Remove(obj);
            Data.Remove(obj);
        }
    }
}
