using DataStructures.GeneticAggregate;

namespace DataStructures.NeuroEvolutionAggregate
{
    public class Spicies
    {
        public RandomHashSet<Client> Clients { get; }
        public Client Representative { get; private set; }
        public double Score { get; private set; }

        public Spicies(Client Representative)
        {
            this.Representative = Representative;
            this.Representative.Spicies = this;
            Clients.Add(Representative);
        }

        public bool Put(Client client)
        {
            if (client.Distance(Representative) < Constants.CP)
            {
                client.Spicies = this;
                Clients.Add(Representative);
                return true;
            }
            return false;
        }

        public void ForcePut(Client client)
        {
            client.Spicies = this;
            Clients.Add(Representative);
        }

        public void Extinguish()
        {
            foreach (Client c in Clients.Data)
            {
                c.Spicies = null;
            }
        }

        public void EvaluateScore()
        {
            double v = 0;
            foreach (Client c in Clients.Data)
            {
                v += c.Score;
            }
            Score = v / Clients.Size();
        }

        public void Reset()
        {
            Representative = Clients.RandomElement();
            foreach (Client c in Clients.Data)
            {
                c.Spicies = null;
            }
            Clients.Clear();

            Clients.Add(Representative); ;
            Representative.Spicies = this;
            Score = 0;
        }

        public void Kill(double percentage)
        {
            Clients.Data.Sort((Client c1, Client c2) => c1.Score.CompareTo(c2.Score));

            for (int i = 0; i < percentage * Clients.Size(); i++)
            {
                Clients.Get(0).Spicies = null;
            }
        }

        public IGenome Breed()
        {
            Client c1 = Clients.RandomElement();
            Client c2 = Clients.RandomElement();

            if (c1.Score > c2.Score) c1.Genome.CrossOver(c2.Genome);
            return c2.Genome.CrossOver(c1.Genome);
        }

        public int Size() => Clients.Size();
    }
}
