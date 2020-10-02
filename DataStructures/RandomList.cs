using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures
{
    /// <summary>
    /// Represents the list that ensure the uniqueness of an element
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RandomList<T> : IEnumerable<T>
    {
        /// <summary>
        /// Stores the elements
        /// </summary>
        private List<T> Data { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public RandomList()
        {
            Data = new List<T>();
        }

        /// <summary>
        /// Check if an element already exists
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>a boolean that confirm or not an element pre-exists</returns>
        public bool Contains(T obj) => Data.Contains(obj);

        /// <summary>
        /// Select a random element
        /// </summary>
        /// <returns>An element</returns>
        public T RandomElement()
        {
            if (Data.Any())
            {
                int i = (int)(ThreadSafeRandom.Random() * Count);
                return Data[i];
            }

            return default;
        }

        /// <summary>
        /// Add an element
        /// </summary>
        /// <param name="obj">An element</param>
        public void Add(T obj)
        {
            if (!Data.Contains(obj))
            {
                Data.Add(obj);
            }
        }

        /// <summary>
        /// Inserts an element into the System.Collections.Generic.List`1 at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        /// <param name="item">The object to insert. The value can be null for reference types.</param>
        public void Insert(int index, T item)
        {
            Data.Insert(index, item);
        }

        /// <summary>
        /// Remove all elements from the list
        /// </summary>
        public void Clear()
        {
            Data.Clear();
        }
        
        /// <summary>
        /// Remove an element at a given index
        /// </summary>
        /// <param name="index">index of the element to remove</param>
        public void Remove(int index)
        {
            if (index < 0 || index >= Count) return;
            Data.Remove(Data[index]);
        }

        /// <summary>
        /// Remove a given element
        /// </summary>
        /// <param name="obj">an element</param>
        /// <returns></returns>
        public bool Remove(T obj) => Data.Remove(obj);

        ///<inheritdoc/>
        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)Data).GetEnumerator();
        }

        ///<inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Data).GetEnumerator();
        }

        /// <summary>
        /// Sort the list on a given criterion
        /// </summary>
        /// <param name="comparison"></param>
        public void Sort(Comparison<T> comparison)
        {
            Data.Sort(comparison);
        }

        /// <summary>
        /// Number of element in the list
        /// </summary>
        public int Count => Data.Count;

        /// <summary>
        /// List index crawler
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count) return default;
                return Data[index];
            }
        }
    }
}
