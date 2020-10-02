using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DataStructures.Calculation.ActivationStrategy
{
    /// <summary>
    /// Activation Enumeration abstraction
    /// </summary>
    public abstract class ActivationBase
    {
        /// <summary>
        /// Activation function
        /// </summary>
        public IActivationFunction Activation { get; set; }

        /// <summary>
        /// Activation function name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Activation"><see cref="Activation"/></param>
        /// <param name="Name"><see cref="Name"/></param>
        public ActivationBase(IActivationFunction Activation, string Name)
        {
            this.Activation = Activation;
            this.Name = Name;
        }

        /// <summary>
        /// Lists the enumerated Activation functions
        /// </summary>
        /// <typeparam name="T">an enumerated activation function</typeparam>
        /// <returns>an enumarated activation function</returns>
        public static IEnumerable<T> GetAll<T>() where T : ActivationBase
        {
            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }
    }
}
