﻿using DataStructures;
using DataStructures.Calculation.ActivationStrategy;
using DataStructures.GeneticAggregate;
using DataStructures.NeuroEvolutionAggregate;
using Genetic;
using System.Collections.Generic;
using System.Diagnostics;

namespace NeuroEvolution
{
    /// <summary>
    /// NeuroEvolution of augmenting topologies
    /// </summary>
    public class Neat : INeat
    {
        /// <summary>
        /// All created <see cref="ConnectionGene"/>s by the deferent <see cref="Genome"/>s
        /// </summary>
        private Dictionary<ConnectionGene, ConnectionGene> AllConnections { get; set; }

        /// <summary>
        /// All created nodes by the deferent genome
        /// </summary>
        private RandomList<NodeGene> AllNodes { get; set; }

        /// <summary>
        /// All existing <see cref="Client"/>s
        /// </summary>
        private RandomList<Client> AllClients { get; set; }

        /// <summary>
        /// Every different <see cref="Species"/>
        /// </summary>
        private RandomList<Species> AllSpecies { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Neat()
        {
            AllConnections = new Dictionary<ConnectionGene, ConnectionGene>();
            AllNodes = new RandomList<NodeGene>();
            AllClients = new RandomList<Client>();
            AllSpecies = new RandomList<Species>();

            Reset();
        }

        ///<inheritdoc/>
        public IGenome EmptyGenome()
        {
            IGenome g = new Genome(this);

            for (int i = 0; i < Constants.InputSize + Constants.OutputSize; i++)
            {
                g.Nodes.Add(GetNode(i + 1));
            }

            return g;
        }

        /// <summary>
        /// Create a fresh start for an <see cref="Neat"/> object
        /// </summary>
        private void Reset()
        {
            AllConnections.Clear();
            AllNodes.Clear();
            AllClients.Clear();

            for (int i = 0; i < Constants.InputSize; i++)
            {
                NodeGene node = CreateNode();
                node.X = 0.1; ;
                node.Y = (i + 1) / (double)(Constants.InputSize + 1);
            }

            for (int i = 0; i < Constants.OutputSize; i++)
            {
                NodeGene node = CreateNode();
                node.X = 0.9; ;
                node.Y = (i + 1) / (double)(Constants.OutputSize + 1);

                ActivationEnumeration a = ActivationEnumeration.Random();
                node.Activation = a.Activation;
                node.ActivationName = a.Name;
            }

            for (int i = 0; i < Constants.MaxClients; i++)
            {
                Client c = new Client(EmptyGenome());
                AllClients.Add(c);
            }
        }

        ///<inheritdoc/>
        public ConnectionGene GetConnection(NodeGene In, NodeGene Out)
        {
            ConnectionGene connection = new ConnectionGene(In, Out);

            if (AllConnections.ContainsKey(connection))
            {
                connection.InnovationNumber = AllConnections[connection].InnovationNumber;
            }
            else
            {
                connection.InnovationNumber = AllConnections.Count + 1;
                AllConnections.Add(connection, connection);
            }

            return connection;
        }

        ///<inheritdoc/>
        public void SetReplaceIndex(NodeGene node1, NodeGene node2, int index)
        {
            AllConnections[new ConnectionGene(node1, node2)].ReplaceIndex = index;
        }

        ///<inheritdoc/>
        public int GetReplaceIndex(NodeGene node1, NodeGene node2)
        {
            ConnectionGene connection = new ConnectionGene(node1, node2);
            ConnectionGene data = AllConnections[connection];
            if (data == null) return 0;
            return data.ReplaceIndex;
        }

        ///<inheritdoc/>
        public NodeGene CreateNode()
        {
            NodeGene node = new NodeGene(AllNodes.Count + 1);
            AllNodes.Add(node);
            return node;
        }

        ///<inheritdoc/>
        public NodeGene GetNode(int id)
        {
            if (id <= AllNodes.Count)
            {
                return AllNodes[id - 1];
            }

            return CreateNode();
        }

        /// <summary>
        /// Evolution procedure
        /// </summary>
        public void Evolve()
        {
            GenerateSpecies();
            Kill();
            RemoveExtinguishedSpecies();
            Reproduce();
            Mutate();
            foreach (Client client in AllClients)
            {
                client.RegenerateCalculator();
            }
        }

        /// <summary>
        /// Generate all <see cref="Species"/> and dispatch within the <see cref="Client"/>s
        /// </summary>
        private void GenerateSpecies()
        {
            foreach (Species s in AllSpecies)
            {
                s.Reset();
            }

            foreach (Client c in AllClients)
            {
                if (c.Species != null) continue;

                bool hasFound = false;
                foreach (Species s in AllSpecies)
                {
                    if (hasFound = s.Put(c))
                    {
                        break;
                    }
                }
                if (!hasFound) AllSpecies.Add(new Species(c));
            }

            foreach (Species s in AllSpecies)
            {
                s.EvaluateScore();
            }
        }

        /// <summary>
        /// Kill <see cref="Species"/> that does not fit (<see cref="Species"/> with bad score first)
        /// </summary>
        public void Kill()
        {
            foreach (Species s in AllSpecies)
            {
                s.Kill(1 - Constants.SURVIVAL_RATE);
            }
        }

        /// <summary>
        /// Remove <see cref="Species"/> that are not represented by enough <see cref="Client"/>s
        /// </summary>
        public void RemoveExtinguishedSpecies()
        {
            for (int i = AllSpecies.Count - 1; i >= 0; i--)
            {
                if (AllSpecies[i].Count <= 1)
                {
                    AllSpecies[i].Extinguish();
                    AllSpecies.Remove(i);
                }
            }
        }

        /// <summary>
        /// Reproduction process
        /// </summary>
        private void Reproduce()
        {
            RandomSelector<Species> selector = new RandomSelector<Species>();
            foreach (Species s in AllSpecies)
            {
                selector.Add(s, s.Score);
            }

            foreach (Client c in AllClients)
            {
                if (c.Species == null)
                {
                    Species s = selector.Random();
                    c.Genome = s.Breed();
                    s.ForcePut(c);
                }
            }
        }

        /// <summary>
        /// Global mutation process
        /// </summary>
        private void Mutate()
        {
            foreach (Client c in AllClients)
            {
                c.Mutate();
            }
        }

        /// <summary>
        /// TODO : move this to unit tests
        /// </summary>
        /// <returns></returns>
        public RandomList<Client> CheckEvolutionProcess()
        {
            Neat neat = new Neat();

            double[] inputs = new double[Constants.InputSize];
            for (int i = 0; i < Constants.InputSize; i++) inputs[i] = ThreadSafeRandom.NormalRand();

            for (int i = 0; i < 100; i++)
            {
                foreach (Client c in neat.AllClients)
                {
                    c.Score = c.Calculate(inputs)[0];
                }
                neat.Evolve();
                //neat.TraceClients();
                neat.TraceSpecies();
            }

            return neat.AllClients;
        }

        /// <summary>
        /// Trace <see cref="Species"/> for debug
        /// </summary>
        public void TraceSpecies()
        {
            Trace.WriteLine("-----------------------------------------");
            foreach (Species s in AllSpecies)
            {
                Trace.WriteLine($"{s.GetHashCode()} {s.Score} {s.Count}");
            }
        }

        /// <summary>
        /// Trace <see cref="Client"/> for debug
        /// </summary>
        public void TraceClients()
        {
            Trace.WriteLine("-----------------------------------------");
            foreach (Client c in AllClients)
            {
                foreach (ConnectionGene g in c.Genome.Connections)
                {
                    Trace.Write($"{g.InnovationNumber} ");
                }
                Trace.WriteLine("");
            }
        }
    }
}
