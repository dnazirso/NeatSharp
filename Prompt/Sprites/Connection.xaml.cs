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
            Place(gene.From, gene.To, width, height, gene.Enabled);

            text.Text = gene.Weight.ToString();
        }

        public void Place(NodeGene from, NodeGene to, double width, double height, bool enabled)
        {
            connection.X1 = from.X * width + 10;
            connection.X2 = to.X * width + 10;
            connection.Y1 = from.Y * height + 10;
            connection.Y2 = to.Y * height + 10;

            connection.Stroke = enabled ? Brushes.Lime : Brushes.Red;

            double X = (from.X + to.X) * width / 2;
            double Y = (from.Y + to.Y) * height / 2;

            text.Margin = new System.Windows.Thickness(X, Y, 0, 0);
        }
    }
}
