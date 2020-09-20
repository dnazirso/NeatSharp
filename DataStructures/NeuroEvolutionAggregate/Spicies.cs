using DataStructures.GeneticAggregate;
using System.Linq;

namespace DataStructures.NeuroEvolutionAggregate
{
    public class Species
    {
        public RandomHashSet<Client> Clients { get; }
        public Client Representative { get; private set; }
        public double Score { get; private set; }

        public Species(Client Representative)
        {
            Clients = new RandomHashSet<Client>();
            this.Representative = Representative;
            this.Representative.Species = this;
            Clients.Add(Representative);
        }

        public bool Put(Client client)
        {
            if (client.Distance(Representative) < Constants.CP)
            {
                client.Species = this;
                Clients.Add(client);
                return true;
            }
            return false;
        }

        public void ForcePut(Client client)
        {
            client.Species = this;
            Clients.Add(client);
        }

        public void Extinguish()
        {
            foreach (Client c in Clients.Data)
            {
                c.Species = null;
            }
        }

        public void EvaluateScore()
        {
            Score = Clients.Data.Sum(d => d.Score) / Clients.Size();
        }

        public void Reset()
        {
            Representative = Clients.RandomElement();
            foreach (Client c in Clients.Data)
            {
                c.Species = null;
            }
            Clients.Clear();

            Clients.Add(Representative); ;
            Representative.Species = this;
            Score = 0;
        }

        public void Kill(double percentage)
        {
            Clients.Data.Sort((Client c1, Client c2) => c1.Score.CompareTo(c2.Score));

            double amount = percentage * Clients.Size();
            for (int i = 0; i < amount; i++)
            {
                Clients.Get(0).Species = null;
                Clients.Remove(0);
            }
        }

        public IGenome Breed()
        {
            Client c1 = Clients.RandomElement();
            Client c2 = Clients.RandomElement();

            if (c1.Score > c2.Score) return c1.Genome.CrossOver(c2.Genome);
            return c2.Genome.CrossOver(c1.Genome);
        }

        public int Size() => Clients.Size();
    }
}
