using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DataStructures.Calculation.ActivationStrategy
{
    public abstract class ActivationBase
    {
        public IActivationFunction Activation { get; set; }
        public string Name { get; set; }

        public ActivationBase(IActivationFunction Activation, string Name)
        {
            this.Activation = Activation;
            this.Name = Name;
        }

        public static IEnumerable<T> GetAll<T>() where T : ActivationBase
        {
            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }
    }
}
