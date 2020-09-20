namespace DataStructures.Calculation
{
    public class Connection
    {
        public Node From { get; set; }
        public Node To { get; set; }

        public double Weight { get; set; }
        public bool Enabled { get; set; } = true;

        public Connection(Node From, Node To)
        {
            this.From = From;
            this.To = To;
        }
    }
}
