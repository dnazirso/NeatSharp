using DataStructures.GeneticAggregate;

namespace DataStructures.NeuroEvolutionAggregate
{
    public interface INeat
    {
        int InputSize { get; set; }
        int MaxClients { get; set; }
        int OutputSize { get; set; }

        NodeGene CreateNode();
        IGenome EmptyGenome();
        ConnectionGene GetConnection(NodeGene From, NodeGene To);
        NodeGene GetNode(int id);
    }
}