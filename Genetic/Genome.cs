﻿using DataStructures;
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

            int highestInnovationNb1 = genome1.Connections.Get(genome1.Connections.Size() - 1).InnovationNumber;
            int highestInnovationNb2 = genome2.Connections.Get(genome2.Connections.Size() - 1).InnovationNumber;

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
            double nbGeneInTheLargerGenome = Math.Max(genome1.Connections.Size(), genome2.Connections.Size());

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

                if (i1 > i2)
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

            return ((Constants.C1 * nbExcess + Constants.C2 * nbDisjoint) / nbGeneInTheLargerGenome) + (Constants.C3 * weightDifference / nbMatching);
        }

        public static IGenome CrossOver(IGenome parent1, IGenome parent2)
        {
            INeat neat = parent1.Neat;
            IGenome offSpringGenome = neat.EmptyGenome();

            int i1 = 0;
            int i2 = 0;

            while (i1 < parent1.Connections.Size() && i2 < parent2.Connections.Size())
            {
                ConnectionGene gene1 = parent1.Connections.Get(i1);
                ConnectionGene gene2 = parent2.Connections.Get(i2);

                // matching gene case
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

                // disjoint gene case
                if (i1 > i2)
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

            // ??? travail déjà fait au dessus
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

                if (a.X.Equals(b.X))
                {
                    continue;
                }

                ConnectionGene connection;
                if (a.X < b.X)
                {
                    connection = new ConnectionGene(a, b);
                }
                else
                {
                    connection = new ConnectionGene(b, a);
                }

                if (Connections.Contains(connection))
                {
                    continue;
                }

                connection = Neat.GetConnection(connection.From, connection.To);
                connection.Weight += ThreadSafeRandom.NormalRand() * Constants.WEIGHT_SHIFT_STRENGTH;

                Connections.AddSorted(connection);

                return;
            }
        }

        public void MutateNode()
        {
            ConnectionGene connection = Connections.RandomElement();
            if (connection != null)
            {
                NodeGene From = connection.From;
                NodeGene To = connection.To;

                NodeGene middle = Neat.CreateNode();
                middle.X = (From.X + To.X) / 2;
                middle.Y = ((From.Y + To.Y) / 2) + ThreadSafeRandom.NormalRand(0, 0.05f);

                ConnectionGene connection1 = Neat.GetConnection(From, middle);
                ConnectionGene connection2 = Neat.GetConnection(middle, To);

                Connections.Remove(connection);
                Connections.Add(connection1);
                Connections.Add(connection2);

                Nodes.Add(middle);
            }
        }

        public void MutateWeightShift()
        {
            ConnectionGene connection = Connections.RandomElement();
            if (connection != null)
            {
                connection.Weight += ThreadSafeRandom.NormalRand() * Constants.WEIGHT_SHIFT_STRENGTH;
            }
        }

        public void MutateWeightRandom()
        {
            ConnectionGene connection = Connections.RandomElement();
            if (connection != null)
            {
                connection.Weight += ThreadSafeRandom.NormalRand() * Constants.WEIGHT_RANDOM_STRENGTH;
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