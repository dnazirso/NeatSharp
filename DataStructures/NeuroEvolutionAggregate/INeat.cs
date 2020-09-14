using DataStructures.GeneticAggregate;

namespace DataStructures.NeuroEvolutionAggregate
{
    public interface INeat
    {
        NodeGene CreateNode();
        IGenome EmptyGenome();
        void SetReplaceIndex(NodeGene node1, NodeGene node2, int index);
        int GetReplaceIndex(NodeGene node1, NodeGene node2);
        ConnectionGene GetConnection(NodeGene From, NodeGene To);
        NodeGene GetNode(int id);
    }
}