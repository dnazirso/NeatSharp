using DataStructures.Calculation.ActivationStrategy;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DataStructures.Calculation
{
    public class Node : IComparable<Node>
    {
        public double X { get; set; }
        public double Output { get; set; }
        public List<Connection> Connections { get; set; } = new List<Connection>();
        public IActivationFunction Activation { get; set; }

        public Node(double X, IActivationFunction Activation)
        {
            this.X = X;
            this.Activation = Activation;
        }

        public int CompareTo([AllowNull] Node other)
        {
            if (X > other.X) return -1;
            if (X < other.X) return 1;
            return 0;
        }

        public void Calculate()
        {
            double z = 0;

            foreach (Connection c in Connections)
            {
                if (c.Enabled)
                {
                    z += c.Weight * c.From.Output;
                }
            }

            Output = Activation.Activate(z);
        }
    }
}
