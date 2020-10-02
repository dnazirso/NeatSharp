using DataStructures.Calculation.ActivationStrategy;

namespace DataStructures.GeneticAggregate
{
    public class NodeGene : Gene
    {
        /// <summary>
        /// X coordinate of a <see cref="NodeGene"/> when displaying
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Y coordinate of a <see cref="NodeGene"/> when displaying
        /// </summary>
        public double Y { get; set; }
        
        /// <summary>
        /// Activation function object
        /// </summary>
        public IActivationFunction Activation { get; set; }
        
        /// <summary>
        /// Activation function name
        /// </summary>
        public string ActivationName { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="innovationNumber">Innovation number</param>
        public NodeGene(int innovationNumber) : base(innovationNumber) { }

        /// <summary>
        /// Check equality between two <see cref="NodeGene"/>s
        /// </summary>
        /// <param name="obj">Another <see cref="NodeGene"/></param>
        /// <returns>a boolean that confirm or not the equality</returns>
        public override bool Equals(object obj)
        {
            if (obj is NodeGene n)
            {
                return InnovationNumber.Equals(n.InnovationNumber);
            }
            return false;
        }

        /// <summary>
        /// Get the object hashcode which in this case is the <see cref="Gene.InnovationNumber"/>
        /// </summary>
        /// <returns>the <see cref="Gene.InnovationNumber"/></returns>
        public override int GetHashCode()
        {
            return InnovationNumber;
        }
    }
}
