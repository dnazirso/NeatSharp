using DataStructures.Calculation;
using DataStructures.NeuroEvolutionAggregate;
using System.Collections.Generic;

namespace DataStructures.GeneticAggregate
{
    public interface IGenome
    {
        RandomHashSet<ConnectionGene> Connections { get; }
        INeat Neat { get; }
        Calculator Calculator { get; set; }
        RandomHashSet<NodeGene> Nodes { get; }

        double Distance(IGenome genome2);
        void GenerateCalculator();
        IList<double> Calculate(IList<double> input);
        void Mutate();
        void MutateLink();
        void MutateNode();
        void MutateToggleLink();
        void MutateWeightRandom();
        void MutateWeightShift();
    }
}