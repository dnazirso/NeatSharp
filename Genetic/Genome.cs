﻿using DataStructures;
using DataStructures.Calculation.ActivationStrategy;
using DataStructures.GeneticAggregate;
using DataStructures.NeuroEvolutionAggregate;

namespace Genetic
{
    /// <summary>
    /// Keeps genetic informations
    /// </summary>
    public class Genome : IGenome
    {
        /// <summary>
        /// <see cref="ConnectionGene"/>s of this <see cref="Genome"/>
        /// </summary>
        public RandomList<ConnectionGene> Connections { get; }

        /// <summary>
        /// <see cref="NodeGene"/>s of this <see cref="Genome"/>
        /// </summary>
        public RandomList<NodeGene> Nodes { get; }

        /// <summary>
        /// Aknowledge and manage existing <see cref="Gene"/>s through all species
        /// </summary>
        public INeat Neat { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Neat"><see cref="IGenome"/></param>
        public Genome(INeat Neat)
        {
            Connections = new RandomList<ConnectionGene>();
            Nodes = new RandomList<NodeGene>();

            this.Neat = Neat;
        }

        /// <summary>
        /// Manage mutation regarding their probabilities
        /// </summary>
        public void Mutate()
        {
            if (Constants.PROBABILITY_MUTATE_LINK > ThreadSafeRandom.Random()) MutateLink();
            if (Constants.PROBABILITY_MUTATE_NODE > ThreadSafeRandom.Random()) MutateNode();
            if (Constants.PROBABILITY_MUTATE_WEIGHT_RANDOM > ThreadSafeRandom.Random()) MutateWeightRandom();
            if (Constants.PROBABILITY_MUTATE_WEIGHT_SHIFT > ThreadSafeRandom.Random()) MutateWeightShift();
            if (Constants.PROBABILITY_MUTATE_TOGGLE_LINK > ThreadSafeRandom.Random()) MutateToggleLink();
            if (Constants.PROBABILITY_MUTATE_ACTIVATION_RANDOM > ThreadSafeRandom.Random()) MutateActivationRandom();
        }

        /// <summary>
        /// Add a <see cref="ConnectionGene"/> between two existing <see cref="NodeGene"/>s
        /// </summary>
        public void MutateLink()
        {
            for (int i = 0; i < 100; i++)
            {
                NodeGene a = Nodes.RandomElement();
                NodeGene b = Nodes.RandomElement();

                if (a == null || b == null) continue;
                if (a.X.Equals(b.X)) continue;

                ConnectionGene connection;
                if (a.X < b.X)
                {
                    connection = new ConnectionGene(a, b);
                }
                else
                {
                    connection = new ConnectionGene(b, a);
                }

                if (Connections.Contains(connection)) continue;

                connection = Neat.GetConnection(connection.In, connection.Out);
                connection.Weight += ThreadSafeRandom.NormalRand(0, 0.2f) * Constants.WEIGHT_SHIFT_STRENGTH;

                AddSorted(connection);

                return;
            }
        }

        /// <summary>
        /// Add an sort Genes per innovation number
        /// </summary>
        /// <param name="gene">a <see cref="ConnectionGene"/></param>
        private void AddSorted(ConnectionGene gene)
        {
            for (int i = 0; i < Connections.Count; i++)
            {
                int innovationNb = Connections[i].InnovationNumber;
                if (gene.InnovationNumber < innovationNb)
                {
                    Connections.Insert(i, gene);
                    return;
                }
            }

            Connections.Add(gene);
        }

        /// <summary>
        /// Add a <see cref="NodeGene"/> on an existing <see cref="ConnectionGene"/>
        /// </summary>
        public void MutateNode()
        {
            ConnectionGene connection = Connections.RandomElement();
            if (connection == null) return;

            NodeGene from = connection.In;
            NodeGene to = connection.Out;

            int replaceIndex = Neat.GetReplaceIndex(from, to);

            NodeGene middle;
            if (replaceIndex == 0)
            {
                ActivationEnumeration a = ActivationEnumeration.Random();
                middle = Neat.CreateNode();
                middle.X = (from.X + to.X) / 2;
                middle.Y = ((from.Y + to.Y) / 2) + (ThreadSafeRandom.NormalRand(0, 0.02f) / 2);
                middle.Activation = a.Activation;
                middle.ActivationName = a.Name;
                Neat.SetReplaceIndex(from, to, middle.InnovationNumber);
            }
            else
            {
                middle = Neat.GetNode(replaceIndex);
            }

            ConnectionGene connection1 = Neat.GetConnection(from, middle);
            ConnectionGene connection2 = Neat.GetConnection(middle, to);

            connection1.Weight = 1;
            connection2.Weight = connection.Weight;
            connection2.Enabled = connection.Enabled;

            connection.Enabled = false;
            Connections.Add(connection1);
            Connections.Add(connection2);

            Nodes.Add(middle);
        }

        /// <summary>
        /// Randomly shift the weight of a random <see cref="ConnectionGene"/>
        /// </summary>
        public void MutateWeightShift()
        {
            ConnectionGene connection = Connections.RandomElement();
            if (connection != null)
            {
                connection.Weight += ThreadSafeRandom.NormalRand(0, 0.2f) * Constants.WEIGHT_SHIFT_STRENGTH;
            }
        }

        /// <summary>
        /// Randomly Change the weight of a random <see cref="ConnectionGene"/>
        /// </summary>
        public void MutateWeightRandom()
        {
            ConnectionGene connection = Connections.RandomElement();
            if (connection != null)
            {
                connection.Weight = ThreadSafeRandom.NormalRand(0, 0.2f) * Constants.WEIGHT_RANDOM_STRENGTH;
            }
        }

        /// <summary>
        /// Activate or deactivate a random <see cref="ConnectionGene"/>
        /// </summary>
        public void MutateToggleLink()
        {
            ConnectionGene connection = Connections.RandomElement();
            if (connection != null)
            {
                connection.Enabled = !connection.Enabled;
            }
        }

        /// <summary>
        /// Change the activation function of a random <see cref="NodeGene"/>
        /// </summary>
        public void MutateActivationRandom()
        {
            NodeGene node = Nodes.RandomElement();
            if (node?.X > 0.1)
            {
                ActivationEnumeration a = ActivationEnumeration.Random();
                node.Activation = a.Activation;
                node.ActivationName = a.Name;
            }
        }
    }
}
