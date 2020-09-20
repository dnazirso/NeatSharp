using DataStructures.GeneticAggregate;
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

            neat.CheckEvolutionProcess();

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
