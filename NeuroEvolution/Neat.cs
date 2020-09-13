using DataStructures;
using DataStructures.GeneticAggregate;
using DataStructures.NeuroEvolutionAggregate;
using Genetic;
using System.Collections.Generic;

namespace NeuroEvolution
{
    public class Neat : INeat
    {
        private Dictionary<ConnectionGene, ConnectionGene> AllConnections { get; set; }
        private RandomHashSet<NodeGene> AllNodes { get; set; }

        public Neat()
        {
            AllConnections = new Dictionary<ConnectionGene, ConnectionGene>();
            AllNodes = new RandomHashSet<NodeGene>();

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
        }

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
    }
}
