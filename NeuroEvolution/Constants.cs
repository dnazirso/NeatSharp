namespace NeuroEvolution
{
    /// <summary>
    /// Constants involved in NEAT logic instanciation
    /// </summary>
    public sealed class Constants
    {
        /// <summary>
        /// Number of inputs
        /// </summary>
        public static readonly int InputSize = 3;

        /// <summary>
        /// Number of ouputs
        /// </summary>
        public static readonly int OutputSize = 2;

        /// <summary>
        /// Maximum number of clients to manage
        /// </summary>
        public static readonly int MaxClients = 1000;

        /// <summary>
        /// Regulate number of client within a species
        /// </summary>
        public static readonly double SURVIVAL_RATE = 0.8;
    }
}
