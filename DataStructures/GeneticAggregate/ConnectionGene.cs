namespace DataStructures.GeneticAggregate
{
    public class ConnectionGene : Gene
    {
        /// <summary>
        /// Input <see cref="NodeGene"/> of this <see cref="ConnectionGene"/>
        /// </summary>
        public NodeGene In { get; set; }

        /// <summary>
        /// Output <see cref="NodeGene"/> of this <see cref="ConnectionGene"/>
        /// </summary>
        public NodeGene Out { get; set; }

        /// <summary>
        /// Weight of the incomming informations
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Indicate whether of not a <see cref="ConnectionGene"/> is used
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Indicates if a <see cref="ConnectionGene"/> as been replaced
        /// </summary>
        public int ReplaceIndex { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="In"><see cref="In"/></param>
        /// <param name="Out"><see cref="Out"/></param>
        public ConnectionGene(NodeGene In, NodeGene Out)
        {
            this.In = In;
            this.Out = Out;
        }

        /// <summary>
        /// Check equatlity between two <see cref="ConnectionGene"/>s
        /// </summary>
        /// <param name="obj">Another <see cref="ConnectionGene"/></param>
        /// <returns>a boolean that confirm or not the equality</returns>
        public override bool Equals(object obj)
        {
            if (obj is ConnectionGene c)
            {
                return In.Equals(c.In) && Out.Equals(c.Out);
            }
            return false;
        }

        /// <summary>
        /// Get the object hashcode which in this case is a computed number of <see cref="In"/> and <see cref="Out"/> <see cref="Gene.InnovationNumber"/>s
        /// </summary>
        /// <returns>computed number of <see cref="In"/> and <see cref="Out"/> <see cref="Gene.InnovationNumber"/>s</returns>
        public override int GetHashCode()
        {
            return In.InnovationNumber * Constants.MAX_NODES + Out.InnovationNumber;
        }
    }
}
