using DataStructures.Calculation.ActivationStrategy;

namespace DataStructures.GeneticAggregate
{
    public class NodeGene : Gene
    {
        public double X { get; set; }
        public double Y { get; set; }
        public IActivationFunction Activation { get; set; }
        public string ActivationName { get; set; }

        public NodeGene(int innovationNumber) : base(innovationNumber) { }

        public override bool Equals(object obj)
        {
            if (obj is NodeGene n)
            {
                return InnovationNumber.Equals(n.InnovationNumber);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return InnovationNumber;
        }
    }
}
