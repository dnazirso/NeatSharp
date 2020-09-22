using DataStructures.NeuroEvolutionAggregate;

namespace DataStructures.GeneticAggregate
{
    public interface IGenome
    {
        RandomHashSet<ConnectionGene> Connections { get; }
        RandomHashSet<NodeGene> Nodes { get; }
        INeat Neat { get; }

        void Mutate();
        void MutateLink();
        void MutateNode();
        void MutateToggleLink();
        void MutateWeightRandom();
        void MutateWeightShift();
    }
}