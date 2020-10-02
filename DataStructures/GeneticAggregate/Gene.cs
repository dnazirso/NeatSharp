namespace DataStructures.GeneticAggregate
{
    /// <summary>
    /// Represents a <see cref="Gene"/> abstraction
    /// </summary>
    public abstract class Gene
    {
        /// <summary>
        /// A number used as an historical marker
        /// </summary>
        public int InnovationNumber { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="InnovationNumber">The innovation number</param>
        public Gene(int InnovationNumber)
        {
            this.InnovationNumber = InnovationNumber;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Gene() { }
    }
}
