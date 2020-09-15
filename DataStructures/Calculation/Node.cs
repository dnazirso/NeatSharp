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

        public Node(double X)
        {
            this.X = X;
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

            Output = ActivationFunction(z);
        }

        private double ActivationFunction(double z) => 1d / (1 + Math.Exp(-z));
    }
}
