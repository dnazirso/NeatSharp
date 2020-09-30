using DataStructures.GeneticAggregate;
using System.Windows.Controls;

namespace Prompt.Sprites
{
    /// <summary>
    /// Interaction logic for Node.xaml
    /// </summary>
    public partial class Node : UserControl
    {
        public Node()
        {
            InitializeComponent();
        }
        public Node(NodeGene gene, double width, double height)
        {
            InitializeComponent();

            SetValue(Canvas.LeftProperty, gene.X * width);
            SetValue(Canvas.TopProperty, gene.Y * height);
            node.Fill = NodeColors.SetColor(gene.ActivationName);
        }
    }
}
