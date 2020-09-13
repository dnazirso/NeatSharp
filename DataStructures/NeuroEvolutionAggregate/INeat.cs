using DataStructures.GeneticAggregate;

namespace DataStructures.NeuroEvolutionAggregate
{
    public interface INeat
    {
        NodeGene CreateNode();
        IGenome EmptyGenome();
        ConnectionGene GetConnection(NodeGene From, NodeGene To);
        NodeGene GetNode(int id);
    }
}