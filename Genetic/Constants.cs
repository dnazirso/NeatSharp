namespace Genetic
{
    /// <summary>
    /// Constants involved in mutations
    /// </summary>
    public sealed class Constants
    {
        /// <summary>
        /// Weight shift strength
        /// </summary>
        public static readonly double WEIGHT_SHIFT_STRENGTH = 0.3;

        /// <summary>
        /// Weight random strength
        /// </summary>
        public static readonly double WEIGHT_RANDOM_STRENGTH = 0.01;

        /// <summary>
        /// Probability to add a connection
        /// </summary>
        public static readonly double PROBABILITY_MUTATE_LINK = 0.01;

        /// <summary>
        /// Probability to add a node
        /// </summary>
        public static readonly double PROBABILITY_MUTATE_NODE = 0.01;

        /// <summary>
        /// Probability to shift a weight
        /// </summary>
        public static readonly double PROBABILITY_MUTATE_WEIGHT_SHIFT = 0.02;

        /// <summary>
        /// Probability to change a weight
        /// </summary>
        public static readonly double PROBABILITY_MUTATE_WEIGHT_RANDOM = 0.02;

        /// <summary>
        /// Probability to activate or deactivate a connection
        /// </summary>
        public static readonly double PROBABILITY_MUTATE_TOGGLE_LINK = 0.02;

        /// <summary>
        /// Probability to change the activation function of a node
        /// </summary>
        public static readonly double PROBABILITY_MUTATE_ACTIVATION_RANDOM = 0.02;
    }
}
