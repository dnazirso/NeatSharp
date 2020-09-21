namespace DataStructures.GeneticAggregate
{
    public class ConnectionGene : Gene
    {
        public NodeGene From { get; set; }
        public NodeGene To { get; set; }

        public double Weight { get; set; }
        public bool Enabled { get; set; } = true;

        public int ReplaceIndex { get; set; }

        public ConnectionGene(NodeGene From, NodeGene To)
        {
            this.From = From;
            this.To = To;
        }

        public override bool Equals(object obj)
        {
            if (obj is ConnectionGene c)
            {
                return From.Equals(c.From) && To.Equals(c.To);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return From.InnovationNumber * Constants.MAX_NODES + To.InnovationNumber;
        }
    }
}
