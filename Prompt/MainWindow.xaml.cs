using DataStructures;
using DataStructures.GeneticAggregate;
using DataStructures.NeuroEvolutionAggregate;
using NeuroEvolution;
using Prompt.Abstraction;
using Prompt.Menu;
using Prompt.Sprites;
using System.Windows;

namespace Prompt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IRefresher
    {
        readonly IGenome genome;
        readonly Neat neat;

        public MainWindow()
        {
            InitializeComponent();

            neat = new Neat();

            double[] inputs = new double[10];
            for (int i = 0; i < 10; i++) inputs[i] = ThreadSafeRandom.Random();

            for (int i = 0; i < 100; i++)
            {
                foreach (Client c in neat.Clients.Data)
                {
                    c.Score = c.Calculate(inputs)[0];
                }
                neat.Evolve();
                neat.TraceSpecies();
            }

            genome = neat.EmptyGenome();

            ButtonStack buttonStack = new ButtonStack(genome, this);

            mainWindow.Children.Add(buttonStack);
            Refresh();
        }

        public void Refresh()
        {
            board.Children.Clear();

            for (int i = 0; i < genome.Nodes.Size(); i++)
            {
                board.Children.Add(new Node(genome.Nodes.Get(i), Width - 50, Height - 100));
            }

            for (int i = 0; i < genome.Connections.Size(); i++)
            {
                board.Children.Add(new Connection(genome.Connections.Get(i), Width - 50, Height - 100));
            }
        }
    }
}
