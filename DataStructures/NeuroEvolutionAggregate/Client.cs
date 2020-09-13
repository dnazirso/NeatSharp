using DataStructures.Calculation;
using DataStructures.GeneticAggregate;
using System.Collections.Generic;

namespace DataStructures.NeuroEvolutionAggregate
{
    public class Client
    {
        public IGenome Genome { get; set; }
        public double Score { get; set; }
        public Spicies Spicies { get; set; }
        public Calculator Calculator { get; set; }

        public void GenerateCalculator()
        {
            Calculator = new Calculator(Genome);
        }

        public IList<double> Calculate(IList<double> input)
        {
            if (Calculator == null) GenerateCalculator();
            return Calculator.Calculate(input);
        }

        public double Distance(Client other) => Genome.Distance(other.Genome);
        public void Mutate() => Genome.Mutate();
    }
}
