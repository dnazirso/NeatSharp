using DataStructures.GeneticAggregate;
using NeuroEvolution;
using Prompt.Menu;
using Prompt.Sprites;
using System.Windows;

namespace Prompt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Neat neat;
        IGenome genome;
        ButtonStack buttonStack;

        public MainWindow()
        {
            InitializeComponent();

            neat = new Neat(3, 2, 0);
            genome = neat.EmptyGenome();
            buttonStack = new ButtonStack(genome, this);

            mainWindow.Children.Add(buttonStack);
            PlaceGenes();
        }

        public void PlaceGenes()
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
