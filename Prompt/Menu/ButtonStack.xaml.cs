using DataStructures.GeneticAggregate;
using System.Windows;
using System.Windows.Controls;

namespace Prompt.Menu
{
    /// <summary>
    /// Interaction logic for Panel.xaml
    /// </summary>
    public partial class ButtonStack : UserControl
    {
        IGenome genome;
        MainWindow main;

        public ButtonStack(IGenome genome, MainWindow main)
        {
            this.genome = genome;
            this.main = main;

            InitializeComponent();
        }

        private void Random_Weight(object sender, RoutedEventArgs e)
        {
            genome.MutateWeightRandom();
            main.PlaceGenes();
        }

        private void Shift_Weight(object sender, RoutedEventArgs e)
        {
            genome.MutateWeightShift();
            main.PlaceGenes();
        }

        private void Mutate_Link(object sender, RoutedEventArgs e)
        {
            genome.MutateLink();
            main.PlaceGenes();
        }

        private void Mutate_Node(object sender, RoutedEventArgs e)
        {
            genome.MutateNode();
            main.PlaceGenes();
        }

        private void Calculate(object sender, RoutedEventArgs e)
        {

        }

        private void Mutate(object sender, RoutedEventArgs e)
        {
            genome.Mutate();
            main.PlaceGenes();
        }

        private void On_Off(object sender, RoutedEventArgs e)
        {

        }
    }
}
