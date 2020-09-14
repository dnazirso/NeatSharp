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

        public override string ToString()
        {
            return "ConnectionGene{" +
                    "From=" + From.InnovationNumber +
                    ", To=" + To.InnovationNumber +
                    ", Weight=" + Weight +
                    ", Enabled=" + Enabled +
                    ", InnovationNumber=" + InnovationNumber+
                    '}';
        }

        public static ConnectionGene GetConnection(ConnectionGene connection)
        {
            ConnectionGene gene = new ConnectionGene(connection.From, connection.To)
            {
                InnovationNumber = connection.InnovationNumber,
                Weight = connection.Weight,
                Enabled = connection.Enabled
            };
            return gene;
        }
    }
}
