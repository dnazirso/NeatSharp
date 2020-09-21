using DataStructures.Calculation;
using DataStructures.GeneticAggregate;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DataStructures.NeuroEvolutionAggregate
{
    public class Client : IComparable<Client>
    {
        public IGenome Genome { get; set; }
        public double Score { get; set; }
        public Species Species { get; set; }
        public Calculator Calculator { get; set; }

        public Client(IGenome Genome)
        {
            this.Genome = Genome;
            Calculator = new Calculator(Genome);
        }

        public void RegenerateCalculator() => Calculator = new Calculator(Genome);
        public IList<double> Calculate(IList<double> input) => Calculator.Calculate(input);
        public double Distance(Client other) => Genome.Distance(other.Genome);
        public void Mutate() => Genome.Mutate();

        public int CompareTo([AllowNull] Client other)
        {
            if (Score > other.Score) return -1;
            if (Score < other.Score) return 1;
            return 0;
        }
    }
}
