using DataStructures;
using DataStructures.GeneticAggregate;
using DataStructures.NeuroEvolutionAggregate;
using Genetic;
using System.Collections.Generic;
using System.Diagnostics;

namespace NeuroEvolution
{
    public class Neat : INeat
    {
        private Dictionary<ConnectionGene, ConnectionGene> AllConnections { get; set; }
        private RandomHashSet<NodeGene> AllNodes { get; set; }
        public RandomHashSet<Client> Clients { get; set; }
        public RandomHashSet<Species> AllSpecies { get; set; }

        public Neat()
        {
            AllConnections = new Dictionary<ConnectionGene, ConnectionGene>();
            AllNodes = new RandomHashSet<NodeGene>();
            Clients = new RandomHashSet<Client>();
            AllSpecies = new RandomHashSet<Species>();

            Reset();
        }

        public IGenome EmptyGenome()
        {
            IGenome g = new Genome(this);

            for (int i = 0; i < Constants.InputSize + Constants.OutputSize; i++)
            {
                g.Nodes.Add(GetNode(i));
            }

            return g;
        }

        private void Reset()
        {
            AllConnections.Clear();
            AllNodes.Clear();
            Clients.Clear();

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
            }

            for (int i = 0; i < Constants.MaxClients; i++)
            {
                Client c = new Client
                {
                    Genome = EmptyGenome()
                };
                c.GenerateCalculator();
                Clients.Add(c);
            }
        }

        public Client GetClient(int index) => Clients.Get(index);

        public ConnectionGene GetConnection(NodeGene From, NodeGene To)
        {
            ConnectionGene connection = new ConnectionGene(From, To);

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

        public void SetReplaceIndex(NodeGene node1, NodeGene node2, int index)
        {
            AllConnections[new ConnectionGene(node1, node2)].ReplaceIndex = index;
        }

        public int GetReplaceIndex(NodeGene node1, NodeGene node2)
        {
            ConnectionGene connection = new ConnectionGene(node1, node2);
            ConnectionGene data = AllConnections[connection];
            if (data == null) return 0;
            return data.ReplaceIndex;
        }

        public NodeGene CreateNode()
        {
            NodeGene node = new NodeGene(AllNodes.Size() + 1);
            AllNodes.Add(node);
            return node;
        }

        public NodeGene GetNode(int id)
        {
            if (id + 1 <= AllNodes.Size())
            {
                return AllNodes.Get(id);
            }

            return CreateNode();
        }

        public void Evolve()
        {
            GenerateSpecies();
            Kill();
            RemoveExtinguishedSpecies();
            TraceSpecies();
            Reproduce();
            Mutate();
            foreach (Client c in Clients.Data)
            {
                c.GenerateCalculator();
            }
        }

        private void GenerateSpecies()
        {
            foreach (Species s in AllSpecies.Data)
            {
                s.Reset();
            }

            foreach (Client c in Clients.Data)
            {
                if (c.Species != null) continue;

                bool hasFound = false;
                foreach (Species s in AllSpecies.Data)
                {
                    if (s.Put(c))
                    {
                        hasFound = true;
                        break;
                    }
                }
                if (!hasFound) AllSpecies.Add(new Species(c));
            }

            foreach (Species s in AllSpecies.Data)
            {
                s.EvaluateScore();
            }
        }

        public void Kill()
        {
            foreach (Species s in AllSpecies.Data)
            {
                s.Kill(1 - Constants.SURVIVAL_RATE);
            }
        }

        public void RemoveExtinguishedSpecies()
        {
            for (int i = AllSpecies.Size() - 1; i >= 0; i--)
            {
                if (AllSpecies.Get(i).Size() <= 1)
                {
                    AllSpecies.Get(i).Extinguish();
                    AllSpecies.Remove(i);
                }
            }
        }

        private void Reproduce()
        {
            RandomSelector<Species> selector = new RandomSelector<Species>();
            foreach (Species s in AllSpecies.Data)
            {
                selector.Add(s, s.Score);
            }

            foreach (Client c in Clients.Data)
            {
                if (c.Species == null)
                {
                    Species s = selector.Random();
                    c.Genome = s.Breed();
                    s.ForcePut(c);
                }
            }
        }

        private void Mutate()
        {
            foreach (Client c in Clients.Data)
            {
                c.Mutate();
            }
        }
        public void TraceSpecies()
        {
            Trace.WriteLine("-----------------------------------------");
            foreach (Species s in AllSpecies.Data)
            {
                Trace.WriteLine($"{s.Representative.Genome.GetHashCode()} {s.Score} {s.Size()}");
            }
        }
    }
}
