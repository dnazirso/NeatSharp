using DataStructures.Calculation;
using DataStructures.GeneticAggregate;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DataStructures.NeuroEvolutionAggregate
{
    public class Client : IComparable<Client>
    {
        /// <summary>
        /// Genetic informations
        /// </summary>
        public IGenome Genome { get; set; }

        /// <summary>
        /// <see cref="Client"/>'s scores
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// <see cref="Client"/>'s <see cref="Species"/>
        /// </summary>
        public Species Species { get; set; }

        /// <summary>
        /// Compute outputs and scores of a <see cref="Client"/>
        /// </summary>
        public Calculator Calculator { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Genome">Bare genetic informations</param>
        public Client(IGenome Genome)
        {
            this.Genome = Genome;
            Calculator = new Calculator(Genome);
        }

        /// <summary>
        /// Regenerate a Calculator in order to recompute scores and outputs
        /// </summary>
        public void RegenerateCalculator() => Calculator = new Calculator(Genome);

        /// <summary>
        /// Compute outputs of a <see cref="Client"/>
        /// </summary>
        /// <param name="input">Inputs list</param>
        /// <returns>Outputs list</returns>
        public IList<double> Calculate(IList<double> input) => Calculator.Calculate(input);

        /// <summary>
        /// Compute genetical distance from another <see cref="Client"/>
        /// </summary>
        /// <param name="other">another <see cref="Client"/></param>
        /// <returns>the genetical distance between two <see cref="Client"/></returns>
        public double Distance(Client other) => Genome.Distance(other.Genome);

        /// <summary>
        /// Proceed to genetical mutation
        /// </summary>
        public void Mutate() => Genome.Mutate();

        /// <summary>
        /// Compare two <see cref="Client"/>s base on their scores
        /// </summary>
        /// <param name="other">another <see cref="Client"/></param>
        /// <returns>an index shift</returns>
        public int CompareTo([AllowNull] Client other)
        {
            if (Score > other.Score) return -1;
            if (Score < other.Score) return 1;
            return 0;
        }
    }
}
