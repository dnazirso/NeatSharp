using DataStructures.GeneticAggregate;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            genome.GenerateCalculator();

            double[] d = genome.Calculate(new double[] { 1, 1, 1 }).ToArray();

            TraceCalculation(d);

            main.PlaceGenes();
        }

        private void Mutate(object sender, RoutedEventArgs e)
        {
            genome.Mutate();
            main.PlaceGenes();
        }

        private void On_Off(object sender, RoutedEventArgs e)
        {
            genome.MutateToggleLink();
            main.PlaceGenes();
        }

        private static void TraceCalculation(double[] d)
        {
            for (int i = 0; i < d.Length; i++)
            {
                if (i == 0) Trace.Write($"[");
                Trace.Write($"{d[i]}");
                if (i == d.Length - 1)
                {
                    Trace.Write($"]\n");
                }
                else
                {
                    Trace.Write(", ");
                }
            }
        }

    }
}
