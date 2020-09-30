using DataStructures.Calculation.ActivationStrategy;
using System.Windows.Media;

namespace Prompt.Sprites
{
    public static class NodeColors
    {
        public static Brush SetColor(string name) => name switch
        {
            ActivationName.Abs => Brushes.Lime,
            ActivationName.Elu => Brushes.Gold,
            ActivationName.Relu => Brushes.DodgerBlue,
            ActivationName.Sigmoid => Brushes.MediumVioletRed,
            _ => Brushes.White,
        };
    }
}
