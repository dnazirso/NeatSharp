using DataStructures.NeuroEvolutionAggregate;

namespace DataStructures.GeneticAggregate
{
    public static class Extensions
    {
        public static IGenome CrossOver(this IGenome parent1, IGenome parent2)
        {
            INeat neat = parent1.Neat;
            IGenome offSpringGenome = neat.EmptyGenome();

            int i1 = 0;
            int i2 = 0;

            while (i1 < parent1.Connections.Size() && i2 < parent2.Connections.Size())
            {
                ConnectionGene gene1 = parent1.Connections.Get(i1);
                ConnectionGene gene2 = parent2.Connections.Get(i2);

                if (gene1.InnovationNumber == gene2.InnovationNumber)
                {
                    if (ThreadSafeRandom.Random() > 0)
                    {
                        offSpringGenome.Connections.Add(ConnectionGene.GetConnection(gene1));
                    }
                    else
                    {
                        offSpringGenome.Connections.Add(ConnectionGene.GetConnection(gene2));
                    }

                    i1++;
                    i2++;
                }

                if (gene1.InnovationNumber > gene2.InnovationNumber)
                {
                    offSpringGenome.Connections.Add(ConnectionGene.GetConnection(gene2));
                    i2++;
                }
                else
                {
                    offSpringGenome.Connections.Add(ConnectionGene.GetConnection(gene1));
                    i1++;
                }
            }

            while (i1 < parent1.Connections.Size())
            {
                ConnectionGene gene1 = parent1.Connections.Get(i1);
                offSpringGenome.Connections.Add(ConnectionGene.GetConnection(gene1));
                i1++;
            }

            foreach (ConnectionGene c in offSpringGenome.Connections.Data)
            {
                offSpringGenome.Nodes.Add(c.From);
                offSpringGenome.Nodes.Add(c.To);
            }

            return offSpringGenome;
        }
    }
}
