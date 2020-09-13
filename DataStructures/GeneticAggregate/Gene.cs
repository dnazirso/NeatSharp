namespace DataStructures.GeneticAggregate
{
    public abstract class Gene
    {
        public int InnovationNumber { get; set; }

        public Gene(int InnovationNumber)
        {
            this.InnovationNumber = InnovationNumber;
        }

        public Gene() { }
    }
}
