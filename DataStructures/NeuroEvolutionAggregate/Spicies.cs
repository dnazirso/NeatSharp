using DataStructures.GeneticAggregate;
using System.Linq;

namespace DataStructures.NeuroEvolutionAggregate
{
    public class Species
    {
        /// <summary>
        /// List of <see cref="Client"/>s of the same species
        /// </summary>
        public RandomList<Client> Clients { get; }

        /// <summary>
        /// Representative of a <see cref="Species"/>
        /// </summary>
        public Client Representative { get; private set; }

        /// <summary>
        /// Score of a <see cref="Species"/>
        /// </summary>
        public double Score { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Representative"><see cref="Client"/> choosen as representative of a <see cref="Species"/></param>
        public Species(Client Representative)
        {
            Clients = new RandomList<Client>();
            this.Representative = Representative;
            this.Representative.Species = this;
            Clients.Add(Representative);
        }

        /// <summary>
        /// Affect a <see cref="Client"/> to a <see cref="Species"/> regarding its genetical distance
        /// </summary>
        /// <param name="client">a <see cref="Client"/></param>
        /// <returns>a boolean that confirm </returns>
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

        /// <summary>
        /// Force a <see cref="Client"/> affectation to a <see cref="Species"/>
        /// </summary>
        /// <param name="client">a <see cref="Client"/></param>
        public void ForcePut(Client client)
        {
            client.Species = this;
            Clients.Add(client);
        }

        /// <summary>
        /// Annihilate a <see cref="Species"/>
        /// </summary>
        public void Extinguish()
        {
            foreach (Client c in Clients)
            {
                c.Species = null;
            }
        }

        /// <summary>
        /// Evaluate Fitness of a <see cref="Species"/> through all its <see cref="Client"/>
        /// </summary>
        public void EvaluateScore()
        {
            Score = Clients.Sum(d => d.Score) / Clients.Count;
        }

        /// <summary>
        /// Clear a <see cref="Species"/> from its <see cref="Client"/>s leaving one representative
        /// </summary>
        public void Reset()
        {
            Representative = Clients.RandomElement();
            foreach (Client c in Clients)
            {
                c.Species = null;
            }
            Clients.Clear();

            Clients.Add(Representative); ;
            Representative.Species = this;
            Score = 0;
        }

        /// <summary>
        /// Regulate the number of <see cref="Client"/> within a <see cref="Species"/>
        /// </summary>
        /// <param name="percentage">a choosen rate of client to obtain</param>
        public void Kill(double percentage)
        {
            Clients.Sort((Client c1, Client c2) => c1.CompareTo(c2));

            double amount = percentage * Clients.Count;
            for (int i = 0; i < amount; i++)
            {
                Clients[^1].Species = null;
                Clients.Remove(Clients[^1]);
            }
        }

        /// <summary>
        /// Manage breeding of <see cref="Client"/>s within a <see cref="Species"/>
        /// </summary>
        /// <returns>an offspring genetic information as <see cref="IGenome"/></returns>
        public IGenome Breed()
        {
            Client c1 = Clients.RandomElement();
            Client c2 = Clients.RandomElement();

            if (c1.Score > c2.Score) return c1.Genome.CrossOver(c2.Genome);
            return c2.Genome.CrossOver(c1.Genome);
        }

        /// <summary>
        /// Number of <see cref="Client"/>s of the same <see cref="Species"/>
        /// </summary>
        public int Count => Clients.Count;
    }
}
