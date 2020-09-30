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
        public int GenomeIndex { get; set; } = 0;

        readonly RandomList<Client> clients;

        readonly Neat neat;

        public MainWindow()
        {
            InitializeComponent();

            neat = new Neat();

            clients = neat.CheckEvolutionProcess();

            ButtonStack buttonStack = new ButtonStack(clients[GenomeIndex].Genome, this);

            mainWindow.Children.Add(buttonStack);
            Refresh();
        }

        public void Refresh()
        {
            board.Children.Clear();

            for (int i = 0; i < clients[GenomeIndex].Genome.Connections.Count; i++)
            {
                board.Children.Add(new Connection(clients[GenomeIndex].Genome.Connections[i], Width - 50, Height - 100));
            }

            for (int i = 0; i < clients[GenomeIndex].Genome.Nodes.Count; i++)
            {
                board.Children.Add(new Node(clients[GenomeIndex].Genome.Nodes[i], Width - 50, Height - 100));
            }
        }
    }
}
