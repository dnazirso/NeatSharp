using DataStructures.GeneticAggregate;

namespace DataStructures.NeuroEvolutionAggregate
{
    /// <summary>
    /// NeuroEvolution of Augmenting Topologies Interface
    /// </summary>
    public interface INeat
    {
        /// <summary>
        /// Create a <see cref="NodeGene"/> and and store it for future use
        /// </summary>
        /// <returns>a <see cref="NodeGene"/></returns>
        NodeGene CreateNode();

        /// <summary>
        /// Create a <see cref="Genome"/> as starter for a <see cref="Client"/>
        /// </summary>
        /// <returns>Genetical informations as an <see cref="IGenome"/></returns>
        IGenome EmptyGenome();

        /// <summary>
        /// Set a replace index as a marker between two <see cref="NodeGene"/>.
        /// Indicates if a <see cref="ConnectionGene"/> as been replaced
        /// </summary>
        /// <param name="node1">first <see cref="NodeGene"/></param>
        /// <param name="node2">second <see cref="NodeGene"/></param>
        /// <param name="index">Intially an innovation number</param>
        void SetReplaceIndex(NodeGene node1, NodeGene node2, int index);

        /// <summary>
        /// Get a replace index if existing between two <see cref="NodeGene"/>
        /// </summary>
        /// <param name="node1">first <see cref="NodeGene"/></param>
        /// <param name="node2">second <see cref="NodeGene"/></param>
        /// <returns>A replace index</returns>
        int GetReplaceIndex(NodeGene node1, NodeGene node2);

        /// <summary>
        /// Find of create a <see cref="ConnectionGene"/>
        /// </summary>
        /// <param name="In">Input <see cref="NodeGene"/> of this connection</param>
        /// <param name="Out">Output <see cref="NodeGene"/> of this connection</param>
        /// <returns></returns>
        ConnectionGene GetConnection(NodeGene From, NodeGene To);

        /// <summary>
        /// Find or Create a <see cref="NodeGene"/> by id (here, the index within the <see cref="NodeGene"/> list)
        /// </summary>
        /// <param name="id">the index within the <see cref="NodeGene"/> list</param>
        NodeGene GetNode(int id);
    }
}