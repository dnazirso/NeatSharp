using DataStructures.NeuroEvolutionAggregate;

namespace DataStructures.GeneticAggregate
{
    /// <summary>
    /// Genetic informations store interface
    /// </summary>
    public interface IGenome
    {
        /// <summary>
        /// <see cref="ConnectionGene"/>s of this <see cref="Genome"/>
        /// </summary>
        RandomList<ConnectionGene> Connections { get; }

        /// <summary>
        /// <see cref="NodeGene"/>s of this <see cref="Genome"/>
        /// </summary>
        RandomList<NodeGene> Nodes { get; }

        /// <summary>
        /// Aknowledge and manage existing <see cref="Gene"/>s through all species
        /// </summary>
        INeat Neat { get; }

        /// <summary>
        /// Manage mutation regarding their probabilities
        /// </summary>
        void Mutate();

        /// <summary>
        /// Add a <see cref="ConnectionGene"/> between two existing <see cref="NodeGene"/>s
        /// </summary>
        void MutateLink();

        /// <summary>
        /// Add a <see cref="NodeGene"/> on an existing <see cref="ConnectionGene"/>
        /// </summary>
        void MutateNode();

        /// <summary>
        /// Activate or deactivate a random <see cref="ConnectionGene"/>
        /// </summary>
        void MutateToggleLink();

        /// <summary>
        /// Randomly change the weight of a random <see cref="ConnectionGene"/>
        /// </summary>
        void MutateWeightRandom();

        /// <summary>
        /// Randomly shift the weight of a random <see cref="ConnectionGene"/>
        /// </summary>
        void MutateWeightShift();

        /// <summary>
        /// Change the activation function of a random <see cref="NodeGene"/>
        /// </summary>
        void MutateActivationRandom();
    }
}