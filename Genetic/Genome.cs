using DataStructures;
using DataStructures.GeneticAggregate;
using DataStructures.NeuroEvolutionAggregate;

namespace Genetic
{
    public class Genome : IGenome
    {
        public RandomList<ConnectionGene> Connections { get; }
        public RandomList<NodeGene> Nodes { get; }
        public INeat Neat { get; }

        public Genome(INeat Neat)
        {
            Connections = new RandomList<ConnectionGene>();
            Nodes = new RandomList<NodeGene>();

            this.Neat = Neat;
        }

        public void Mutate()
        {
            if (Constants.PROBABILITY_MUTATE_LINK > ThreadSafeRandom.Random()) MutateLink();
            if (Constants.PROBABILITY_MUTATE_NODE > ThreadSafeRandom.Random()) MutateNode();
            if (Constants.PROBABILITY_MUTATE_WEIGHT_RANDOM > ThreadSafeRandom.Random()) MutateWeightRandom();
            if (Constants.PROBABILITY_MUTATE_WEIGHT_SHIFT > ThreadSafeRandom.Random()) MutateWeightShift();
            if (Constants.PROBABILITY_MUTATE_TOGGLE_LINK > ThreadSafeRandom.Random()) MutateToggleLink();
        }

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

                connection = Neat.GetConnection(connection.From, connection.To);
                connection.Weight += ThreadSafeRandom.NormalRand(0, 0.2f) * Constants.WEIGHT_SHIFT_STRENGTH;

                Connections.AddSorted(connection);

                return;
            }
        }

        public void MutateNode()
        {
            ConnectionGene connection = Connections.RandomElement();
            if (connection == null) return;

            NodeGene from = connection.From;
            NodeGene to = connection.To;

            int replaceIndex = Neat.GetReplaceIndex(from, to);

            NodeGene middle;
            if (replaceIndex == 0)
            {
                middle = Neat.CreateNode();
                middle.X = (from.X + to.X) / 2;
                middle.Y = ((from.Y + to.Y) / 2) + (ThreadSafeRandom.NormalRand(0, 0.02f) / 2);
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

        public void MutateWeightShift()
        {
            ConnectionGene connection = Connections.RandomElement();
            if (connection != null)
            {
                connection.Weight += ThreadSafeRandom.NormalRand(0, 0.2f) * Constants.WEIGHT_SHIFT_STRENGTH;
            }
        }

        public void MutateWeightRandom()
        {
            ConnectionGene connection = Connections.RandomElement();
            if (connection != null)
            {
                connection.Weight = ThreadSafeRandom.NormalRand(0, 0.2f) * Constants.WEIGHT_RANDOM_STRENGTH;
            }
        }

        public void MutateToggleLink()
        {
            ConnectionGene connection = Connections.RandomElement();
            if (connection != null)
            {
                connection.Enabled = !connection.Enabled;
            }
        }
    }
}
