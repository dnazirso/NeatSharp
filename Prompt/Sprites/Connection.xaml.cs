using DataStructures.GeneticAggregate;
using System.Windows.Controls;
using System.Windows.Media;

namespace Prompt.Sprites
{
    /// <summary>
    /// Interaction logic for Connection.xaml
    /// </summary>
    public partial class Connection : UserControl
    {
        public Connection(ConnectionGene gene, double width, double height)
        {
            InitializeComponent();
            Place(gene.From, gene.To, width, height);
        }

        public void Place(NodeGene from, NodeGene to, double width, double height)
        {
            connection.X1 = from.X * width + 10;
            connection.X2 = to.X * width + 10;
            connection.Y1 = from.Y * height + 10;
            connection.Y2 = to.Y * height + 10;
        }

        public void Activate(bool enabled)
        {
            connection.Stroke = enabled ? Brushes.Lime : Brushes.Gainsboro;
        }
    }
}
