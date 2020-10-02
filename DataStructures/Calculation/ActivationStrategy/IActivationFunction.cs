namespace DataStructures.Calculation.ActivationStrategy
{
    /// <summary>
    /// Activation function interace
    /// </summary>
    public interface IActivationFunction
    {
        /// <summary>
        /// Compute the activation
        /// </summary>
        /// <param name="z"></param>
        /// <returns></returns>
        double Activate(double z);
    }
}
