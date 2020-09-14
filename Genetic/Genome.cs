using DataStructures;
using DataStructures.GeneticAggregate;
using DataStructures.NeuroEvolutionAggregate;
using System;

namespace Genetic
{
    public class Genome : IGenome
    {
        public RandomHashSet<ConnectionGene> Connections { get; }
        public RandomHashSet<NodeGene> Nodes { get; }
        public INeat Neat { get; }

        public Genome(INeat Neat)
        {
            Connections = new RandomHashSet<ConnectionGene>();
            Nodes = new RandomHashSet<NodeGene>();

            this.Neat = Neat;
        }

        public double Distance(IGenome genome2)
        {
            IGenome genome1 = this;

            int highestInnovationNb1 = 0;
            int highestInnovationNb2 = 0;

            if (genome1.Connections.Size() > 0)
            {
                highestInnovationNb1 = genome1.Connections.Get(genome1.Connections.Size() - 1).InnovationNumber;
            }
            if (genome2.Connections.Size() > 0)
            {
                highestInnovationNb2 = genome2.Connections.Get(genome2.Connections.Size() - 1).InnovationNumber;
            }

            if (highestInnovationNb1 < highestInnovationNb2)
            {
                genome1 = genome2;
                genome2 = this;
            }

            int i1 = 0;
            int i2 = 0;

            int nbMatching = 0;
            int nbDisjoint = 0;

            double weightDifference = 0;

            while (i1 < genome1.Connections.Size() && i2 < genome2.Connections.Size())
            {
                ConnectionGene gene1 = genome1.Connections.Get(i1);
                ConnectionGene gene2 = genome2.Connections.Get(i2);

                if (gene1.InnovationNumber == gene2.InnovationNumber)
                {
                    nbMatching++;

                    weightDifference += Math.Abs(gene1.Weight - gene2.Weight);

                    i1++;
                    i2++;
                }
                else if (gene1.InnovationNumber > gene2.InnovationNumber)
                {
                    nbDisjoint++;
                    i2++;
                }
                else
                {
                    nbDisjoint++;
                    i1++;
                }

            }

            int nbExcess = genome1.Connections.Size() - i1;

            double nbGeneInTheLargerGenome = Math.Max(genome1.Connections.Size(), genome2.Connections.Size());
            if (nbGeneInTheLargerGenome < 20)
            {
                nbGeneInTheLargerGenome = 1;
            }

            double meanWdiff = weightDifference * Math.Max(1, nbMatching);

            return ((Constants.C1 * nbExcess + Constants.C2 * nbDisjoint) / nbGeneInTheLargerGenome) + (Constants.C3 * meanWdiff);
        }

        public void Mutate()
        {
            if (Constants.PROBABILITY_MUTATE_LINK > ThreadSafeRandom.Random()) MutateLink();
            if (Constants.PROBABILITY_MUTATE_NODE > ThreadSafeRandom.Random()) MutateNode();
            if (Constants.PROBABILITY_MUTATE_WEIGHT_RANDOM > ThreadSafeRandom.Random()) MutateWeightRandom();
            if (Constants.PROBABILITY_MUTATE_WEIGHT_SHIFT > ThreadSafeRandom.Random()) MutateWeightShift();
            if (Constants.PROBABILITY_MUTATE_WEIGHT_TOGGLE_LINK > ThreadSafeRandom.Random()) MutateToggleLink();
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
                middle = Neat.GetNode(replaceIndex - 1);
            }

            ConnectionGene connection1 = Neat.GetConnection(from, middle);
            ConnectionGene connection2 = Neat.GetConnection(middle, to);

            connection1.Weight = 1;
            connection2.Weight = connection.Weight;
            connection2.Enabled = connection.Enabled;

            Connections.Remove(connection);
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
