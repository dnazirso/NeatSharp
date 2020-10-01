namespace DataStructures.GeneticAggregate
{
    public class ConnectionGene : Gene
    {
        public NodeGene In { get; set; }
        public NodeGene Out { get; set; }

        public double Weight { get; set; }
        public bool Enabled { get; set; } = true;

        public int ReplaceIndex { get; set; }

        public ConnectionGene(NodeGene In, NodeGene Out)
        {
            this.In = In;
            this.Out = Out;
        }

        public override bool Equals(object obj)
        {
            if (obj is ConnectionGene c)
            {
                return In.Equals(c.In) && Out.Equals(c.Out);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return In.InnovationNumber * Constants.MAX_NODES + Out.InnovationNumber;
        }
    }
}
