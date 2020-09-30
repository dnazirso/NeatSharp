using DataStructures.GeneticAggregate;
using NeuroEvolution;
using Prompt.Abstraction;
using System.Windows;
using System.Windows.Controls;

namespace Prompt.Menu
{
    /// <summary>
    /// Interaction logic for Panel.xaml
    /// </summary>
    public partial class ButtonStack : UserControl
    {
        readonly IGenome genome;
        readonly IRefresher refresher;

        public ButtonStack(IGenome genome, IRefresher main)
        {
            this.genome = genome;
            this.refresher = main;

            InitializeComponent();
        }

        private void Random_Weight(object sender, RoutedEventArgs e)
        {
            genome.MutateWeightRandom();
            refresher.Refresh();
        }

        private void Shift_Weight(object sender, RoutedEventArgs e)
        {
            genome.MutateWeightShift();
            refresher.Refresh();
        }

        private void Mutate_Link(object sender, RoutedEventArgs e)
        {
            genome.MutateLink();
            refresher.Refresh();
        }

        private void Mutate_Node(object sender, RoutedEventArgs e)
        {
            genome.MutateNode();
            refresher.Refresh();
        }

        private void Mutate_Activation(object sender, RoutedEventArgs e)
        {
            genome.MutateActivationRandom();
            refresher.Refresh();
        }

        private void Calculate(object sender, RoutedEventArgs e)
        {
            //genome.GenerateCalculator();

            //double[] d = genome.Calculate(new double[] { 1, 1, 1 }).ToArray();

            //TraceCalculation(d);

            //refresher.Refresh();
        }

        private void Mutate(object sender, RoutedEventArgs e)
        {
            genome.Mutate();
            refresher.Refresh();
        }

        private void On_Off(object sender, RoutedEventArgs e)
        {
            genome.MutateToggleLink();
            refresher.Refresh();
        }

        private void Get_Prev(object sender, RoutedEventArgs e)
        {
            refresher.GenomeIndex--;
            if (refresher.GenomeIndex < 0) refresher.GenomeIndex = 0;
            refresher.Refresh();
        }

        private void Get_Next(object sender, RoutedEventArgs e)
        {
            refresher.GenomeIndex++;
            if (refresher.GenomeIndex > Constants.MaxClients - 1) refresher.GenomeIndex = Constants.MaxClients - 1;
            refresher.Refresh();
        }
    }
}
