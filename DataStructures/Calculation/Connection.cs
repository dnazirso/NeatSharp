namespace DataStructures.Calculation
{
    /// <summary>
    /// Represent an axon within a neural network
    /// </summary>
    public class Connection
    {
        /// <summary>
        /// Node that provides informations
        /// </summary>
        public Node In { get; set; }

        /// <summary>
        /// Weight of an information
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Indicate whether of not a <see cref="Connection"/> is used
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="In">Information provider</param>
        public Connection(Node In)
        {
            this.In = In;
        }
    }
}
