using DataStructures.Calculation.ActivationStrategy;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DataStructures.Calculation
{
    /// <summary>
    /// Represent a neuron within a neural network
    /// </summary>
    public class Node : IComparable<Node>
    {
        /// <summary>
        /// X coordinate
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Computed output
        /// </summary>
        public double Output { get; set; }

        /// <summary>
        /// List of connection entering this <see cref="Node"/>
        /// </summary>
        public List<Connection> Connections { get; set; } = new List<Connection>();

        /// <summary>
        /// Affected activation function
        /// </summary>
        public IActivationFunction Activation { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="X">X coordinate</param>
        /// <param name="Activation">Activation function</param>
        public Node(double X, IActivationFunction Activation)
        {
            this.X = X;
            this.Activation = Activation;
        }

        ///<inheritdoc/>
        public int CompareTo([AllowNull] Node other)
        {
            if (X > other.X) return -1;
            if (X < other.X) return 1;
            return 0;
        }

        /// <summary>
        /// Compute the activation of a node
        /// </summary>
        public void Calculate()
        {
            double z = 0;

            foreach (Connection c in Connections)
            {
                if (c.Enabled)
                {
                    z += c.Weight * c.In.Output;
                }
            }

            Output = Activation.Activate(z);
        }
    }
}
