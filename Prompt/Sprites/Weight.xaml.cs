using System.Windows.Controls;

namespace Prompt.Sprites
{
    /// <summary>
    /// Interaction logic for Weight.xaml
    /// </summary>
    public partial class Weight : UserControl
    {
        public Weight(string weight, double x, double y)
        {
            InitializeComponent();
            text.Text = weight;
            SetValue(Canvas.LeftProperty, x);
            SetValue(Canvas.TopProperty, y);
        }
    }
}
