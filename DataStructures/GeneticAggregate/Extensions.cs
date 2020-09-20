﻿using DataStructures.NeuroEvolutionAggregate;
using System;

namespace DataStructures.GeneticAggregate
{
    public static class Extensions
    {
        public static double Distance(this IGenome genome1, IGenome genome2)
        {
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
                IGenome genome0 = genome1;
                genome1 = genome2;
                genome2 = genome0;
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

            double meanWdiff = weightDifference / Math.Max(1, nbMatching);

            return ((Constants.C1 * nbExcess + Constants.C2 * nbDisjoint ) / nbGeneInTheLargerGenome) + Constants.C3 * meanWdiff;
        }

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
                    if (ThreadSafeRandom.Random() > 0.5)
                    {
                        offSpringGenome.Connections.Add(gene1.GetConnection());
                    }
                    else
                    {
                        offSpringGenome.Connections.Add(gene2.GetConnection());
                    }

                    i1++;
                    i2++;
                }
                else if (gene1.InnovationNumber > gene2.InnovationNumber)
                {
                    offSpringGenome.Connections.Add(gene2.GetConnection());
                    i2++;
                }
                else
                {
                    offSpringGenome.Connections.Add(gene1.GetConnection());
                    i1++;
                }
            }

            while (i1 < parent1.Connections.Size())
            {
                ConnectionGene gene1 = parent1.Connections.Get(i1);
                offSpringGenome.Connections.Add(gene1.GetConnection());
                i1++;
            }

            foreach (ConnectionGene c in offSpringGenome.Connections.Data)
            {
                offSpringGenome.Nodes.Add(c.From);
                offSpringGenome.Nodes.Add(c.To);
            }

            return offSpringGenome;
        }

        private static ConnectionGene GetConnection(this ConnectionGene connection)
        {
            ConnectionGene gene = new ConnectionGene(connection.From, connection.To)
            {
                InnovationNumber = connection.InnovationNumber,
                Weight = connection.Weight,
                Enabled = connection.Enabled
            };
            return gene;
        }
    }
}
