using DataStructures.GeneticAggregate;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.Calculation
{
    public class Calculator
    {
        readonly List<Node> InputNodes = new List<Node>();
        readonly List<Node> HiddenNodes = new List<Node>();
        readonly List<Node> OutputNodes = new List<Node>();
        public Calculator(IGenome genome)
        {
            RandomList<NodeGene> nodes = genome.Nodes;
            RandomList<ConnectionGene> connections = genome.Connections;

            Dictionary<int, Node> nodeDictionnary = new Dictionary<int, Node>();

            foreach (NodeGene n in nodes)
            {
                Node node = new Node(n.X, n.Activation);
                nodeDictionnary[n.InnovationNumber] = node;

                if (n.X <= 0.1)
                {
                    InputNodes.Add(node);
                }
                else if (n.X >= 0.9)
                {
                    OutputNodes.Add(node);
                }
                else
                {
                    HiddenNodes.Add(node);
                }
            }

            HiddenNodes.Sort((Node n1, Node n2) => n1.CompareTo(n2));

            foreach (ConnectionGene c in connections)
            {
                Node from = nodeDictionnary[c.In.InnovationNumber];
                Node to = nodeDictionnary[c.Out.InnovationNumber];

                Connection connection = new Connection(from, to)
                {
                    Enabled = c.Enabled,
                    Weight = c.Weight,
                };

                to.Connections.Add(connection);
            }
        }

        public IList<double> Calculate(IList<double> inputs)
        {
            if (inputs.Count() != InputNodes.Count)
            {
                throw new System.Exception("Data does'nt fit");
            }

            for (int i = 0; i < InputNodes.Count; i++)
            {
                InputNodes[i].Output = inputs[i];
            }

            foreach (Node n in HiddenNodes)
            {
                n.Calculate();
            }

            double[] output = new double[OutputNodes.Count];
            for (int i = 0; i < OutputNodes.Count; i++)
            {
                OutputNodes[i].Calculate();
                output[i] = OutputNodes[i].Output;
            }

            return output;
        }
    }
}
