using System;

namespace DataStructures
{
    public sealed class Constants
    {
        /// <summary>
        /// Number of maximum nodes
        /// </summary>
        public static readonly int MAX_NODES = (int)Math.Pow(2, 20);

        /// <summary>
        /// threshold that decide when two client are too geneticaly distant under its value
        /// </summary>
        public static readonly double CP = 4;

        /// <summary>
        /// Importance of the excess gene ratio when creating an offspring
        /// </summary>
        public static readonly double C1 = 1;

        /// <summary>
        /// Importance of the disjoint gene ratio when creating an offspring
        /// </summary>
        public static readonly double C2 = 1;

        /// <summary>
        /// Importance of the mean weight difference ratio through genes when creating an offspring
        /// </summary>
        public static readonly double C3 = 1;
    }
}
